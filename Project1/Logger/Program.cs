using System;

class Program
{
    private static LoggerController _loggerController;
    private static AlterEventRepeater<Order> _evOrderRepeater;
    private static AlterEventRepeater<Table> _evTableRepeater;

    private static void Main()
    {
        _loggerController = new LoggerController();
        _evOrderRepeater = new AlterEventRepeater<Order>();
        _evOrderRepeater.AlterEvent += DoOrderAlterations;
        _loggerController.AddOrderAlterEvent(_evOrderRepeater.Repeater);

        _evTableRepeater = new AlterEventRepeater<Table>();
        _evTableRepeater.AlterEvent += DoTableAlterations;
        _loggerController.AddTableAlterEvent(_evTableRepeater.Repeater);

        Console.WriteLine("[Logger]");
        Console.WriteLine("Press Enter to terminate.");
        Console.ReadLine();

        _evOrderRepeater.AlterEvent -= DoOrderAlterations;
        _loggerController.RemoveOrderAlterEvent(_evOrderRepeater.Repeater);

        _evTableRepeater.AlterEvent -= DoTableAlterations;
        _loggerController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
    }

    /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

    private static void DoOrderAlterations(Operation op, Order order)
    {
        Console.WriteLine("[Logger]:  \"{0}\" -  \"{1}\"", op, order);
    }

    private static void DoTableAlterations(Operation op, Table table)
    {
        Console.WriteLine("[Logger]:  \"{0}\" -  \"{1}\"", op, table);
    }
}