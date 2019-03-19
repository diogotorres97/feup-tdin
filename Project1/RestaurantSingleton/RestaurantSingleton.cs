﻿using System;
using System.Collections;
using System.Threading;

public class RestaurantSingleton : MarshalByRefObject, IRestaurantSingleton
{
    private ArrayList _orderList;
    private ArrayList _tableList;
    private ArrayList _productList;
    public event AlterDelegate AlterEvent;

    //TODO: Será que preciso dos id's incremental das classes aqui?

    public RestaurantSingleton()
    {
        Console.WriteLine("Constructor called.");
        _orderList = new ArrayList();
        CreateTables(10);
        CreateProducts();
    }

    private void CreateTables(uint numberOfTables)
    {
        _tableList = new ArrayList();
        for (int i = 0; i < numberOfTables; i++)
        {
            _tableList.Add(new Table());
        }
    }

    private void CreateProducts()
    {
        _productList = new ArrayList
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

    public ArrayList GetListOfOrders()
    {
        Console.WriteLine("GetListOfOrders() called.");
        return _orderList;
    }

    public ArrayList GetListOfTables()
    {
        Console.WriteLine("GetListOfTables() called.");
        return _tableList;
    }

    public ArrayList GetListOfProducts()
    {
        Console.WriteLine("GetListOfProducts() called.");
        return _productList;
    }

    public void AddOrder(Order order)
    {
        _orderList.Add(order);
        NotifyClients(Operation.New, order);
    }

    public void ChangeStatusOrder(uint orderId)
    {
        Order order = null;

        foreach (Order ord in _orderList)
        {
            if (ord.Id == orderId)
            {
                ord.State++; //TODO: Check limits like if Ready cannot turn to InPreparation
                order = ord;
                break;
            }
        }

        NotifyClients(Operation.Change, order);
    }

    private void NotifyClients(Operation op, Order order)
    {
        if (AlterEvent != null)
        {
            Delegate[] invkList = AlterEvent.GetInvocationList();

            foreach (AlterDelegate handler in invkList)
            {
                new Thread(() =>
                {
                    try
                    {
                        handler(op, order);
                        Console.WriteLine("Invoking event handler");
                    }
                    catch (Exception)
                    {
                        AlterEvent -= handler;
                        Console.WriteLine("Exception: Removed an event handler");
                    }
                }).Start();
            }
        }
    }
}