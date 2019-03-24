using System;

class Program
{
    private static PrinterController _printerController;
    private static OperationEventRepeater<Order> _evOrderRepeater;
    private static OperationEventRepeater<Table> _evTableRepeater;
    private static OperationEventRepeater<Invoice> _evPrintRepeater;

    private static void Main()
    {
        _printerController = new PrinterController();
        _evOrderRepeater = new OperationEventRepeater<Order>();
        _evOrderRepeater.OperationEvent += DoOrderAlterations;
        _printerController.AddOrderAlterEvent(_evOrderRepeater.Repeater);

        _evTableRepeater = new OperationEventRepeater<Table>();
        _evTableRepeater.OperationEvent += DoTableAlterations;
        _printerController.AddTableAlterEvent(_evTableRepeater.Repeater);

        _evPrintRepeater = new OperationEventRepeater<Invoice>();
        _evPrintRepeater.OperationEvent += PrintInvoice;
        _printerController.AddPrinterEvent(_evPrintRepeater.Repeater);

        Console.WriteLine("[Printer]");
        Console.WriteLine("Press Enter to terminate.");
        Console.ReadLine();

        _evOrderRepeater.OperationEvent -= DoOrderAlterations;
        _printerController.RemoveOrderAlterEvent(_evOrderRepeater.Repeater);

        _evTableRepeater.OperationEvent -= DoTableAlterations;
        _printerController.RemoveTableAlterEvent(_evTableRepeater.Repeater);

        _evPrintRepeater.OperationEvent -= PrintInvoice;
        _printerController.RemovePrinterEvent(_evPrintRepeater.Repeater);
    }

    /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

    private static void DoOrderAlterations(Operation op, Order order)
    {
        Console.WriteLine("[DoAlterations]:  {0} - {1}", op, order);
    }

    private static void DoTableAlterations(Operation op, Table table)
    {
        Console.WriteLine("[DoTable]:  {0} - {1}", op, table);
    }

    private static void PrintInvoice(Operation op, Invoice invoice)
    {
        Console.WriteLine(invoice.ToString());
    }
}