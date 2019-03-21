using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;

public class PaymentController
{
    private IRestaurantSingleton _restaurantServer;

    private List<Order> Orders { get; }
    public List<Product> Products { get; }
    private List<Table> Tables { get; }

    public PaymentController()
    {
        RemotingConfiguration.Configure("Payment.exe.config", false);
        _restaurantServer = (IRestaurantSingleton) RemoteNew.New(typeof(IRestaurantSingleton));
        Orders = _restaurantServer.GetListOfOrders();
        Products = _restaurantServer.GetListOfProducts();
        Tables = _restaurantServer.GetListOfTables();
    }

    public List<Order> ConsultTable(uint tableId)
    {
        return _restaurantServer.ConsultTable(tableId);
    }

    public void DoPayment(uint tableId)
    {
        _restaurantServer.DoPayment(tableId);
    }

    public void AddOrderAlterEvent(AlterOrderDelegate alterOrderEvent)
    {
        _restaurantServer.AlterOrderEvent += alterOrderEvent;
    }

    public void RemoveOrderAlterEvent(AlterOrderDelegate alterOrderEvent)
    {
        _restaurantServer.AlterOrderEvent -= alterOrderEvent;
    }

    public void AddTableAlterEvent(AlterTableDelegate alterTableEvent)
    {
        _restaurantServer.AlterTableEvent += alterTableEvent;
    }

    public void RemoveTableAlterEvent(AlterTableDelegate alterTableEvent)
    {
        _restaurantServer.AlterTableEvent -= alterTableEvent;
    }
}

/* Mechanism for instantiating a remote object through its interface, using the config file */

static class RemoteNew
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
        WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry) _types[type];
        if (entry == null)
            throw new RemotingException("Type not found!");
        return RemotingServices.Connect(type, entry.ObjectUrl);
    }
}