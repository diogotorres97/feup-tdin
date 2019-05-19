using PusherClient;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace @interface
{
    public partial class CreateOrderDialog : Form
    {
        private static Pusher _pusher;
        private static Channel _chatChannel;
        private static PresenceChannel _presenceChannel;

        private int idClient, idBook;
        private bool isOrder;
        public CreateOrderDialog(int idClient, int idBook, bool isOrder)
        {
            InitializeComponent();
            this.idClient = idClient;
            this.idBook = idBook;
            this.isOrder = isOrder;

            InitPusher();

     
        

        }

        // Pusher Initiation / Connection
        private static void InitPusher()
        {
            _pusher = new Pusher(Utils.AppKey, new PusherOptions()
            {
                Cluster = "eu"
            });
            _pusher.Error += _pusher_Error;

            // Setup private channel
            _chatChannel = _pusher.Subscribe("store");

            // Inline binding!
            _chatChannel.Bind("print_invoice", (dynamic data) =>
            {
                Console.WriteLine("[" + data.name + "] " + data.message);
            });

            _pusher.Connect();
        }


        static void _pusher_Error(object sender, PusherException error)
        {
            Console.WriteLine("Pusher Error: " + error);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            string url;
            if(isOrder)
                url = Utils.orders;
            else
                url = Utils.sells;

            dynamic body = new ExpandoObject();
            body.quantity = Convert.ToInt32(numQuantity.Value);
            body.bookId = idBook;
            body.clientId = idClient;
            string json = JsonConvert.SerializeObject(body);
            Console.WriteLine(json);
            
            IRestResponse response = Utils.executeRequest2(url, "", Method.POST, json);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show(response.Content, "Create Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Close();
            }
        }
    }
}
