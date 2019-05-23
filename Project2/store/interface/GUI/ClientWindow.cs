using System;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace @interface
{
    public partial class ClientWindow : Form
    {
        private int _clientId;
        public delegate void OperationDelegate(dynamic data);
        PusherController pusherCreateSell;
        PusherController pusherUpdateSell;
        PusherController pusherCreateOrder;
        PusherController pusherUpdateOrder;
        public ClientWindow(int clientId)
        {
            InitializeComponent();
            _clientId = clientId;
            FillFields();

            pusherCreateSell = new PusherController("create_sell", createSellDelegate);
            pusherUpdateSell = new PusherController("update_sell", updateSellDelegate);

            pusherCreateOrder = new PusherController("create_order", createOrderDelegate);
            pusherUpdateOrder = new PusherController("update_order", updateOrderDelegate);
        }

        private void FillFields()
        {
            IRestResponse response = Utils.ExecuteRequest(Utils.Clients, Method.GET, _clientId.ToString(), "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Client Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                Client client = (Client) JsonConvert.DeserializeObject(response.Content, typeof(Client));
                LoadInfos(client);
                LoadOrders(client);
                LoadSells(client);
            }
        }

        private void LoadInfos(Client client)
        {
            txtBoxID.Text = client.id.ToString();
            txtBoxName.Text = client.name;
            txtBoxEmail.Text = client.email;
            txtBoxAddress.Text = client.address;
        }

        private void LoadOrders(Client client)
        {
            foreach (Order order in client.orders)
            {
                ListViewItem lvItem = new ListViewItem(new[]
                {
                    order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                    order.book.title
                });
                listViewOrders.Items.Add(lvItem);
            }
        }

        private void LoadSells(Client client)
        {
            foreach (Sell sell in client.sells)
            {
                ListViewItem lvItem = new ListViewItem(new[]
                {
                    sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title
                });
                listViewSells.Items.Add(lvItem);
            }
        }

        public void createOrderDelegate(dynamic data)
        {
            OperationDelegate del = createOrder;
            BeginInvoke(del, data);
        }

        private void createOrder(dynamic data)
        {
            Book book = new Book((int)data.Book.id, (string)data.Book.title, (string)data.Book.author, (double)data.Book.price, (int)data.Book.stock);

            Client client = new Client((int)data.Client.id, (string)data.Client.name, (string)data.Client.address, (string)data.Client.email);

            Order order = new Order((string)data.uuid, (int)data.quantity, (double)data.totalPrice, (string)data.state, (string)data.stateDate, book, client);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                    order.book.title
            });
            listViewOrders.Items.Add(lvItem);
        }

        public void updateOrderDelegate(dynamic data)
        {
            OperationDelegate del = updateOrder;
            BeginInvoke(del, data);
        }

        public void updateOrder(dynamic data)
        {
            Book book = new Book((int)data.Book.id, (string)data.Book.title, (string)data.Book.author, (double)data.Book.price, (int)data.Book.stock);

            Client client = new Client((int)data.Client.id, (string)data.Client.name, (string)data.Client.address, (string)data.Client.email);

            Order order = new Order((string)data.uuid, (int)data.quantity, (double)data.totalPrice, (string)data.state, (string)data.stateDate, book, client);

            foreach (ListViewItem item in listViewOrders.Items)
            {
                if (item.SubItems[0].Text == order.uuid)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                    order.book.title
                    });
                    listViewOrders.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

        public void createSellDelegate(dynamic data)
        {
            OperationDelegate del = createSell;
            BeginInvoke(del, data);
        }

        public void createSell(dynamic data)
        {
            Book book = new Book((int)data.Book.id, (string)data.Book.title, (string)data.Book.author, (double)data.Book.price, (int)data.Book.stock);

            Client client = new Client((int)data.Client.id, (string)data.Client.name, (string)data.Client.address, (string)data.Client.email);

            Sell sell = new Sell((string)data.uuid, (int)data.quantity, (double)data.totalPrice, book, client);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title
            });
            listViewSells.Items.Add(lvItem);
        }

        public void updateSellDelegate(dynamic data)
        {
            OperationDelegate del = updateSell;
            BeginInvoke(del, data);
        }

        public void updateSell(dynamic data)
        {
            Book book = new Book((int)data.Book.id, (string)data.Book.title, (string)data.Book.author, (double)data.Book.price, (int)data.Book.stock);

            Client client = new Client((int)data.Client.id, (string)data.Client.name, (string)data.Client.address, (string)data.Client.email);

            Sell sell = new Sell((string)data.uuid, (int)data.quantity, (double)data.totalPrice, book, client);

            foreach (ListViewItem item in listViewSells.Items)
            {
                if (item.SubItems[0].Text == sell.uuid)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title
                    });
                    listViewSells.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

        private void Form_FormClosing(object sender, EventArgs e)
        {
            pusherCreateSell.disconnect();
            pusherUpdateSell.disconnect();
            pusherCreateOrder.disconnect();
            pusherUpdateOrder.disconnect();
        }
    }
}