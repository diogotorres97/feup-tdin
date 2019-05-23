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
        public delegate void OperationDelegate(dynamic data);
        PusherController pusherCreateReceiveStock;
        PusherController pusherUpdateReceiveStock;
        public NotificationsWindow()
        {
            InitializeComponent();
            LoadReceiveStock();

            pusherCreateReceiveStock = new PusherController("create_receiveStock", createReceiveStockDelegate);
            pusherUpdateReceiveStock = new PusherController("update_receiveStock", updateReceiveStockDelegate);
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
                Console.WriteLine(listViewReceiveStock.SelectedItems[0].SubItems[2].Text);
                if (!listViewReceiveStock.SelectedItems[0].SubItems[2].Text.Equals(""))
                    return;

                IRestResponse response = Utils.ExecuteRequest(Utils.ReceiveStock, Method.PUT,
                    listViewReceiveStock.SelectedItems[0].SubItems[0].Text, "");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Content, "Receive Stock Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    
                }
            }
        }

        public void createReceiveStockDelegate(dynamic data)
        {
            OperationDelegate del = createReceiveStock;
            BeginInvoke(del, data);
        }

        public void createReceiveStock(dynamic data)
        {
            Console.WriteLine(data);
            Book book = new Book((int)data.Book.id, (string)data.Book.title, (string)data.Book.author, (double)data.Book.price, (int)data.Book.stock);

            ReceiveStock receiveStock = new ReceiveStock((int)data.id, (int)data.quantity, (string)data.processedDate, book);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                receiveStock.id.ToString(), receiveStock.quantity.ToString(), receiveStock.processedDate,
                        receiveStock.book.title
            });
            listViewReceiveStock.Items.Add(lvItem);
        }

        public void updateReceiveStockDelegate(dynamic data)
        {
            OperationDelegate del = updateReceiveStock;
            BeginInvoke(del, data);
        }

        public void updateReceiveStock(dynamic data)
        {
            
            Book book = new Book((int)data.Book.id, (string)data.Book.title, (string)data.Book.author, (double)data.Book.price, (int)data.Book.stock);

            ReceiveStock receiveStock = new ReceiveStock((int)data.id, (int)data.quantity, (string)data.processedDate, book);

            foreach (ListViewItem item in listViewReceiveStock.Items)
            {
                
                if (int.Parse(item.SubItems[0].Text) == receiveStock.id)
                {
                    Console.WriteLine(receiveStock.processedDate);
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
            pusherCreateReceiveStock.disconnect();
            pusherUpdateReceiveStock.disconnect();
        }
    }
}