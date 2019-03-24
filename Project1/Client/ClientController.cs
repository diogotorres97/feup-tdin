using System;
using System.Collections;
using System.Runtime.Remoting;

public class ClientController: AbstractController
{
    public ClientController() : base("Client.exe.config")
    {
    }

    public void AddOrder(uint tableId, uint productId, uint quantity)
    {
        _restaurantServer.AddOrder(tableId, productId, quantity);
    }
}