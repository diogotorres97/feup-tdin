using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace @interface
{
    public partial class ClientWindow : Form
    {
        private int clientID;
        public ClientWindow(int cliendID)
        {
            InitializeComponent();
            this.clientID = cliendID;
            FillFields();
            Console.Write(cliendID.ToString());
        }

        private void FillFields()
        {
            IRestResponse response = Utils.executeRequest(Utils.clients, clientID.ToString(), Method.GET, "");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Client Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Console.WriteLine(response.Content);
                Client client = (Client)Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content, typeof(Client));
                Console.WriteLine(client.ToString());
                LoadInfos(client);
                LoadOrders(client);
                LoadSells(client);
            }
        }

        private void LoadInfos(Client client)
        {
            txtBoxID.Text = client.id.ToString();
            txtBoxName.Text = client.name.ToString();
            txtBoxEmail.Text = client.email.ToString();
            txtBoxAddress.Text = client.address.ToString();
        }

        private void LoadOrders(Client client)
        {
            foreach (Order order in client.orders)
            {
                ListViewItem lvItem = new ListViewItem(new[]
                    {
                        order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate, order.book.title    
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

