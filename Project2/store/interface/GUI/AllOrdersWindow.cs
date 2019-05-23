using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace @interface
{
    public partial class AllOrdersWindow : Form
{
        public delegate void OperationDelegate(dynamic data);
        PusherController pusherCreateOrder;
        PusherController pusherUpdateOrder;
        public AllOrdersWindow()
    {
        InitializeComponent();
        LoadOrders();

        pusherCreateOrder = new PusherController("create_order", createOrderDelegate);
        pusherUpdateOrder = new PusherController("update_order", updateOrderDelegate);
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
            List<Order> orderList = (List<Order>)JsonConvert.DeserializeObject(response.Content, typeof(List<Order>));
            foreach (Order order in orderList)
            {
                ListViewItem lvItem = new ListViewItem(new[]
                {
                        order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                    order.book.title, order.client.name
                    });
                listViewOrders.Items.Add(lvItem);
            }
        }
    }

        public void createOrderDelegate(dynamic data)
        {
            OperationDelegate del = createOrder;
            BeginInvoke(del, data);
        }

        private void createOrder(dynamic data)
        {
            Book book =  new Book((int)data.Book.id, (string)data.Book.title, (string)data.Book.author, (double)data.Book.price, (int)data.Book.stock);

            Client client = new Client((int) data.Client.id, (string)data.Client.name, (string)data.Client.address, (string)data.Client.email);

            Order order = new Order((string) data.uuid, (int) data.quantity, (double) data.totalPrice, (string) data.state, (string) data.stateDate, book, client);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                    order.book.title, order.client.name
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
                    order.book.title, order.client.name
                    });
                    listViewOrders.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

        private void Form_FormClosing(object sender, EventArgs e)
        {
            pusherCreateOrder.disconnect();
            pusherUpdateOrder.disconnect();
        }

    }
}
