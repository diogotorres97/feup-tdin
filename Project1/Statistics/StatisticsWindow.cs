using System;
using System.Windows.Forms;

namespace Statistics
{
    public partial class StatisticsWindow : Form
    {
        private StatisticsController _statisticsController;
        private OperationEventRepeater<Order> _evOrderRepeater;
        private OperationEventRepeater<Table> _evTableRepeater;

        private delegate void SetTextCallback();

        private delegate ListViewItem LvAddDelegate(ListViewItem lvItem);

        private delegate void ChangeProductQuantityDelegate(uint productId, double quantity);

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
        }

        /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

        private void DoOrderAlterations(Operation op, Order order)
        {
            if (order.State != OrderState.Paid) return;

            double amount = order.Quantity * order.Product.Price;
            if (_statisticsController.AmountByDay.ContainsKey(order.Date.ToShortDateString()))
            {
                _statisticsController.AmountByDay[order.Date.ToShortDateString()] += amount;
            }
            else
            {
                _statisticsController.AmountByDay.TryAdd(order.Date.ToShortDateString(), amount);
            }

            if (_statisticsController.ProductQuantity.ContainsKey(order.Product.Id))
            {
                _statisticsController.ProductQuantity[order.Product.Id] += order.Quantity;

                ChangeProductQuantityDelegate change = ChangeProductQuantity;
                BeginInvoke(change, order.Product.Id, _statisticsController.ProductQuantity[order.Product.Id]);
            }
            else
            {
                _statisticsController.ProductQuantity.TryAdd(order.Product.Id, order.Quantity);

                LvAddDelegate lvAdd = productsListView.Items.Add;
                ListViewItem lvItem = new ListViewItem(new[]
                {
                    order.Product.Id.ToString(), order.Product.Type.ToString(), order.Product.Description,
                    order.Product.Price.ToString(), order.Quantity.ToString()
                });
                BeginInvoke(lvAdd, lvItem);
            }
        }

        private void ChangeProductQuantity(uint productId, double quantity)
        {
            foreach (ListViewItem lvI in productsListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == productId)
                    lvI.SubItems[4] = new ListViewItem.ListViewSubItem(lvI, quantity.ToString());
        }

        private void DoTableAlterations(Operation op, Table table)
        {
            if (!table.Availability) return;

            _statisticsController.TotalInvoices++;
            SetTextCallback d = SetTotalInvoices;
            BeginInvoke(d);
        }

        private void StatisticsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evOrderRepeater.OperationEvent -= DoOrderAlterations;
            _statisticsController.RemoveOrderAlterEvent(_evOrderRepeater.Repeater);

            _evTableRepeater.OperationEvent -= DoTableAlterations;
            _statisticsController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
        }

        private void btnViewAmountByDay_Click(object sender, EventArgs e)
        {
            using (AmountByDayDialog form = new AmountByDayDialog(_statisticsController.AmountByDay))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void SetTotalInvoices()
        {
            txtBoxNumInvoices.Text = _statisticsController.TotalInvoices + "";
        }
    }
}