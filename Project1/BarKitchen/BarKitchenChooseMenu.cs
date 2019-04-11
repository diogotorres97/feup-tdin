using System;
using System.Windows.Forms;

namespace BarKitchen
{
    public partial class BarKitchenChooseMenu : Form
    {
        public BarKitchenChooseMenu()
        {
            InitializeComponent();
        }

        private void btnBar_Click(object sender, EventArgs e)
        {
            BarKitchenController _barKitchenController = new BarKitchenController(ProductType.Drink);
            Hide();
            using (BarKitchenWindow form = new BarKitchenWindow(_barKitchenController))
            {
                if (form.ShowDialog() == DialogResult.Cancel)
                {
                    Close();
                }
            }
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            BarKitchenController _barKitchenController = new BarKitchenController(ProductType.Dish);
            Hide();
            using (BarKitchenWindow form = new BarKitchenWindow(_barKitchenController))
            {
                if (form.ShowDialog() == DialogResult.Cancel)
                {
                    Close();
                }
            }
        }
    }
}