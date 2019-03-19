using System;
using System.Collections;
using System.Windows.Forms;

public partial class ClientWindow : Form
{
    ClientController _clientController;
    AlterEventRepeater evRepeater;

    delegate ListViewItem LVAddDelegate(ListViewItem lvItem);

    delegate void ChCommDelegate(Order order);

    public ClientWindow()
    {
        _clientController = new ClientController();
        InitializeComponent();
        evRepeater = new AlterEventRepeater();
        evRepeater.AlterEvent += new AlterDelegate(DoAlterations);
        _clientController.AddAlterEvent(new AlterDelegate(evRepeater.Repeater));
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
        LVAddDelegate lvAdd;
        ChCommDelegate chComm;

        switch (op)
        {
            case Operation.New:
                lvAdd = new LVAddDelegate(itemListView.Items.Add);
                ListViewItem lvItem = new ListViewItem(new string[] {order.Id.ToString(), order.State.ToString()});
                BeginInvoke(lvAdd, new object[] {lvItem});
                break;
            case Operation.Change:
                chComm = new ChCommDelegate(ChangeAnOrder);
                BeginInvoke(chComm, new object[] {order});
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
        ArrayList orders = _clientController.orders;

        foreach (Order it in orders)
        {
            ListViewItem lvItem = new ListViewItem(new string[] {it.Id.ToString(), it.State.ToString()});
            itemListView.Items.Add(lvItem);
        }
    }

    private void ClientWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _clientController.RemoveAlterEvent(new AlterDelegate(evRepeater.Repeater));
        evRepeater.AlterEvent -= new AlterDelegate(DoAlterations);
    }

    private void changeCommentButton_Click(object sender, EventArgs e)
    {
        if (itemListView.SelectedItems.Count > 0)
        {
            int type = Convert.ToInt32(itemListView.SelectedItems[0].SubItems[0].Text);
            CommentDlg commDlg = new CommentDlg();
            if (commDlg.ShowDialog(this) == DialogResult.OK)
                _clientController.ChangeStatusOrder((uint) type);
        }
    }

    private void addItemButton_Click(object sender, EventArgs e)
    {
        if (nameTB.Text == "")
        {
            MessageBox.Show("Name need values!", "Insufficient data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        _clientController.AddOrder();
        nameTB.Text = "";
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}