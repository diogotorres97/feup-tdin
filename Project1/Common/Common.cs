using System;
using System.Collections.Generic;
using System.Threading;

[Serializable]
public class Table
{
    private static int _nextId;
    public uint Id { get; }
    public bool Availability { get; private set; }

    public Table()
    {
        Id = (uint) Interlocked.Increment(ref _nextId);
        Availability = true;
    }

    public override string ToString()
    {
        return "[Table]: #" + Id + " Availability: " + Availability;
    }

    public void ChangeAvailability()
    {
        Availability = !Availability;
    }
}

[Serializable]
public class Order
{
    public uint Id { get; }

    public Product Product { get; }

    public double Quantity { get; }

    public OrderState State { get; set; }

    public uint TableId { get; }

    public DateTime Date { get; }

    public Order(uint id, Product product, double quantity, uint tableId)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
        State = OrderState.NotPicked;
        TableId = tableId;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return "[Order]: #" + Id + " Qty: " + Quantity + " Description: " + Product.Description + " State: " + State +
               " table #" + TableId;
    }
}

[Serializable]
public class Product
{
    private static int _nextId;
    public uint Id { get; }
    public string Description { get; }
    public double Price { get; }
    public ProductType Type { get; }

    public Product(string description, double price, ProductType type)
    {
        Id = (uint) Interlocked.Increment(ref _nextId);
        Description = description;
        Price = price;
        Type = type;
    }

    public override string ToString()
    {
        return "Product #" + Id + " Description: " + Description + " Unit Price: " + Price + " € Type: " + Type;
    }
}

[Serializable]
public class Invoice
{
    private static int _nextId;
    private uint Id { get; }

    private uint TableId { get; }

    private List<Order> Orders { get; }

    private double TotalInvoice { get; set; }

    private DateTime Date { get; }

    public Invoice(uint tableId, List<Order> orders)
    {
        Id = (uint) Interlocked.Increment(ref _nextId);
        TableId = tableId;
        Orders = orders;
        TotalInvoice = 0;
        Date = DateTime.Now;

        orders.ForEach(order => { TotalInvoice += order.Product.Price * order.Quantity; });
    }

    public override string ToString()
    {
        string printedInvoice = "[Invoice]: " + TableId + "| Date: " + Date.ToString("G") + "\n";

        Orders.ForEach(order => { printedInvoice += order.ToString() + "\n"; });

        printedInvoice += "[TotalInvoice]: " + TotalInvoice;

        return printedInvoice;
    }
}

public enum ProductType
{
    Drink,
    Dish
}

public enum OrderState
{
    NotPicked,
    InPreparation,
    Ready,
    Delivered,
    Paid
}

public enum Operation
{
    New,
    Change,
    Remove
}

public delegate void OperationDelegate<in T>(Operation op, T obj);

public interface IRestaurantSingleton
{
    event OperationDelegate<Order> OperationOrderEvent;
    event OperationDelegate<Table> OperationTableEvent;
    event OperationDelegate<Invoice> PrintEvent;
    List<Order> GetListOfOrders();
    List<Table> GetListOfTables();
    List<Product> GetListOfProducts();
    List<Invoice> GetListOfInvoices();
    List<Order> ConsultTable(uint tableId);
    void AddOrder(uint tableId, uint productId, uint quantity);
    void ChangeStatusOrder(uint orderId);
    void ChangeAvailabilityTable(uint tableId);
    bool DoPayment(uint tableId);
}

public class OperationEventRepeater<T> : MarshalByRefObject
{
    public event OperationDelegate<T> OperationEvent;

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public void Repeater(Operation op, T obj)
    {
        OperationEvent?.Invoke(op, obj);
    }
}