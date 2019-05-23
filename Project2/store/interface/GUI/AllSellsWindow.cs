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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace @interface
{
    public partial class AllSellsWindow : Form
    {
        public delegate void OperationDelegate(dynamic data);
        PusherController pusherCreateSell;
        PusherController pusherUpdateSell;
        public AllSellsWindow()
        {
            InitializeComponent();
            LoadSells();

            pusherCreateSell = new PusherController("create_sell", createSellDelegate);
            pusherUpdateSell = new PusherController("update_sell", updateSellDelegate);
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
                        sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title, sell.client.name
                    });
                    listViewSells.Items.Add(lvItem);
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
                sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title, sell.client.name
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
                        sell.uuid, sell.quantity.ToString(), sell.totalPrice.ToString(), sell.book.title, sell.client.name
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
        }

    }
}
