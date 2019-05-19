using System;
using System.Dynamic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using PusherClient;
using RestSharp;

namespace @interface
{
    public partial class CreateOrderDialog : Form
    {
        private static Pusher _pusher;
        private static Channel _chatChannel;
        private static PresenceChannel _presenceChannel;

        private int _clientId, _bookId;
        private bool _isOrder;
        public CreateOrderDialog(int clientId, int bookId, bool isOrder)
        {
            InitializeComponent();
            _clientId = clientId;
            _bookId = bookId;
            _isOrder = isOrder;

            InitPusher();
        }

        // Pusher Initiation / Connection
        private static void InitPusher()
        {
            _pusher = new Pusher(Utils.PusherKey, new PusherOptions
            {
                Cluster = "eu"
            });
            _pusher.Error += _pusher_Error;

            // Setup private channel
            _chatChannel = _pusher.Subscribe("store");

            // Inline binding!
            _chatChannel.Bind("print_invoice", data =>
            {
                Console.WriteLine("[" + data.name + "] " + data.message);
            });

            _pusher.Connect();
        }


        private static void _pusher_Error(object sender, PusherException error)
        {
            Console.WriteLine("Pusher Error: " + error);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var url = _isOrder ? Utils.Orders : Utils.Sells;

            dynamic body = new ExpandoObject();
            body.quantity = Convert.ToInt32(numQuantity.Value);
            body.bookId = _bookId;
            body.clientId = _clientId;
            string order = JsonConvert.SerializeObject(body);
            
            Console.WriteLine(order);
            
            IRestResponse response = Utils.ExecutePostRequest(url, order);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                MessageBox.Show(response.Content, "Create Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Close();
            }
        }
    }
}