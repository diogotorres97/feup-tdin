using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace @interface
{
    public partial class WarehouseWindow : Form
    {
        private delegate void OperationDelegate(dynamic data);

        private PusherController _pusherBook;
        private PusherController _pusherCreateRequest;
        private PusherController _pusherUpdateRequest;

        public WarehouseWindow()
        {
            InitializeComponent();
            LoadRequests();
            LoadBooks();
            _pusherBook = new PusherController("update_book", UpdateBookDelegate);
            _pusherCreateRequest = new PusherController("create_request", CreateRequestDelegate);
            _pusherUpdateRequest = new PusherController("update_request", UpdateRequestDelegate);
        }

        private void LoadRequests()
        {
            IRestResponse response = Utils.ExecuteRequest(Utils.Requests, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Requests Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                List<Request> requestList =
                    (List<Request>) JsonConvert.DeserializeObject(response.Content, typeof(List<Request>));

                foreach (Request request in requestList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        request.id.ToString(), request.quantity.ToString(), request.processedDate, request.book.title
                    });
                    listViewRequests.Items.Add(lvItem);
                }
            }
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
                List<Book> bookList =
                    (List<Book>) JsonConvert.DeserializeObject(response.Content, typeof(List<Book>));

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

        private void btnShip_Click(object sender, EventArgs e)
        {
            if (listViewRequests.SelectedItems.Count > 0)
            {
                if (!listViewRequests.SelectedItems[0].SubItems[2].Text.Equals(""))
                    return;

                IRestResponse response = Utils.ExecuteRequest(Utils.Requests, Method.PUT,
                    listViewRequests.SelectedItems[0].SubItems[0].Text, "");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Content, "Send Stock Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
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

        private void UpdateRequestDelegate(dynamic data)
        {
            OperationDelegate del = UpdateRequest;
            BeginInvoke(del, data);
        }

        private void UpdateRequest(dynamic data)
        {
            Book book = new Book(data.Book);

            Request request = new Request(data, book);

            foreach (ListViewItem item in listViewRequests.Items)
            {
                if (int.Parse(item.SubItems[0].Text) == request.id)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        request.id.ToString(), request.quantity.ToString(), request.processedDate,
                        request.book.title
                    });
                    listViewRequests.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

        private void CreateRequestDelegate(dynamic data)
        {
            OperationDelegate del = CreateRequest;
            BeginInvoke(del, data);
        }

        private void CreateRequest(dynamic data)
        {
            Book book = new Book(data.Book);

            Request request = new Request(data, book);

            ListViewItem lvItem = new ListViewItem(new[]
            {
                request.id.ToString(), request.quantity.ToString(), request.processedDate,
                request.book.title
            });
            listViewRequests.Items.Add(lvItem);
        }

        private void Form_FormClosing(object sender, EventArgs e)
        {
            _pusherBook.Disconnect();
            _pusherCreateRequest.Disconnect();
            _pusherUpdateRequest.Disconnect();
        }
    }
}