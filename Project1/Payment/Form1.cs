﻿using System.Windows.Forms;

namespace Payment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            PaymentController paymentController = new PaymentController();
            InitializeComponent();
            paymentController.DoPayment(1);
        }
    }
}