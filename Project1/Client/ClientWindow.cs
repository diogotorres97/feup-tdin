using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Windows.Forms;

public partial class ClientWindow : Form
{
    IRestaurantSingleton _restaurantServer;
    AlterEventRepeater evRepeater;
    ArrayList items;

    delegate ListViewItem LVAddDelegate(ListViewItem lvItem);

    delegate void ChCommDelegate(Order order);

    public ClientWindow()
    {
        RemotingConfiguration.Configure("Client.exe.config", false);
        InitializeComponent();
        _restaurantServer = (IRestaurantSingleton) RemoteNew.New(typeof(IRestaurantSingleton));
        items = _restaurantServer.GetListOfOrders();
        evRepeater = new AlterEventRepeater();
        evRepeater.AlterEvent += new AlterDelegate(DoAlterations);
        _restaurantServer.AlterEvent += new AlterDelegate(evRepeater.Repeater);
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
                chComm = new ChCommDelegate(ChangeAItem);
                BeginInvoke(chComm, new object[] {order});
                break;
        }
    }

    private void ChangeAItem(Order it)
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
        foreach (Order it in items)
        {
            ListViewItem lvItem = new ListViewItem(new string[] {it.Id.ToString(), it.State.ToString()});
            itemListView.Items.Add(lvItem);
        }
    }

    private void ClientWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _restaurantServer.AlterEvent -= new AlterDelegate(evRepeater.Repeater);
        evRepeater.AlterEvent -= new AlterDelegate(DoAlterations);
    }

    private void changeCommentButton_Click(object sender, EventArgs e)
    {
        if (itemListView.SelectedItems.Count > 0)
        {
            int type = Convert.ToInt32(itemListView.SelectedItems[0].SubItems[0].Text);
            CommentDlg commDlg = new CommentDlg();
            if (commDlg.ShowDialog(this) == DialogResult.OK)
                _restaurantServer.ChangeStatusOrder(type);
        }
    }

    private void addItemButton_Click(object sender, EventArgs e)
    {
        if (nameTB.Text == "")
        {
            MessageBox.Show("Name need values!", "Insufficient data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Order it = new Order(new Product("",0,ProductType.Drink),1,1 );
        _restaurantServer.AddOrder(it);
        nameTB.Text = "";
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}

/* Mechanism for instanciating a remote object through its interface, using the config file */

class RemoteNew
{
    private static Hashtable types = null;

    private static void InitTypeTable()
    {
        types = new Hashtable();
        foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
            types.Add(entry.ObjectType, entry);
    }

    public static object New(Type type)
    {
        if (types == null)
            InitTypeTable();
        WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry) types[type];
        if (entry == null)
            throw new RemotingException("Type not found!");
        return RemotingServices.Connect(type, entry.ObjectUrl);
    }
}