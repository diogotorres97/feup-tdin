
using PusherClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace @interface
{
    class PusherController
    {
        private static Pusher _pusher;
        private static Channel _channel;
        public delegate void OperationDelegate(dynamic data);
        public PusherController(String request, OperationDelegate update)
        {
            _pusher = new Pusher(Utils.PusherKey, new PusherOptions
            {
                Cluster = "eu"
            });
            _pusher.Error += _pusher_Error;

            // Setup private channel
            _channel = _pusher.Subscribe("warehouse");

            // Inline binding!
            _channel.Bind(request, data =>
            {
                update(data);
            });

            _pusher.Connect();
        }

        private static void _pusher_Error(object sender, PusherException error)
        {
            Console.WriteLine("Pusher Error: " + error);
        }

    }
}
