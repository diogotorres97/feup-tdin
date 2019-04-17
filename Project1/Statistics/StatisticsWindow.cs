using System;
using System.Windows.Forms;

namespace Statistics
{
    public partial class StatisticsWindow : Form
    {
        private StatisticsController _statisticsController;
        private OperationEventRepeater<Invoice> _evStatisticsRepeater;

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
            _evStatisticsRepeater = new OperationEventRepeater<Invoice>();
            _evStatisticsRepeater.OperationEvent += CalculateStatistics;
            _statisticsController.AddStatisticsEvent(_evStatisticsRepeater.Repeater);
        }

        private void InitializeStats()
        {
            txtBoxNumInvoices.Text = "0";
        }

        /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

        private void CalculateStatistics(Operation op, Invoice invoice)
        {
            invoice.Orders.ForEach(order =>
            {
                if (_statisticsController.ProductQuantity.ContainsKey(order.Product.Id))
                {
                    _statisticsController.ProductQuantity[order.Product.Id] += order.Quantity;

                    ChangeProductQuantityDelegate change = ChangeProductQuantity;
                    BeginInvoke(change,
                        order.Product.Id,
                        _statisticsController.ProductQuantity[order.Product.Id]);
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
            });

            if (_statisticsController.AmountByDay.ContainsKey(invoice.Date.ToShortDateString()))
                _statisticsController.AmountByDay[invoice.Date.ToShortDateString()] += invoice.TotalInvoice;
            else
                _statisticsController.AmountByDay.TryAdd(invoice.Date.ToShortDateString(), invoice.TotalInvoice);

            _statisticsController.TotalInvoices++;
            SetTextCallback d = SetTotalInvoices;
            BeginInvoke(d);
        }

        private void ChangeProductQuantity(uint productId, double quantity)
        {
            foreach (ListViewItem lvI in productsListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == productId)
                    lvI.SubItems[4] = new ListViewItem.ListViewSubItem(lvI, quantity.ToString());
        }

        private void StatisticsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evStatisticsRepeater.OperationEvent -= CalculateStatistics;
            _statisticsController.RemovePrinterEvent(_evStatisticsRepeater.Repeater);
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