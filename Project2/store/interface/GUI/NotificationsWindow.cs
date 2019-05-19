using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

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
            IRestResponse response = Utils.ExecuteRequest(Utils.ReceiveStock, Method.GET, "", "");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Receive Stock Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                List<ReceiveStock> receiveStockList =
                    (List<ReceiveStock>) JsonConvert.DeserializeObject(response.Content, typeof(List<ReceiveStock>));

                foreach (ReceiveStock receiveStock in receiveStockList)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        receiveStock.id.ToString(), receiveStock.quantity.ToString(), receiveStock.processedDate,
                        receiveStock.book.title
                    });
                    listViewReceiveStock.Items.Add(lvItem);
                }
            }
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            if (listViewReceiveStock.SelectedItems.Count > 0)
            {
                IRestResponse response = Utils.ExecuteRequest(Utils.ReceiveStock, Method.PUT,
                    listViewReceiveStock.SelectedItems[0].SubItems[0].Text, "");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Content, "Receive Stock Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    Close();
                }
            }
        }
    }
}