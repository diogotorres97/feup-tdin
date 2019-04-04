using System;
using System.Collections.Generic;
using System.Threading;

public class RestaurantSingleton : MarshalByRefObject, IRestaurantSingleton
{
    private static int _nextOrderId;
    private List<Order> _orderList;
    private List<Table> _tableList;
    private List<Product> _productList;
    public event OperationDelegate<Order> OperationOrderEvent;
    public event OperationDelegate<Table> OperationTableEvent;
    public event OperationDelegate<Invoice> PrintEvent;

    public RestaurantSingleton()
    {
        Console.WriteLine("Constructor called.");
        _orderList = new List<Order>();
        CreateTables(10);
        CreateProducts();
    }

    private void CreateTables(uint numberOfTables)
    {
        _tableList = new List<Table>();
        for (int i = 0; i < numberOfTables; i++)
        {
            _tableList.Add(new Table());
        }
    }

    private void CreateProducts()
    {
        _productList = new List<Product>
        {
            new Product("Water", 1, ProductType.Drink),
            new Product("Coca-Cola", 2, ProductType.Drink),
            new Product("Red Wine", 2, ProductType.Drink),
            new Product("White Wine", 2.5, ProductType.Drink),
            new Product("Coffee", 0.80, ProductType.Drink),
            new Product("Soup", 3, ProductType.Dish),
            new Product("Lemon Chicken", 12.80, ProductType.Dish),
            new Product("Beef with Vegetable", 12.80, ProductType.Dish),
            new Product("BBQ Pork with Vegetable", 12.80, ProductType.Dish),
            new Product("Grilled Salmon", 20.00, ProductType.Dish)
        };
    }

    public override object InitializeLifetimeService()
    {
        return null;
    }

    private static uint GetNextOrderId()
    {
        return (uint) Interlocked.Increment(ref _nextOrderId);
    }

    public List<Order> GetListOfOrders()
    {
        Console.WriteLine("GetListOfOrders() called.");
        return _orderList;
    }

    public List<Table> GetListOfTables()
    {
        Console.WriteLine("GetListOfTables() called.");
        return _tableList;
    }

    public List<Product> GetListOfProducts()
    {
        Console.WriteLine("GetListOfProducts() called.");
        return _productList;
    }

    public List<Order> ConsultTable(uint tableId)
    {
        return _orderList.FindAll(order => order.TableId == tableId && order.State != OrderState.Paid);
    }

    public void AddOrder(uint tableId, uint productId, uint quantity)
    {
        Product productRequested = _productList.Find(product => product.Id == productId);
        Order ord = new Order(GetNextOrderId(), productRequested, quantity, tableId);
        AddOrder(ord);
    }

    private void AddOrder(Order order)
    {
        Table table = _tableList.Find(tab => tab.Id == order.TableId);
        if (table.Availability)
            ChangeAvailabilityTable(table);

        _orderList.Add(order);
        NotifyClients(OperationOrderEvent, Operation.New, order);
    }

    public void ChangeStatusOrder(uint orderId)
    {
        Order order = _orderList.Find(ord => ord.Id == orderId);
        order.State++; //TODO: Check limits like if Ready cannot turn to InPreparation
        NotifyClients(OperationOrderEvent, Operation.Change, order);
    }

    public void ChangeAvailabilityTable(uint tableId)
    {
        Table table = _tableList.Find(tab => tab.Id == tableId);
        ChangeAvailabilityTable(table);
    }

    private void ChangeAvailabilityTable(Table table)
    {
        table.ChangeAvailability();
        NotifyClients(OperationTableEvent, Operation.Change, table);
    }

    public bool DoPayment(uint tableId)
    {
        List<Order> ordersToPay = ConsultTable(tableId);

        if (ordersToPay.Count == 0 || !CheckIfAllOrdersAreDelivered(ordersToPay))
            return false;

        //Change State of each order to Paid
        ordersToPay.ForEach(order =>
        {
            order.State = OrderState.Paid;
            NotifyClients(OperationOrderEvent, Operation.Change, order);
        });

        //Change Availability of Table to True
        ChangeAvailabilityTable(tableId);

        //Print the Invoice
        NotifyClients(PrintEvent, Operation.New, new Invoice(tableId, ordersToPay));

        //Delete ordersPaid
        _orderList.RemoveAll(ordersToPay.Contains); //TODO: Notify Clients??

        return true;
    }

    private static bool CheckIfAllOrdersAreDelivered(List<Order> ordersToPay)
    {
        return !ordersToPay.Exists(order => order.State != OrderState.Delivered);
    }

    private static void NotifyClients<T>(OperationDelegate<T> operationDelegate, Operation op, T obj)
    {
        if (operationDelegate == null) return;

        Delegate[] invkList = operationDelegate.GetInvocationList();

        foreach (OperationDelegate<T> handler in invkList)
        {
            new Thread(() =>
            {
                try
                {
                    handler(op, obj);
                    Console.WriteLine("Invoking event handler");
                }
                catch (Exception)
                {
                    operationDelegate -= handler;
                    Console.WriteLine("Exception: Removed an event handler");
                }
            }).Start();
        }
    }
}