﻿using System;
using PusherClient;

namespace @interface
{
    internal class PusherController
    {
        private Pusher _pusher;

        public delegate void OperationDelegate(dynamic data);

        public PusherController(string request, OperationDelegate update)
        {
            _pusher = new Pusher(Utils.PusherKey, new PusherOptions
            {
                Cluster = "eu"
            });
            _pusher.Error += _pusher_Error;

            // Setup private channel
            var channel = _pusher.Subscribe("store");

            // Inline binding!
            channel.Bind(request, data => { update(data); });

            _pusher.Connect();
        }

        public void Disconnect()
        {
            _pusher.Disconnect();
        }

        private static void _pusher_Error(object sender, PusherException error)
        {
            Console.WriteLine("Pusher Error: " + error);
        }
    }
}