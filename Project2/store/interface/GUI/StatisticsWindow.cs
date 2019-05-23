using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string url = Utils.Statistics + "/topBooks";
            IRestResponse response = Utils.ExecuteRequest(url, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Top Books Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                Console.WriteLine(response.Content);
                List<TopBooks> topBooksList = (List<TopBooks>)JsonConvert.DeserializeObject(response.Content, typeof(List<TopBooks>));

                foreach (TopBooks top in topBooksList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        top._book.id.ToString(), top._book.title, top._book.author, top._book.price.ToString(), top._total
                    });
                    listViewBooks.Items.Add(lvItem);
                }
            }
        }

        public class Account
        {
            public string _totalsales { get; set; }

            public Account(string totalsales)
            {
                _totalsales = totalsales;
            }
        }

        private void LoadTotalSales()
        {
            string url = Utils.Statistics + "/totalSales";
            IRestResponse response = Utils.ExecuteRequest(url, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Total Sales Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                TotalSales totalSales = (TotalSales)JsonConvert.DeserializeObject(response.Content, typeof(TotalSales));
                txtBoxTotalSold.Text = totalSales._totalsales;
            }

        }

    }
}
