using System;
using System.Collections.Generic;
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
        private delegate void ChangeProductQuantityDelegate(Order order, float quantity);

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
                
            if (_statisticsController.AmountByDay.ContainsKey(order.Date.ToShortDateString())) {
                _statisticsController.AmountByDay[order.Date.ToShortDateString()] += amount;
            } else {
                _statisticsController.AmountByDay.TryAdd(order.Date.ToShortDateString(), amount);
            }

            /*_statisticsController.TotalSumOfDay += order.Quantity * order.Product.Price;
            txtBoxTotalAmount.Text = _statisticsController.TotalSumOfDay + "";*/

            float productQuantity = order.Quantity;
            if (_statisticsController.ProductQuantity.ContainsKey(order.Product.Id))
            {
                productQuantity += _statisticsController.ProductQuantity[order.Product.Id];
                _statisticsController.ProductQuantity[order.Product.Id] = productQuantity;

                ChangeProductQuantityDelegate change = ChangeProductQuantity;
                BeginInvoke(change, order, productQuantity);

                
            }
            else
            {
                _statisticsController.ProductQuantity.TryAdd(order.Product.Id, productQuantity);
                LvAddDelegate lvAdd = productsListView.Items.Add;
                ListViewItem lvItem = new ListViewItem(new[]
                {
                    order.Product.Id.ToString(), order.Product.Type.ToString(), order.Product.Description,
                    order.Product.Price.ToString(), productQuantity.ToString()
                });
                BeginInvoke(lvAdd, lvItem);
            }
        }

        private void ChangeProductQuantity(Order order, float quantity)
        {
            foreach (ListViewItem lvI in productsListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == order.Product.Id)
                    lvI.SubItems[4] = new ListViewItem.ListViewSubItem(lvI, quantity.ToString());
        }

        private void DoTableAlterations(Operation op, Table table)
        {
            if (!table.Availability) return;

            _statisticsController.TotalInvoices++;
            SetTextCallback d = new SetTextCallback(setTotalInvoices);
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
            using (AmountByDayDialog form = new AmountByDayDialog(_statisticsController.AmountByDay)) {
                if (form.ShowDialog() == DialogResult.OK) {
                    
                }
            }
        }

        private void setTotalInvoices()
        {
            txtBoxNumInvoices.Text = _statisticsController.TotalInvoices + "";
        }

        
    }
}