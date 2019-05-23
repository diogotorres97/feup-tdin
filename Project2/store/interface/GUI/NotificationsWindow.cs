using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace @interface
{
    public partial class NotificationsWindow : Form
    {
        private delegate void OperationDelegate(dynamic data);

        private PusherController _pusherCreateReceiveStock;
        private PusherController _pusherUpdateReceiveStock;

        public NotificationsWindow()
        {
            InitializeComponent();
            LoadReceiveStock();

            _pusherCreateReceiveStock = new PusherController("create_receiveStock", CreateReceiveStockDelegate);
            _pusherUpdateReceiveStock = new PusherController("update_receiveStock", UpdateReceiveStockDelegate);
        }

        private void LoadReceiveStock()
        {
            IRestResponse response = Utils.ExecuteRequest(Utils.ReceiveStock, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Receive Stock Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                List<ReceiveStock> receiveStockList =
                    (List<ReceiveStock>) JsonConvert.DeserializeObject(response.Content, typeof(List<ReceiveStock>));

                foreach (ReceiveStock receiveStock in receiveStockList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        receiveStock.id.ToString(), receiveStock.quantity.ToString(), receiveStock.processedDate,
                        receiveStock.book.title
                    });
                    listViewReceiveStock.Items.Add(lvItem);
                }
            }
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            if (listViewReceiveStock.SelectedItems.Count > 0)
            {
                if (!listViewReceiveStock.SelectedItems[0].SubItems[2].Text.Equals(""))
                    return;

                IRestResponse response = Utils.ExecuteRequest(Utils.ReceiveStock, Method.PUT,
                    listViewReceiveStock.SelectedItems[0].SubItems[0].Text, "");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Content, "Receive Stock Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
        }

        private void CreateReceiveStockDelegate(dynamic data)
        {
            OperationDelegate del = CreateReceiveStock;
            BeginInvoke(del, data);
        }

        private void CreateReceiveStock(dynamic data)
        {
            Book book = new Book(data.Book);

            ReceiveStock receiveStock = new ReceiveStock(data, book);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                receiveStock.id.ToString(), receiveStock.quantity.ToString(), receiveStock.processedDate,
                receiveStock.book.title
            });
            listViewReceiveStock.Items.Add(lvItem);
        }

        private void UpdateReceiveStockDelegate(dynamic data)
        {
            OperationDelegate del = UpdateReceiveStock;
            BeginInvoke(del, data);
        }

        private void UpdateReceiveStock(dynamic data)
        {
            Book book = new Book(data.Book);

            ReceiveStock receiveStock = new ReceiveStock(data, book);

            foreach (ListViewItem item in listViewReceiveStock.Items)
            {
                if (int.Parse(item.SubItems[0].Text) == receiveStock.id)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        receiveStock.id.ToString(), receiveStock.quantity.ToString(), receiveStock.processedDate,
                        receiveStock.book.title
                    });
                    listViewReceiveStock.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

        private void Form_FormClosing(object sender, EventArgs e)
        {
            _pusherCreateReceiveStock.Disconnect();
            _pusherUpdateReceiveStock.Disconnect();
        }
    }
}