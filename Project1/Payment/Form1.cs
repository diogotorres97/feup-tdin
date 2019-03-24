using System;
using System.Windows.Forms;

namespace Payment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            PaymentController paymentController = new PaymentController();
            InitializeComponent();
            Console.WriteLine(paymentController.DoPayment(1) ? "Paid!" : "Not all orders are ready to pay!");
        }
    }
}