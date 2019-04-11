using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiningRoom
{
    public partial class DiningRoomTable : Form
    {
        private uint tableID;

        private DiningRoomController _diningRoomController;
        private OperationEventRepeater<Order> _evOrderRepeater;
        private OperationEventRepeater<Table> _evTableRepeater;

        private delegate ListViewItem LvAddDelegate(ListViewItem lvItem);
        private delegate void ChangeStateDelegate(Order order);

        private delegate void ChangeTableStateDelegate(Table table);

        public DiningRoomTable(String tableName, DiningRoomController controller)
        {
            InitializeComponent();
            this._diningRoomController = controller;
            this.tableID= Convert.ToUInt32(tableName.Substring(8));

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

        public void DoOrderAlterations(Operation op, Order order)
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

        private void ChangeTableAvailability(Table table)
        {
            if (table.Availability)
                this.Close();
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
          using (ProductsDialog form = new ProductsDialog(_diningRoomController.Products)) {
                if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    _diningRoomController.AddOrder(tableID, form.selectedProduct.Id, form.quantity);
                }
            }
        }

        private void DiningRoomTable_Load(object sender, EventArgs e)
        {
            List<Order> orders = _diningRoomController.ConsultTable(tableID);

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

        private void DiningRoomTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evOrderRepeater.OperationEvent -= DoOrderAlterations;
            _diningRoomController.RemoveOrderAlterEvent(_evOrderRepeater.Repeater);

            _evTableRepeater.OperationEvent -= DoTableAlterations;
            _diningRoomController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeliver_Click(object sender, EventArgs e)
        {
            if (itemListView.SelectedItems.Count > 0) {
                ListViewItem item = itemListView.SelectedItems[0];
                int id = Convert.ToInt32(item.SubItems[0].Text);
                Order order = _diningRoomController.ConsultTable(tableID).Find(ord => ord.Id == id);
                if(order.State == OrderState.Ready)
                    _diningRoomController.ChangeStatusOrder((uint)id);
            }
        }
    }
}
