public class PrinterController : AbstractController
{
    public PrinterController() : base("Printer.exe.config")
    {
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