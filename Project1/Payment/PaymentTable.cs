using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Payment
{
    public partial class PaymentTable : Form
    {
        private uint _tableId;

        private PaymentController _paymentController;
        private OperationEventRepeater<Order> _evRepeater;
        private OperationEventRepeater<Table> _evTableRepeater;

        private delegate ListViewItem LvAddDelegate(ListViewItem lvItem);

        private delegate void ChangeStateDelegate(Order order);

        private delegate void ChangeTableStateDelegate(Table table);

        public PaymentTable(string tableName, PaymentController controller)
        {
            InitializeComponent();
            _paymentController = controller;
            _tableId = Convert.ToUInt32(tableName.Substring(8));

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

        private void DoAlterations(Operation op, Order order)
        {
            switch (op)
            {
                case Operation.New:
                    LvAddDelegate lvAdd = itemListView.Items.Add;
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        order.Id.ToString(), order.Product.Description, order.Product.Price.ToString(),
                        order.Quantity.ToString(), order.State.ToString()
                    });
                    lvItem.BackColor = Color.LightSalmon;
                    BeginInvoke(lvAdd, lvItem);
                    break;
                case Operation.Change:
                    ChangeStateDelegate changeState = ChangeAnOrder;
                    BeginInvoke(changeState, order);
                    break;
                case Operation.Remove:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
        }

        private void DoTableAlterations(Operation op, Table table)
        {
            ChangeTableStateDelegate changeState = ChangeTableAvailability;
            BeginInvoke(changeState, table);
        }

        private void ChangeTableAvailability(Table table)
        {
            if (table.Availability)
                Close();
        }

        private void ChangeAnOrder(Order it)
        {
            foreach (ListViewItem lvI in itemListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == it.Id)
                {
                    lvI.SubItems[4] = new ListViewItem.ListViewSubItem(lvI, it.State.ToString());
                    switch (it.State)
                    {
                        case OrderState.InPreparation:
                            lvI.BackColor = Color.Yellow;
                            break;
                        case OrderState.Ready:
                            lvI.BackColor = Color.LightGreen;
                            break;
                        case OrderState.Delivered:
                            lvI.BackColor = Color.Cyan;
                            break;
                        case OrderState.NotPicked:
                            break;
                        case OrderState.Paid:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
        }

        private void PaymentTable_Load(object sender, EventArgs e)
        {
            List<Order> orders = _paymentController.ConsultTable(_tableId);

            orders.ForEach(order =>
            {
                ListViewItem lvItem = new ListViewItem(new[]
                {
                    order.Id.ToString(), order.Product.Description, order.Product.Price.ToString(),
                    order.Quantity.ToString(), order.State.ToString()
                });

                switch (order.State)
                {
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
                    case OrderState.Paid:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                itemListView.Items.Add(lvItem);
            });
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            _paymentController.DoPayment(_tableId);
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