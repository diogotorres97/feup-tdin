using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;

public class PrinterController
{
    private IRestaurantSingleton _restaurantServer;

    public List<Order> Orders { get; }
    public List<Product> Products { get; }
    public List<Table> Tables { get; }

    public PrinterController()
    {
        RemotingConfiguration.Configure("Printer.exe.config", false);
        _restaurantServer = (IRestaurantSingleton) RemoteNew.New(typeof(IRestaurantSingleton));
        Orders = _restaurantServer.GetListOfOrders();
        Products = _restaurantServer.GetListOfProducts();
        Tables = _restaurantServer.GetListOfTables();
    }

    public void AddAlterEvent(AlterDelegate alterEvent)
    {
        _restaurantServer.AlterEvent += alterEvent;
    }

    public void RemoveAlterEvent(AlterDelegate alterEvent)
    {
        _restaurantServer.AlterEvent -= alterEvent;
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