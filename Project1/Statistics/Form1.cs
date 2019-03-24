using System;
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
            if (order.State != OrderState.Paid) return;

            _statisticsController.TotalSumOfDay += order.Quantity * order.Product.Price;

            float productQuantity = order.Quantity;
            if (_statisticsController.ProductQuantity.ContainsKey(order.Id))
                productQuantity += _statisticsController.ProductQuantity[order.Id];

            _statisticsController.ProductQuantity.Add(order.Id, productQuantity);

            Console.WriteLine("[Statistics]: TotalSumOfDay: {0}", _statisticsController.TotalSumOfDay);
        }

        private static void DoTableAlterations(Operation op, Table table)
        {
            if (!table.Availability) return;

            _statisticsController.TotalInvoices++;
            Console.WriteLine("[Statistics]: TotalInvoices: {0}", _statisticsController.TotalInvoices);
        }
    }
}