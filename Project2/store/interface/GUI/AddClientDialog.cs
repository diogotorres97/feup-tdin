using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            Console.WriteLine(client);

            IRestResponse response = Utils.ExecuteRequest(Utils.Clients, Method.POST, "", client);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                MessageBox.Show(response.Content, "Create Client Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Close();
            }

        }
}
}
