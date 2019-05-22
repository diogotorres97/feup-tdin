﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace @interface
{
    public partial class AllOrdersWindow : Form
{
        public delegate void OperationDelegate(dynamic data);
        public AllOrdersWindow()
    {
        InitializeComponent();
        LoadOrders();
        
        PusherController pusherCreateOrder = new PusherController("create_order", createOrderDelegate);
        PusherController pusherUpdateOrder = new PusherController("update_order", updateOrderDelegate);
    }
    private void LoadOrders()
    {
        IRestResponse response = Utils.ExecuteRequest(Utils.Orders, Method.GET, "", "");
        if (response.StatusCode != HttpStatusCode.OK)
        {
            MessageBox.Show(response.Content, "Fetch Orders Failed", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }
        else
        {
            List<Order> orderList = (List<Order>)JsonConvert.DeserializeObject(response.Content, typeof(List<Order>));

            foreach (Order order in orderList)
            {
                ListViewItem lvItem = new ListViewItem(new[]
                {
                        order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                    order.book.title, order.client.name
                    });
                listViewOrders.Items.Add(lvItem);
            }
        }
    }

        public void createOrderDelegate(dynamic data)
        {
            OperationDelegate del = createOrder;
            BeginInvoke(del, data);
        }

        private void createOrder(dynamic data)
        {
            Order order =
                    (Order)JsonConvert.DeserializeObject(data, typeof(Order));

            ListViewItem lvItem = new ListViewItem(new[]
            {
                order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                    order.book.title, order.client.name
            });
            listViewOrders.Items.Add(lvItem);
        }

        public void updateOrderDelegate(dynamic data)
        {
            OperationDelegate del = updateOrder;
            BeginInvoke(del, data);
        }

        public void updateOrder(dynamic data)
        {
            Order order =
                    (Order)JsonConvert.DeserializeObject(data, typeof(Order));

            foreach (ListViewItem item in listViewOrders.Items)
            {
                if (item.SubItems[0].Text == order.uuid)
                {
                    ListViewItem lvItem = new ListViewItem(new[]
                    {
                        order.uuid, order.quantity.ToString(), order.totalPrice.ToString(), order.state, order.stateDate,
                    order.book.title, order.client.name
                    });
                    listViewOrders.Items[item.Index] = lvItem;
                    break;
                }
            }
        }

    }
}