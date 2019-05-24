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
        private delegate void OperationDelegate(dynamic data);

        private PusherController _pusherClient;
        private PusherController _pusherBook;

        public StoreWindow()
        {
            InitializeComponent();
            LoadBooks();
            LoadClients();

            _pusherClient = new PusherController("create_client", CreateClientDelegate);
            _pusherBook = new PusherController("update_book", UpdateBookDelegate);
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

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            AddClientDialog form = new AddClientDialog();
            form.Show();
        }

        private void CreateClientDelegate(dynamic data)
        {
            OperationDelegate del = CreateClient;
            BeginInvoke(del, data);
        }

        private void CreateClient(dynamic data)
        {
            Client client = new Client(data);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                client.id.ToString(), client.name
            });
            listViewClients.Items.Add(lvItem);
        }

        private void UpdateBookDelegate(dynamic data)
        {
            OperationDelegate del = UpdateBook;
            BeginInvoke(del, data);
        }

        private void UpdateBook(dynamic data)
        {
            Book book = new Book(data);

            foreach (ListViewItem item in listViewBooks.Items)
            {
                if (int.Parse(item.SubItems[0].Text) == book.id)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        book.id.ToString(), book.title, book.author, book.price.ToString(), book.stock.ToString()
                    });
                    listViewBooks.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

        private void btnAllSells_Click(object sender, EventArgs e)
        {
            AllSellsWindow form = new AllSellsWindow();
            form.Show();
        }

        private void btnAllOrders_Click(object sender, EventArgs e)
        {
            AllOrdersWindow form = new AllOrdersWindow();
            form.Show();
        }

        private void Form_FormClosing(object sender, EventArgs e)
        {
            _pusherClient.Disconnect();
            _pusherBook.Disconnect();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            StatisticsWindow form = new StatisticsWindow();
            form.Show();
        }
    }
}