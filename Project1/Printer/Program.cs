using System;

internal static class Program
{
    private static PrinterController _printerController;
    private static OperationEventRepeater<Invoice> _evPrintRepeater;

    private static void Main()
    {
        _printerController = new PrinterController();

        _evPrintRepeater = new OperationEventRepeater<Invoice>();
        _evPrintRepeater.OperationEvent += PrintInvoice;
        _printerController.AddPrinterEvent(_evPrintRepeater.Repeater);

        Console.WriteLine("[Printer]");
        Console.WriteLine("Press Enter to terminate.");
        Console.ReadLine();

        _evPrintRepeater.OperationEvent -= PrintInvoice;
        _printerController.RemovePrinterEvent(_evPrintRepeater.Repeater);
    }

    private static void PrintInvoice(Operation op, Invoice invoice)
    {
        Console.WriteLine(invoice.ToString());
    }
}