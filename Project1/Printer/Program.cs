using System;

class Program
{
    private static PrinterController _printerController;
    private static AlterEventRepeater _evRepeater;

    private static void Main()
    {
        _printerController = new PrinterController();
        _evRepeater = new AlterEventRepeater();
        _evRepeater.AlterEvent += DoAlterations;
        _printerController.AddAlterEvent(_evRepeater.Repeater);

        Console.WriteLine("[Printer]");
        Console.WriteLine("Press Enter to terminate.");
        Console.ReadLine();

        _evRepeater.AlterEvent -= DoAlterations;
        _printerController.RemoveAlterEvent(_evRepeater.Repeater);
    }

    /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

    private static void DoAlterations(Operation op, Order order)
    {
        Console.WriteLine("[DoAlterations]:  \"{0}\" -  \"{1}\"", op, order);
    }
}