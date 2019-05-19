using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

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
            IRestResponse response = Utils.ExecuteRequest(Utils.Books, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Books Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                List<Book> bookList = (List<Book>) JsonConvert.DeserializeObject(response.Content, typeof(List<Book>));

                foreach (Book book in bookList)
                {
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
            IRestResponse response = Utils.ExecuteRequest(Utils.Clients, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Clients Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                List<Client> clientList =
                    (List<Client>) JsonConvert.DeserializeObject(response.Content, typeof(List<Client>));
                
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
            if (listViewClients.SelectedItems.Count > 0 && listViewBooks.SelectedItems.Count > 0)
            {
                CreateOrderDialog form = new CreateOrderDialog(
                    int.Parse(listViewClients.SelectedItems[0].SubItems[0].Text),
                    int.Parse(listViewBooks.SelectedItems[0].SubItems[0].Text), true);
                form.Show();
            }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count > 0 && listViewBooks.SelectedItems.Count > 0)
            {
                CreateOrderDialog form = new CreateOrderDialog(
                    int.Parse(listViewClients.SelectedItems[0].SubItems[0].Text),
                    int.Parse(listViewBooks.SelectedItems[0].SubItems[0].Text), false);
                form.Show();
            }
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            NotificationsWindow form = new NotificationsWindow();
            form.Show();
        }
    }
}