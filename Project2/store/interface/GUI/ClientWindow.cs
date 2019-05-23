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

        private delegate void OperationDelegate(dynamic data);

        private PusherController _pusherCreateSell;
        private PusherController _pusherUpdateSell;
        private PusherController _pusherCreateOrder;
        private PusherController _pusherUpdateOrder;

        public ClientWindow(int clientId)
        {
            InitializeComponent();
            _clientId = clientId;
            FillFields();

            _pusherCreateSell = new PusherController("create_sell", CreateSellDelegate);
            _pusherUpdateSell = new PusherController("update_sell", UpdateSellDelegate);

            _pusherCreateOrder = new PusherController("create_order", CreateOrderDelegate);
            _pusherUpdateOrder = new PusherController("update_order", UpdateOrderDelegate);
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

        private void CreateOrderDelegate(dynamic data)
        {
            OperationDelegate del = CreateOrder;
            BeginInvoke(del, data);
        }

        private void CreateOrder(dynamic data)
        {
            Book book = new Book(data.Book);

            Client client = new Client(data.Client);

            Order order = new Order(data, book, client);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                order.book.title
            });
            listViewOrders.Items.Add(lvItem);
        }

        private void UpdateOrderDelegate(dynamic data)
        {
            OperationDelegate del = UpdateOrder;
            BeginInvoke(del, data);
        }

        private void UpdateOrder(dynamic data)
        {
            Book book = new Book(data.Book);

            Client client = new Client(data.Client);

            Order order = new Order(data, book, client);

            foreach (ListViewItem item in listViewOrders.Items)
            {
                if (item.SubItems[0].Text == order.uuid)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state,
                        order.stateDate,
                        order.book.title
                    });
                    listViewOrders.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

        private void CreateSellDelegate(dynamic data)
        {
            OperationDelegate del = CreateSell;
            BeginInvoke(del, data);
        }

        private void CreateSell(dynamic data)
        {
            Book book = new Book(data.Book);

            Client client = new Client(data.Client);

            Sell sell = new Sell(data, book, client);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title
            });
            listViewSells.Items.Add(lvItem);
        }

        private void UpdateSellDelegate(dynamic data)
        {
            OperationDelegate del = UpdateSell;
            BeginInvoke(del, data);
        }

        private void UpdateSell(dynamic data)
        {
            Book book = new Book(data.Book);

            Client client = new Client(data.Client);

            Sell sell = new Sell(data, book, client);

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
            _pusherCreateSell.Disconnect();
            _pusherUpdateSell.Disconnect();
            _pusherCreateOrder.Disconnect();
            _pusherUpdateOrder.Disconnect();
        }
    }
}