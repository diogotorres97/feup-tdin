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
        public delegate void OperationDelegate(dynamic data);
        public WarehouseWindow()
        {
            InitializeComponent();
            LoadRequests();
            LoadBooks();
            PusherController pusherBook = new PusherController("update_book", updateBookDelegate);
            PusherController pusherCreateRequest = new PusherController("create_request", createRequestDelegate);
            PusherController pusherUpdateRequest = new PusherController("update_request", updateRequestDelegate);

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
                    (List<Book>)JsonConvert.DeserializeObject(response.Content, typeof(List<Book>));

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
                Console.WriteLine(listViewRequests.SelectedItems[0].SubItems[2].Text);
                if (!listViewRequests.SelectedItems[0].SubItems[2].Text.Equals(""))
                    return;

                IRestResponse response = Utils.ExecuteRequest(Utils.Requests, Method.PUT,
                    listViewRequests.SelectedItems[0].SubItems[0].Text, "");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Content, "Send Stock Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    
                }
            }
        }

        public void updateBookDelegate(dynamic data)
        {
            OperationDelegate del = updateBook;
            BeginInvoke(del, data);
            
        }

        private void updateBook(dynamic data)
        {
            Book book =
                    data.ToObject<Book>(); ;


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
        public void updateRequestDelegate(dynamic data)
        {
            OperationDelegate del = updateRequest;
            BeginInvoke(del, data);
        }
        private void updateRequest(dynamic data)
        {
            Request request =
                    data.ToObject<Request>();

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

        public void createRequestDelegate(dynamic data)
        {
            Console.WriteLine(data);
            Request request =
                    data.ToObject<Request>(); ;

            ListViewItem lvItem = new ListViewItem(new[]
            {
                request.id.ToString(), request.quantity.ToString(), request.processedDate,
                        request.book.title
            });
            listViewRequests.Items.Add(lvItem);
        }

       

        
    }
}