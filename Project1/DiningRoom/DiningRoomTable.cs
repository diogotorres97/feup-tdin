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
        private OperationEventRepeater<Order> _evRepeater;

        private delegate ListViewItem LvAddDelegate(ListViewItem lvItem);

        public DiningRoomTable(String tableName, DiningRoomController controller)
        {
            InitializeComponent();
            this._diningRoomController = controller;
            this.tableID= Convert.ToUInt32(tableName.Substring(5));

            _evRepeater = new OperationEventRepeater<Order>();
            _evRepeater.OperationEvent += DoAlterations;
            _diningRoomController.AddOrderAlterEvent(_evRepeater.Repeater);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

        public void DoAlterations(Operation op, Order order)
        {
            LvAddDelegate lvAdd;
            //ChCommDelegate chComm;

            switch (op) {
                case Operation.New:
                    lvAdd = itemListView.Items.Add;
                    ListViewItem lvItem = new ListViewItem(new[] { order.Id.ToString(), order.Product.Description.ToString(), order.Product.Price.ToString(), order.Quantity.ToString(), order.State.ToString() });
                    BeginInvoke(lvAdd, lvItem);
                    break;
                /*case Operation.Change:
                    chComm = ChangeAnOrder;
                    BeginInvoke(chComm, order);
                    break;*/
            }
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
            List<Order> orders = _diningRoomController.Orders;

            foreach (Order it in orders) {
                ListViewItem lvItem = new ListViewItem(new[] { it.Id.ToString(), it.Product.Description.ToString(), it.Product.Price.ToString(), it.Quantity.ToString(), it.State.ToString() });
                itemListView.Items.Add(lvItem);
            }
        }

        private void DiningRoomTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            _diningRoomController.RemoveOrderAlterEvent(_evRepeater.Repeater);
            _evRepeater.OperationEvent -= DoAlterations;
        }

     
    }
}
