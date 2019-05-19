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
        public WarehouseWindow()
        {
            InitializeComponent();
            LoadRequests();
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

        private void btnShip_Click(object sender, EventArgs e)
        {
            if (listViewRequests.SelectedItems.Count > 0)
            {
                IRestResponse response = Utils.ExecuteRequest(Utils.Requests, Method.PUT,
                    listViewRequests.SelectedItems[0].SubItems[0].Text, "");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Content, "Send Stock Failed", MessageBoxButtons.OK,
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