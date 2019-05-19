using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class StoreWindow : Form
    {
        public StoreWindow()
        {
            InitializeComponent();
            LoadBooks();
            LoadClients();
        }

        private void LoadBooks()
        {
            IRestResponse response = Utils.executeRequest(Utils.books, "", Method.GET, "");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Books Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {
                List<Book> bookList = (List<Book>) Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content, typeof(List<Book>));

                foreach (Book book in bookList) {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        book.id.ToString(), book.title, book.author, book.price.ToString(), book.stock.ToString()
                    });
                    listViewBooks.Items.Add(lvItem);
                }
            }
        }

        private void LoadClients()
        {
            IRestResponse response = Utils.executeRequest(Utils.clients, "", Method.GET, "");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Clients Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                List<Client> clientList = (List<Client>)Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content, typeof(List<Client>));
                Console.WriteLine(clientList.Count.ToString());

                foreach (Client client in clientList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        client.id.ToString(), client.name
                    });
                    listViewClients.Items.Add(lvItem);
                }
            }
        }

        private void btnViewClient_Click(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count == 0)
                return;

            ClientWindow form = new ClientWindow(int.Parse(listViewClients.SelectedItems[0].SubItems[0].Text));
            form.Show();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {

        }

        private void btnSell_Click(object sender, EventArgs e)
        {

        }
    }
}
