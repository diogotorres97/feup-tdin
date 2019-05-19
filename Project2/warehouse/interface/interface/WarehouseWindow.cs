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
    public partial class WarehouseWindow : Form
    {
        public WarehouseWindow()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            IRestResponse response = Utils.executeRequest(Utils.requests, "", Method.GET, "");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Fetch Requests Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                List<Request> requestList = (List<Request>)Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content, typeof(List<Request>));

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
            if(listViewRequests.SelectedItems.Count > 0)
            {
                IRestResponse response = Utils.executeRequestRequest(Utils.requests, listViewRequests.SelectedItems[0].SubItems[0].Text, Method.PUT, "");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(response.Content, "Send Stock Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
