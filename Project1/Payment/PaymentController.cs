public class PaymentController : AbstractController
{
    public PaymentController() : base("Payment.exe.config")
    {
    }

    public bool DoPayment(uint tableId)
    {
        return _restaurantServer.DoPayment(tableId);
    }
}