using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payment
{
    public partial class PaymentTable : Form
    {
        private uint tableID;

        private PaymentController _paymentController;
        private OperationEventRepeater<Order> _evRepeater;
        private OperationEventRepeater<Table> _evTableRepeater;

        private delegate ListViewItem LvAddDelegate(ListViewItem lvItem);
        private delegate void ChangeStateDelegate(Order order);
        private delegate void ChangeTableStateDelegate(Table table);
        public PaymentTable(String tableName, PaymentController controller)
        {
            InitializeComponent();
            this._paymentController = controller;
            this.tableID = Convert.ToUInt32(tableName.Substring(8));

            _evRepeater = new OperationEventRepeater<Order>();
            _evRepeater.OperationEvent += DoAlterations;
            _paymentController.AddOrderAlterEvent(_evRepeater.Repeater);

            _evTableRepeater = new OperationEventRepeater<Table>();
            _evTableRepeater.OperationEvent += DoTableAlterations;
            _paymentController.AddTableAlterEvent(_evTableRepeater.Repeater);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void DoAlterations(Operation op, Order order)
        {
            LvAddDelegate lvAdd;
            ChangeStateDelegate changeState;

            switch (op) {
                case Operation.New:
                    lvAdd = itemListView.Items.Add;
                    ListViewItem lvItem = new ListViewItem(new[] { order.Id.ToString(), order.Product.Description.ToString(), order.Product.Price.ToString(), order.Quantity.ToString(), order.State.ToString() });
                    lvItem.BackColor = Color.LightSalmon;
                    BeginInvoke(lvAdd, lvItem);
                    break;
                case Operation.Change:
                    changeState = ChangeAnOrder;
                    BeginInvoke(changeState, order);
                    break;

            }
        }

        private void DoTableAlterations(Operation op, Table table)
        {
            ChangeTableStateDelegate changeState = ChangeTableAvailability;
            BeginInvoke(changeState, table);
        }

        private void ChangeTableAvailability(Table table)
        {
            if(table.Availability)
                this.Close();
        }

        private void ChangeAnOrder(Order it)
        {

            foreach (ListViewItem lvI in itemListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == it.Id) {
                    lvI.SubItems[4] = new ListViewItem.ListViewSubItem(lvI, it.State.ToString());
                    switch (it.State) {
                        case OrderState.InPreparation:
                            lvI.BackColor = Color.Yellow;
                            break;
                        case OrderState.Ready:
                            lvI.BackColor = Color.LightGreen;
                            break;
                        case OrderState.Delivered:
                            lvI.BackColor = Color.Cyan;
                            break;
                    }
                    break;
                }


        }

        private void PaymentTable_Load(object sender, EventArgs e)
        {
            List<Order> orders = _paymentController.ConsultTable(tableID);

            foreach (Order it in orders) {
                ListViewItem lvItem = new ListViewItem(new[] { it.Id.ToString(), it.Product.Description.ToString(), it.Product.Price.ToString(), it.Quantity.ToString(), it.State.ToString() });
                switch (it.State) {
                    case OrderState.NotPicked:
                        lvItem.BackColor = Color.LightSalmon;
                        break;
                    case OrderState.InPreparation:
                        lvItem.BackColor = Color.Yellow;
                        break;
                    case OrderState.Ready:
                        lvItem.BackColor = Color.LightGreen;
                        break;
                    case OrderState.Delivered:
                        lvItem.BackColor = Color.Cyan;
                        break;
                }
                itemListView.Items.Add(lvItem);
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            _paymentController.DoPayment(tableID);
        }

        private void PaymentTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evRepeater.OperationEvent -= DoAlterations;
            _paymentController.RemoveOrderAlterEvent(_evRepeater.Repeater);

            _evTableRepeater.OperationEvent -= DoTableAlterations;
            _paymentController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
        }


    }
}
