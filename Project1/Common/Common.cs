using System;
using System.Collections.Generic;
using System.Threading;

[Serializable]
public class Table
{
    private static int _nextId;
    public uint Id { get; }
    public bool Availability { get; set; }

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

    public Product Product { get; set; }

    public float Quantity { get; set; }

    public OrderState State { get; set; }

    public uint TableId { get; set; }

    private DateTime Date { get; set; }

    public Order(uint id, Product product, float quantity, uint tableId)
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
               " table #" +
               TableId;
    }
}

[Serializable]
public class Product
{
    private static int _nextId;
    public uint Id { get; }
    public string Description { get; set; }
    public double Price { get; set; }
    public ProductType Type { get; set; }

    public Product(string description, double price, ProductType type)
    {
        Id = (uint) Interlocked.Increment(ref _nextId);
        Description = description;
        Price = price;
        Type = type;
    }

    // Override toString function
    public override string ToString()
    {
        return "Product #" + Id + " Description: " + Description + " Unit Price: " + Price + " € Type: " + Type;
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
} // TODO: DELIVERED??

public enum Operation
{
    New,
    Change // TODO: Join Print and Payment or create two different operations
}

public delegate void AlterDelegate<T>(Operation op, T obj);

public delegate void AlterDelegate(Operation op, Order order);

public delegate void AlterTableDelegate(Operation op, Table table);

public delegate void PrintDelegate(uint tableId, List<Order> orders);

public interface IRestaurantSingleton
{
    event AlterDelegate<Order> AlterOrderEvent;

    event AlterDelegate<Table> AlterTableEvent; 
    
//    event AlterOrderDelegate AlterOrderEvent;
//    event AlterTableDelegate AlterTableEvent;
    event PrintDelegate PrintEvent;
    List<Order> GetListOfOrders();

    List<Table> GetListOfTables();

    List<Product> GetListOfProducts();

    List<Order> ConsultTable(uint tableId);

    void AddOrder(uint tableId, uint productId, uint quantity);

    void ChangeStatusOrder(uint orderId);
    void ChangeAvailabilityTable(uint tableId);

    void DoPayment(uint tableId);
}

public class AlterEventRepeater<T> : MarshalByRefObject
{
    public event AlterDelegate<T> AlterEvent;

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public void Repeater(Operation op, T obj)
    {
        AlterEvent?.Invoke(op, obj);
    }
}

//public class AlterOrderEventRepeater : MarshalByRefObject
//{
//    public event AlterOrderDelegate AlterOrderEvent;
//
//    public override object InitializeLifetimeService()
//    {
//        return null;
//    }
//
//    public void Repeater(Operation op, Order order)
//    {
//        AlterOrderEvent?.Invoke(op, order);
//    }
//}
//
//public class AlterTableEventRepeater : MarshalByRefObject
//{
//    public event AlterTableDelegate AlterTableEvent;
//
//    public override object InitializeLifetimeService()
//    {
//        return null;
//    }
//
//    public void Repeater(Operation op, Table table)
//    {
//        AlterTableEvent?.Invoke(op, table);
//    }
//}

public class PrintEventRepeater : MarshalByRefObject
{
    public event PrintDelegate PrintEvent;

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public void Repeater(uint tableId, List<Order> orders)
    {
        PrintEvent?.Invoke(tableId, orders);
    }
}