using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BarKitchen
{
    public partial class BarKitchenWindow : Form
    {
        private BarKitchenController _barKitchenController;
        private OperationEventRepeater<Order> _evRepeater;

        private delegate ListViewItem LvAddDelegate(ListViewItem lvItem);

        private delegate void ChangeStateDelegate(Order order);

        public BarKitchenWindow(BarKitchenController controller)
        {
            _barKitchenController = controller;
            InitializeComponent();
            _evRepeater = new OperationEventRepeater<Order>();
            _evRepeater.OperationEvent += DoAlterations;
            _barKitchenController.AddOrderAlterEvent(_evRepeater.Repeater);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        private void DoAlterations(Operation op, Order order)
        {
            if (order.Product.Type != _barKitchenController._productType) return;

            switch (op)
            {
                case Operation.New:
                    LvAddDelegate lvAdd = notPickedListView.Items.Add;
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        order.Id.ToString(), order.Product.Description, order.Quantity.ToString(),
                        order.State.ToString()
                    });
                    BeginInvoke(lvAdd, lvItem);
                    break;
                case Operation.Change:
                    ChangeStateDelegate changeState = ChangeAnOrder;
                    BeginInvoke(changeState, order);
                    break;
            }
        }

        private void ChangeAnOrder(Order it)
        {
            foreach (ListViewItem lvI in inPreparationListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == it.Id)
                {
                    inPreparationListView.Items.Remove(lvI);
                    break;
                }

            foreach (ListViewItem lvI in notPickedListView.Items)
                if (Convert.ToInt32(lvI.SubItems[0].Text) == it.Id)
                {
                    notPickedListView.Items.Remove(lvI);
                    lvI.SubItems[3] = new ListViewItem.ListViewSubItem(lvI, it.State.ToString());
                    inPreparationListView.Items.Add(lvI);
                    break;
                }
        }

        private void btnPrepare_Click(object sender, EventArgs e)
        {
            if (notPickedListView.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(notPickedListView.SelectedItems[0].SubItems[0].Text);
                _barKitchenController.ChangeStatusOrder((uint) id);
            }
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            if (inPreparationListView.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(inPreparationListView.SelectedItems[0].SubItems[0].Text);
                _barKitchenController.ChangeStatusOrder((uint) id);
            }
        }

        private void BarKitchenWindow_Load(object sender, EventArgs e)
        {
            List<Order> notPickedOrders = _barKitchenController.GetListOfOrdersByState(OrderState.NotPicked);
            List<Order> inPreparationOrders = _barKitchenController.GetListOfOrdersByState(OrderState.InPreparation);

            foreach (Order it in notPickedOrders)
            {
                ListViewItem lvItem = new ListViewItem(new[]
                    {it.Id.ToString(), it.Product.Description, it.Quantity.ToString(), it.State.ToString()});
                notPickedListView.Items.Add(lvItem);
            }

            foreach (Order it in inPreparationOrders)
            {
                ListViewItem lvItem = new ListViewItem(new[]
                    {it.Id.ToString(), it.Product.Description, it.Quantity.ToString(), it.State.ToString()});
                inPreparationListView.Items.Add(lvItem);
            }
        }

        private void BarKitchenWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evRepeater.OperationEvent -= DoAlterations;
            _barKitchenController.RemoveOrderAlterEvent(_evRepeater.Repeater);
        }
    }
}