using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.Hide();
            using (BarKitchenWindow form = new BarKitchenWindow(_barKitchenController)) {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) {
                    this.Close();
                }
            }
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            BarKitchenController _barKitchenController = new BarKitchenController(ProductType.Dish);
            this.Hide();
            using (BarKitchenWindow form = new BarKitchenWindow(_barKitchenController)) {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) {
                    this.Close();
                }
            }
        }
    }
}
