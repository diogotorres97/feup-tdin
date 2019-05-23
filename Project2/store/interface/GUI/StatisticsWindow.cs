using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace @interface
{
    public partial class StatisticsWindow : Form
    {
        public StatisticsWindow()
        {
            InitializeComponent();
            LoadTopBooks();
            LoadTotalSales();
        }

        private void LoadTopBooks()
        {
            const string url = Utils.Statistics + "/topBooks";
            IRestResponse response = Utils.ExecuteRequest(url, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Top Books Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                List<TopBooks> topBooksList =
                    (List<TopBooks>) JsonConvert.DeserializeObject(response.Content, typeof(List<TopBooks>));

                foreach (TopBooks top in topBooksList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        top.book.id.ToString(), top.book.title, top.book.author, top.book.price.ToString(),
                        top.total
                    });
                    listViewBooks.Items.Add(lvItem);
                }
            }
        }

        private void LoadTotalSales()
        {
            const string url = Utils.Statistics + "/totalSales";
            IRestResponse response = Utils.ExecuteRequest(url, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Total Sales Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                TotalSales totalSales =
                    (TotalSales) JsonConvert.DeserializeObject(response.Content, typeof(TotalSales));
                txtBoxTotalSold.Text = totalSales.totalSales;
            }
        }
    }
}