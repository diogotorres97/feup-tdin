using System;
using System.Collections;
using System.Runtime.Remoting;

public class PrinterController
{
    private IRestaurantSingleton _restaurantServer;

    public PrinterController()
    {
        RemotingConfiguration.Configure("Printer.exe.config", false);
        _restaurantServer = (IRestaurantSingleton) RemoteNew.New(typeof(IRestaurantSingleton));
    }
    public void AddPrinterEvent(OperationDelegate<Invoice> printEvent)
    {
        _restaurantServer.PrintEvent += printEvent;
    }

    public void RemovePrinterEvent(OperationDelegate<Invoice> printEvent)
    {
        _restaurantServer.PrintEvent -= printEvent;
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