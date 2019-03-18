using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Windows.Forms;

public partial class ClientWindow : Form
{
    IListSingleton listServer;
    AlterEventRepeater evRepeater;
    ArrayList items;

    delegate ListViewItem LVAddDelegate(ListViewItem lvItem);

    delegate void ChCommDelegate(Item item);

    public ClientWindow()
    {
        RemotingConfiguration.Configure("Client.exe.config", false);
        InitializeComponent();
        listServer = (IListSingleton) RemoteNew.New(typeof(IListSingleton));
        items = listServer.GetList();
        evRepeater = new AlterEventRepeater();
        evRepeater.alterEvent += new AlterDelegate(DoAlterations);
        listServer.alterEvent += new AlterDelegate(evRepeater.Repeater);
    }

    /* The client is also a remote object. The Server calls remotely the AlterEvent handler  *
     * Infinite lifetime                                                                     */

    public override object InitializeLifetimeService()
    {
        return null;
    }

    /* Event handler for the remote AlterEvent subscription and other auxiliary methods */

    public void DoAlterations(Operation op, Item item)
    {
        LVAddDelegate lvAdd;
        ChCommDelegate chComm;

        switch (op)
        {
            case Operation.New:
                lvAdd = new LVAddDelegate(itemListView.Items.Add);
                ListViewItem lvItem = new ListViewItem(new string[] {item.Type.ToString(), item.Name, item.Comment});
                BeginInvoke(lvAdd, new object[] {lvItem});
                break;
            case Operation.Change:
                chComm = new ChCommDelegate(ChangeAItem);
                BeginInvoke(chComm, new object[] {item});
                break;
        }
    }

    private void ChangeAItem(Item it)
    {
        foreach (ListViewItem lvI in itemListView.Items)
            if (Convert.ToInt32(lvI.SubItems[0].Text) == it.Type)
            {
                lvI.SubItems[2].Text = it.Comment;
                break;
            }
    }

    /* Client interface event handlers */

    private void ClientWindow_Load(object sender, EventArgs e)
    {
        foreach (Item it in items)
        {
            ListViewItem lvItem = new ListViewItem(new string[] {it.Type.ToString(), it.Name, it.Comment});
            itemListView.Items.Add(lvItem);
        }
    }

    private void ClientWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        listServer.alterEvent -= new AlterDelegate(evRepeater.Repeater);
        evRepeater.alterEvent -= new AlterDelegate(DoAlterations);
    }

    private void changeCommentButton_Click(object sender, EventArgs e)
    {
        if (itemListView.SelectedItems.Count > 0)
        {
            int type = Convert.ToInt32(itemListView.SelectedItems[0].SubItems[0].Text);
            CommentDlg commDlg = new CommentDlg();
            if (commDlg.ShowDialog(this) == DialogResult.OK)
                listServer.ChangeComment(type, commDlg.comment);
        }
    }

    private void addItemButton_Click(object sender, EventArgs e)
    {
        if (nameTB.Text == "")
        {
            MessageBox.Show("Name need values!", "Insufficient data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Item it = new Item(listServer.GetNewType(), nameTB.Text, null);
        listServer.AddItem(it);
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