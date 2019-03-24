using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;

public class DiningRoomController
{
    private IRestaurantSingleton _restaurantServer;

    private List<Order> Orders { get; }
    public List<Product> Products { get; }
    private List<Table> Tables { get; }

    public DiningRoomController()
    {
        RemotingConfiguration.Configure("DiningRoom.exe.config", false);
        _restaurantServer = (IRestaurantSingleton) RemoteNew.New(typeof(IRestaurantSingleton));
        Orders = _restaurantServer.GetListOfOrders();
        Products = _restaurantServer.GetListOfProducts();
        Tables = _restaurantServer.GetListOfTables();
    }

    public void AddOrder(uint tableId, uint productId, uint quantity)
    {
        _restaurantServer.AddOrder(tableId, productId, quantity);
    }

    public List<Order> ConsultTable(uint tableId)
    {
        return _restaurantServer.ConsultTable(tableId);
    }

    public void ChangeStatusOrder(uint orderId) //TODO: Delete this if not implements DELIVERED State
    {
        _restaurantServer.ChangeStatusOrder(orderId);
    }

    public void AddOrderAlterEvent(AlterDelegate<Order> alterEvent)
    {
        _restaurantServer.AlterOrderEvent += alterEvent;
    }

    public void RemoveOrderAlterEvent(AlterDelegate<Order> alterEvent)
    {
        _restaurantServer.AlterOrderEvent -= alterEvent;
    }
    
    public void AddTableAlterEvent(AlterDelegate<Table> alterTableEvent)
    {
        _restaurantServer.AlterTableEvent += alterTableEvent;
    }

    public void RemoveTableAlterEvent(AlterDelegate<Table> alterTableEvent)
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