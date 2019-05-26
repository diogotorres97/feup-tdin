using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace @interface
{
    public partial class AllOrdersWindow : Form
    {
        private delegate void OperationDelegate(dynamic data);

        private PusherController _pusherCreateOrder;
        private PusherController _pusherUpdateOrder;

        public AllOrdersWindow()
        {
            InitializeComponent();
            LoadOrders();

            _pusherCreateOrder = new PusherController("create_order", CreateOrderDelegate);
            _pusherUpdateOrder = new PusherController("update_order", UpdateOrderDelegate);
        }

        private void LoadOrders()
        {
            IRestResponse response = Utils.ExecuteRequest(Utils.Orders, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Orders Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                List<Order> orderList =
                    (List<Order>) JsonConvert.DeserializeObject(response.Content, typeof(List<Order>));
                foreach (Order order in orderList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state,
                        order.stateDate,
                        order.book.title, order.client.name
                    });
                    listViewOrders.Items.Add(lvItem);
                }
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
                order.book.title, order.client.name
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
                        order.book.title, order.client.name
                    });
                    listViewOrders.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

        private void Form_FormClosing(object sender, EventArgs e)
        {
            _pusherCreateOrder.Disconnect();
            _pusherUpdateOrder.Disconnect();
        }
    }
}