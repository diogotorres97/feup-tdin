using System;
using System.Dynamic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace @interface
{
    public partial class AddClientDialog : Form
    {
        public AddClientDialog()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string name = txtBoxName.Text;
            string address = txtBoxAddress.Text;
            string email = txtBoxEmail.Text;

            if (name.Length == 0 || address.Length == 0 || email.Length == 0)
                return;

            dynamic body = new ExpandoObject();
            body.name = name;
            body.address = address;
            body.email = email;
            string client = JsonConvert.SerializeObject(body);

            IRestResponse response = Utils.ExecuteRequest(Utils.Clients, Method.POST, "", client);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                MessageBox.Show(response.Content, "Create Client Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                Close();
            }
        }
    }
}