using System;

class Program : MarshalByRefObject
{
    private static PrinterController printerController;
    private static AlterEventRepeater _evRepeater;

    private static void Main()
    {
        printerController = new PrinterController();
        _evRepeater = new AlterEventRepeater();
        _evRepeater.AlterEvent += DoAlterations;
        printerController.AddAlterEvent(_evRepeater.Repeater);

        Console.WriteLine("[Printer]");
        Console.WriteLine("Press Enter to terminate.");
        Console.ReadLine();
        
        _evRepeater.AlterEvent -= DoAlterations;
        printerController.RemoveAlterEvent(_evRepeater.Repeater);
    }

    /* The client is also a remote object. The Server calls remotely the AlterEvent handler  *
     * Infinite lifetime                                                                     */

    public override object InitializeLifetimeService()
    {
        return null;
    }


    /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

    public static void DoAlterations(Operation op, Order order)
    {
        Console.WriteLine("[DoAlterations]:  \"{0}\" -  \"{1}\"", op, order);
    }
}