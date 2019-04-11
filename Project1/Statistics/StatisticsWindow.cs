using System;
using System.Windows.Forms;

namespace Statistics
{
    public partial class StatisticsWindow : Form
    {
        private StatisticsController _statisticsController;
        private OperationEventRepeater<Order> _evOrderRepeater;
        private OperationEventRepeater<Table> _evTableRepeater;


        public StatisticsWindow()
        {
            InitializeComponent();
            InitializeController();
            InitializeStats();
        }

        private void InitializeController()
        {
            _statisticsController = new StatisticsController();
            _evOrderRepeater = new OperationEventRepeater<Order>();
            _evOrderRepeater.OperationEvent += DoOrderAlterations;
            _statisticsController.AddOrderAlterEvent(_evOrderRepeater.Repeater);

            _evTableRepeater = new OperationEventRepeater<Table>();
            _evTableRepeater.OperationEvent += DoTableAlterations;
            _statisticsController.AddTableAlterEvent(_evTableRepeater.Repeater);
        }

        private void InitializeStats()
        {
            txtBoxNumInvoices.Text = "0";
            txtBoxTotalAmount.Text = "0";
        }

        /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

        private void DoOrderAlterations(Operation op, Order order)
        {
            if (order.State != OrderState.Paid) return;

            _statisticsController.TotalSumOfDay += order.Quantity * order.Product.Price;
            txtBoxTotalAmount.Text = _statisticsController.TotalSumOfDay + "";

            float productQuantity = order.Quantity;
            if (_statisticsController.ProductQuantity.ContainsKey(order.Product.Id))
            {
                productQuantity += _statisticsController.ProductQuantity[order.Product.Id];
                _statisticsController.ProductQuantity[order.Product.Id] = productQuantity;
                
                foreach (ListViewItem lvI in productsListView.Items)
                    if (Convert.ToInt32(lvI.SubItems[0].Text) == order.Product.Id)
                        lvI.SubItems[4] = new ListViewItem.ListViewSubItem(lvI, productQuantity.ToString());
            }
            else
            {
                _statisticsController.ProductQuantity.Add(order.Product.Id, productQuantity);

                ListViewItem lvItem = new ListViewItem(new[]
                {
                    order.Product.Id.ToString(), order.Product.Type.ToString(), order.Product.Description,
                    order.Product.Price.ToString(), productQuantity.ToString()
                });
                productsListView.Items.Add(lvItem);
            }
        }

        private void DoTableAlterations(Operation op, Table table)
        {
            if (!table.Availability) return;

            _statisticsController.TotalInvoices++;
            txtBoxNumInvoices.Text = _statisticsController.TotalInvoices + "";
        }

        private void StatisticsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evOrderRepeater.OperationEvent -= DoOrderAlterations;
            _statisticsController.RemoveOrderAlterEvent(_evOrderRepeater.Repeater);

            _evTableRepeater.OperationEvent -= DoTableAlterations;
            _statisticsController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
        }
    }
}