using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;

public class BarKitchenController
{
    private IRestaurantSingleton _restaurantServer;

    private ProductType _productType;
    private List<Order> Orders { get; }

    public BarKitchenController(ProductType productType)
    {
        _productType = productType;
        RemotingConfiguration.Configure("BarKitchen.exe.config", false);
        _restaurantServer = (IRestaurantSingleton) RemoteNew.New(typeof(IRestaurantSingleton));
        Orders = _restaurantServer.GetListOfOrders();
    }

    public List<Order> getListOfOrdersByState(OrderState orderState)
    {
        return Orders.FindAll(order => order.State == orderState && order.Product.Type == _productType);
    }

    public void ChangeStatusOrder(uint orderId)
    {
        _restaurantServer.ChangeStatusOrder(orderId);
    }

    public void AddAlterEvent(OperationDelegate<Order> operationOrderEvent)
    {
        _restaurantServer.AlterOrderEvent += operationOrderEvent;
    }

    public void RemoveAlterEvent(OperationDelegate<Order> operationOrderEvent)
    {
        _restaurantServer.AlterOrderEvent -= operationOrderEvent;
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