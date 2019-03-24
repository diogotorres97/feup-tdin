using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class ClientWindow : Form
{
    private ClientController _clientController;
    private AlterEventRepeater<Order> _evRepeater;

    private delegate ListViewItem LvAddDelegate(ListViewItem lvItem);

    private delegate void ChCommDelegate(Order order);

    public ClientWindow()
    {
        _clientController = new ClientController();
        InitializeComponent();
        _evRepeater = new AlterEventRepeater<Order>();
        _evRepeater.AlterEvent += DoAlterations;
        _clientController.AddOrderAlterEvent(_evRepeater.Repeater);
    }


    /* The client is also a remote object. The Server calls remotely the AlterEvent handler  *
     * Infinite lifetime                                                                     */

    public override object InitializeLifetimeService()
    {
        return null;
    }


    /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

    public void DoAlterations(Operation op, Order order)
    {
        LvAddDelegate lvAdd;
        ChCommDelegate chComm;

        switch (op)
        {
            case Operation.New:
                lvAdd = itemListView.Items.Add;
                ListViewItem lvItem = new ListViewItem(new[] {order.Id.ToString(), order.State.ToString()});
                BeginInvoke(lvAdd, lvItem);
                break;
            case Operation.Change:
                chComm = ChangeAnOrder;
                BeginInvoke(chComm, order);
                break;
        }
    }

    private void ChangeAnOrder(Order it)
    {
        foreach (ListViewItem lvI in itemListView.Items)
            if (Convert.ToInt32(lvI.SubItems[0].Text) == it.Id)
            {
                lvI.SubItems[2].Text = it.State.ToString();
                break;
            }
    }

    /* Client interface event handlers */

    private void ClientWindow_Load(object sender, EventArgs e)
    {
        List<Order> orders = _clientController.Orders;

        foreach (Order it in orders)
        {
            ListViewItem lvItem = new ListViewItem(new[] {it.Id.ToString(), it.State.ToString()});
            itemListView.Items.Add(lvItem);
        }
    }

    private void ClientWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _clientController.RemoveOrderAlterEvent(_evRepeater.Repeater);
        _evRepeater.AlterEvent -= DoAlterations;
    }

    private void changeCommentButton_Click(object sender, EventArgs e)
    {
        if (itemListView.SelectedItems.Count > 0)
        {
            int type = Convert.ToInt32(itemListView.SelectedItems[0].SubItems[0].Text);
            CommentDlg commDlg = new CommentDlg();
            if (commDlg.ShowDialog(this) == DialogResult.OK)
                _clientController.ChangeStatusOrder((uint) type);
            
            _clientController.ChangeAvailabilityTable(1);
            _clientController.ChangeAvailabilityTable(3);
        }
    }

    private void addItemButton_Click(object sender, EventArgs e)
    {
        if (nameTB.Text == "")
        {
            MessageBox.Show("Name need values!", "Insufficient data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        _clientController.AddOrder(1,1,5);
        nameTB.Text = "";
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}