using PusherClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @interface
{
    class PusherController
    {
        private Pusher _pusher;
        private Channel _channel;
        public delegate void OperationDelegate(dynamic data);

        public PusherController(String request, OperationDelegate update)
        {
            _pusher = new Pusher(Utils.PusherKey, new PusherOptions
            {
                Cluster = "eu"
            });
            _pusher.Error += _pusher_Error;

            // Setup private channel
            _channel = _pusher.Subscribe("store");

            // Inline binding!
            _channel.Bind(request, data =>
            {
                update(data);
            });

            _pusher.Connect();
        }

        public void disconnect()
        {
            _pusher.Disconnect();
        }

        private static void _pusher_Error(object sender, PusherException error)
        {
            Console.WriteLine("Pusher Error: " + error);
        }

    }
}
