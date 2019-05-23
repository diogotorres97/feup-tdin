using System;
using System.Dynamic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace @interface
{
    public partial class CreateOrderDialog : Form
    {
        private int _clientId, _bookId;
        private bool _isOrder;

        public CreateOrderDialog(int clientId, int bookId, bool isOrder)
        {
            InitializeComponent();
            _clientId = clientId;
            _bookId = bookId;
            _isOrder = isOrder;
        }

        private void CreateOrderDialog_Load(object sender, EventArgs e)
        {
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var url = _isOrder ? Utils.Orders : Utils.Sells;

            dynamic body = new ExpandoObject();
            body.quantity = Convert.ToInt32(numQuantity.Value);
            body.bookId = _bookId;
            body.clientId = _clientId;
            string order = JsonConvert.SerializeObject(body);

            IRestResponse response = Utils.ExecutePostRequest(url, order);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                MessageBox.Show(response.Content, "Create Order Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                Close();
            }
        }
    }
}