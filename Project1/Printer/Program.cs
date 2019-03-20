﻿using System;

class Program
{
    private static PrinterController _printerController;
    private static AlterOrderEventRepeater _evOrderRepeater;
    private static AlterTableEventRepeater _evTableRepeater;

    private static void Main()
    {
        _printerController = new PrinterController();
        _evOrderRepeater = new AlterOrderEventRepeater();
        _evOrderRepeater.AlterOrderEvent += DoOrderAlterations;
        _printerController.AddOrderAlterEvent(_evOrderRepeater.Repeater);
        
        _evTableRepeater = new AlterTableEventRepeater();
        _evTableRepeater.AlterTableEvent += DoTableAlterations;
        _printerController.AddTableAlterEvent(_evTableRepeater.Repeater);

        Console.WriteLine("[Printer]");
        Console.WriteLine("Press Enter to terminate.");
        Console.ReadLine();

        _evOrderRepeater.AlterOrderEvent -= DoOrderAlterations;
        _printerController.RemoveOrderAlterEvent(_evOrderRepeater.Repeater);
        
        _evTableRepeater.AlterTableEvent -= DoTableAlterations;
        _printerController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
    }

    /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

    private static void DoOrderAlterations(Operation op, Order order)
    {
        Console.WriteLine("[DoAlterations]:  \"{0}\" -  \"{1}\"", op, order);
    }
    
    private static void DoTableAlterations(Operation op, Table table)
    {
        Console.WriteLine("[DoTable]:  \"{0}\" -  \"{1}\"", op, table);
    }
}