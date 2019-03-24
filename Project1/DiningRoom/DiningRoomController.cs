public class DiningRoomController : AbstractController
{
    public DiningRoomController() : base("DiningRoom.exe.config")
    {
    }

    public void AddOrder(uint tableId, uint productId, uint quantity)
    {
        _restaurantServer.AddOrder(tableId, productId, quantity);
    }
}