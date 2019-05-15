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
        }

        private void LoadBooks()
        {
            IRestResponse response = Utils.executeRequest(Utils.books, "", Method.GET, "");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Books Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {
                Console.WriteLine(response.Content);
                List<Book> bookList = (List<Book>) Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content, typeof(List<Book>));
                Console.WriteLine(bookList.Count.ToString());

                foreach (Book book in bookList) {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        book.id.ToString(), book.title, book.author, book.price.ToString(), book.stock.ToString()
                    });
                    listViewBooks.Items.Add(lvItem);
                }
            }

           
        }


    }
}
