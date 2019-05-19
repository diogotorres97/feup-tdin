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

        public ClientWindow(int clientId)
        {
            InitializeComponent();
            _clientId = clientId;
            FillFields();

            Console.Write(clientId.ToString());
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
                Console.WriteLine(response.Content);
                Client client = (Client) JsonConvert.DeserializeObject(response.Content, typeof(Client));
                Console.WriteLine(client.ToString());
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
    }
}