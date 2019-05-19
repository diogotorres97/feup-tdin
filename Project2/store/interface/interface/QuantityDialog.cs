using PusherClient;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;


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

            string parameters = "quantity=" + numQuantity.Value.ToString() + "&bookId=" + idBook.ToString() + "&clientId=" + idClient.ToString();
            IRestResponse response = Utils.executeRequest(url, "", Method.POST, parameters);
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
