using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Statistics
{
    public partial class Form1 : Form
    {
        private static StatisticsController _statisticsController;
        private static OperationEventRepeater<Order> _evOrderRepeater;
        private static OperationEventRepeater<Table> _evTableRepeater;

        public Form1()
        {
            InitializeComponent();
            InitializeController();
        }

        private static void InitializeController()
        {
            _statisticsController = new StatisticsController();
            _evOrderRepeater = new OperationEventRepeater<Order>();
            _evOrderRepeater.OperationEvent += DoOrderAlterations;
            _statisticsController.AddOrderAlterEvent(_evOrderRepeater.Repeater);

            _evTableRepeater = new OperationEventRepeater<Table>();
            _evTableRepeater.OperationEvent += DoTableAlterations;
            _statisticsController.AddTableAlterEvent(_evTableRepeater.Repeater);

            Console.WriteLine("[Statistics]");
            Console.WriteLine("Press Enter to terminate.");
            Console.ReadLine();

            _evOrderRepeater.OperationEvent -= DoOrderAlterations;
            _statisticsController.RemoveOrderAlterEvent(_evOrderRepeater.Repeater);

            _evTableRepeater.OperationEvent -= DoTableAlterations;
            _statisticsController.RemoveTableAlterEvent(_evTableRepeater.Repeater);

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
}