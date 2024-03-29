using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;

public class AbstractController
{
    protected IRestaurantSingleton RestaurantServer;

    public List<Order> Orders { get; }
    public List<Product> Products { get; }
    public List<Table> Tables { get; }

    protected AbstractController(string remotingConfiguration)
    {
        RemotingConfiguration.Configure(remotingConfiguration, false);
        RestaurantServer = (IRestaurantSingleton) RemoteNew.New(typeof(IRestaurantSingleton));
        Orders = RestaurantServer.GetListOfOrders();
        Products = RestaurantServer.GetListOfProducts();
        Tables = RestaurantServer.GetListOfTables();
    }

    public List<Order> ConsultTable(uint tableId)
    {
        return RestaurantServer.ConsultTable(tableId);
    }

    public void ChangeStatusOrder(uint orderId)
    {
        RestaurantServer.ChangeStatusOrder(orderId);
    }

    public void AddOrderAlterEvent(OperationDelegate<Order> operationEvent)
    {
        RestaurantServer.OperationOrderEvent += operationEvent;
    }

    public void RemoveOrderAlterEvent(OperationDelegate<Order> operationEvent)
    {
        RestaurantServer.OperationOrderEvent -= operationEvent;
    }

    public void AddTableAlterEvent(OperationDelegate<Table> operationTableEvent)
    {
        RestaurantServer.OperationTableEvent += operationTableEvent;
    }

    public void RemoveTableAlterEvent(OperationDelegate<Table> operationTableEvent)
    {
        RestaurantServer.OperationTableEvent -= operationTableEvent;
    }
}

/* Mechanism for instantiating a remote object through its interface, using the config file */

public static class RemoteNew
{
    private static Hashtable _types;

    private static void InitTypeTable()
    {
        _types = new Hashtable();
        foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
            _types.Add(entry.ObjectType, entry);
    }

    public static object New(Type type)
    {
        if (_types == null)
            InitTypeTable();
        WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry) _types?[type];
        if (entry == null)
            throw new RemotingException("Type not found!");
        return RemotingServices.Connect(type, entry.ObjectUrl);
    }
}