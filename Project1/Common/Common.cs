﻿using System;
using System.Collections.Generic;
using System.Threading;

[Serializable]
public class Table
{
    private static int _nextId;
    private uint Id { get; }
    private bool Availability { get; set; }

    public Table()
    {
        Id = (uint) Interlocked.Increment(ref _nextId);
        Availability = true;
    }

    public override string ToString()
    {
        return "[Table]: #" + Id + " Availability: " + Availability;
    }
}

[Serializable]
public class Order
{
    public uint Id { get; }

    public Product Product { get; set; }

    private float Quantity { get; set; }

    public OrderState State { get; set; }

    private bool PaymentDone { get; set; } //TODO: Check if it will be necessary!!!! 

    private uint TableId { get; set; }

    private DateTime Date { get; set; }

    public Order(uint id, Product product, float quantity, uint tableId)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
        State = OrderState.NotPicked;
        PaymentDone = false;
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
    private double Price { get; set; }

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

public delegate void AlterDelegate(Operation op, Order order);

public interface IRestaurantSingleton
{
    event AlterDelegate AlterEvent;

    uint GetNextOrderId();
    List<Order> GetListOfOrders();

    List<Table> GetListOfTables();

    List<Product> GetListOfProducts();

    void AddOrder(Order order);
    void ChangeStatusOrder(uint orderId);
}

public class AlterEventRepeater : MarshalByRefObject
{
    public event AlterDelegate AlterEvent;

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public void Repeater(Operation op, Order order)
    {
        AlterEvent?.Invoke(op, order);
    }
}