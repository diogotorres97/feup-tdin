public class PrinterController : AbstractController
{
    public PrinterController() : base("Printer.exe.config")
    {
    }

    public void AddPrinterEvent(OperationDelegate<Invoice> printEvent)
    {
        RestaurantServer.PrintEvent += printEvent;
    }

    public void RemovePrinterEvent(OperationDelegate<Invoice> printEvent)
    {
        RestaurantServer.PrintEvent -= printEvent;
    }
}