using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiningRoom
{
    public partial class DiningRoomTable : Form
    {
        private uint _tableId;

        private DiningRoomController _diningRoomController;
        private OperationEventRepeater<Order> _evOrderRepeater;
        private OperationEventRepeater<Table> _evTableRepeater;

        private delegate ListViewItem LvAddDelegate(ListViewItem lvItem);

        private delegate void ChangeStateDelegate(Order order);

        private delegate void ChangeTableStateDelegate(Table table);

        public DiningRoomTable(string tableName, DiningRoomController controller)
        {
            InitializeComponent();
            _diningRoomController = controller;
            _tableId = Convert.ToUInt32(tableName.Substring(8));

            _evOrderRepeater = new OperationEventRepeater<Order>();
            _evOrderRepeater.OperationEvent += DoOrderAlterations;
            _diningRoomController.AddOrderAlterEvent(_evOrderRepeater.Repeater);

            _evTableRepeater = new OperationEventRepeater<Table>();
            _evTableRepeater.OperationEvent += DoTableAlterations;
            _diningRoomController.AddTableAlterEvent(_evTableRepeater.Repeater);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

        private void DoOrderAlterations(Operation op, Order order)
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

                    return;
                }
        }

        private void ChangeTableAvailability(Table table)
        {
            if (table.Availability)
                Close();
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            using (ProductsDialog form = new ProductsDialog(_diningRoomController.Products))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _diningRoomController.AddOrder(_tableId, form.SelectedProduct.Id, form.Quantity);
                }
            }
        }

        private void DiningRoomTable_Load(object sender, EventArgs e)
        {
            List<Order> orders = _diningRoomController.ConsultTable(_tableId);

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

        private void DiningRoomTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evOrderRepeater.OperationEvent -= DoOrderAlterations;
            _diningRoomController.RemoveOrderAlterEvent(_evOrderRepeater.Repeater);

            _evTableRepeater.OperationEvent -= DoTableAlterations;
            _diningRoomController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDeliver_Click(object sender, EventArgs e)
        {
            if (itemListView.SelectedItems.Count > 0)
            {
                ListViewItem item = itemListView.SelectedItems[0];
                int id = Convert.ToInt32(item.SubItems[0].Text);
                Order order = _diningRoomController.ConsultTable(_tableId).Find(ord => ord.Id == id);
                if (order.State == OrderState.Ready)
                    _diningRoomController.ChangeStatusOrder((uint) id);
            }
        }
    }
}