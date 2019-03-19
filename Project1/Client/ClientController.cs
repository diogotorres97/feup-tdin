using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Windows.Forms;

public class ClientController
{
    IRestaurantSingleton _restaurantServer;
    public AlterEventRepeater AlterEvent { get; set; }
    public ArrayList orders { get; }

    delegate ListViewItem LVAddDelegate(ListViewItem lvItem);

    delegate void ChCommDelegate(Order order);

    public ClientController()
    {
        RemotingConfiguration.Configure("Client.exe.config", false);
        _restaurantServer = (IRestaurantSingleton) RemoteNew.New(typeof(IRestaurantSingleton));
        orders = _restaurantServer.GetListOfOrders();
    }

    public void AddAlterEvent(AlterDelegate alterEvent)
    {
        _restaurantServer.AlterEvent += alterEvent;
    }

    public void RemoveAlterEvent(AlterDelegate alterEvent)
    {
        _restaurantServer.AlterEvent -= alterEvent;
    }

    public void ChangeStatusOrder(uint orderId)
    {
        _restaurantServer.ChangeStatusOrder(orderId);
    }

    public void AddOrder()
    {
        Order it = new Order(new Product("", 0, ProductType.Drink), 1, 1);
        _restaurantServer.AddOrder(it);
    }
}

/* Mechanism for instanciating a remote object through its interface, using the config file */

class RemoteNew
{
    private static Hashtable types = null;

    private static void InitTypeTable()
    {
        types = new Hashtable();
        foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
            types.Add(entry.ObjectType, entry);
    }

    public static object New(Type type)
    {
        if (types == null)
            InitTypeTable();
        WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry) types[type];
        if (entry == null)
            throw new RemotingException("Type not found!");
        return RemotingServices.Connect(type, entry.ObjectUrl);
    }
}