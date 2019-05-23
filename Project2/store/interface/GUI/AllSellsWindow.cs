using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace @interface
{
    public partial class AllSellsWindow : Form
    {
        private delegate void OperationDelegate(dynamic data);

        private PusherController _pusherCreateSell;
        private PusherController _pusherUpdateSell;

        public AllSellsWindow()
        {
            InitializeComponent();
            LoadSells();

            _pusherCreateSell = new PusherController("create_sell", CreateSellDelegate);
            _pusherUpdateSell = new PusherController("update_sell", UpdateSellDelegate);
        }

        private void LoadSells()
        {
            IRestResponse response = Utils.ExecuteRequest(Utils.Sells, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Sells Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                List<Sell> sellList = (List<Sell>) JsonConvert.DeserializeObject(response.Content, typeof(List<Sell>));

                foreach (Sell sell in sellList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title,
                        sell.client.name
                    });
                    listViewSells.Items.Add(lvItem);
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
                sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title, sell.client.name
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
                        sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title,
                        sell.client.name
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
        }
    }
}