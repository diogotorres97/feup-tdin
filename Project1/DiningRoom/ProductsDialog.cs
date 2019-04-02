using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiningRoom
{
    public partial class ProductsDialog : Form
    {
        public Product selectedProduct { get; set; }
        public uint quantity { get; set; }

        private List<Product> products;

        public ProductsDialog(List<Product> products)
        {
            InitializeComponent();
            this.products = products;
            InitializeProductsList();
        }

        private void InitializeProductsList()
        {
            foreach (Product it in products) 
                cmbBoxProducts.Items.Add(it.Description);

            cmbBoxProducts.SelectedIndex = 0;
            
        }

        private void cmbBoxProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = cmbBoxProducts.SelectedIndex;

            selectedProduct = products[selectedIndex];

            txtBoxPrice.Text = selectedProduct.Price + "€";

            if (numQuantity.Value != 0) 
                btnConfirm.Enabled = true;

            

        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (numQuantity.Value != 0) 
                btnConfirm.Enabled = true;
            else
                btnConfirm.Enabled = false;

            quantity = Convert.ToUInt32(numQuantity.Value);
        }
    }
}
