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
    public partial class NotificationsWindow : Form
{
        public NotificationsWindow()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            IRestResponse response = Utils.executeRequest(Utils.receiveStock, "", Method.GET, "");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Receive Stock Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                List<ReceiveStock> receiveStockList = (List<ReceiveStock>)Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content, typeof(List<ReceiveStock>));

                foreach (ReceiveStock receiveStock in receiveStockList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        receiveStock.id.ToString(), receiveStock.quantity.ToString(), receiveStock.processedDate, receiveStock.book.title
                    });
                    listViewReceiveStock.Items.Add(lvItem);
                }
            }
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            if (listViewReceiveStock.SelectedItems.Count > 0)
            {
                IRestResponse response = Utils.executeReceiveStockRequest(Utils.receiveStock, listViewReceiveStock.SelectedItems[0].SubItems[0].Text, Method.PUT, "");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Content, "Receive Stock Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
