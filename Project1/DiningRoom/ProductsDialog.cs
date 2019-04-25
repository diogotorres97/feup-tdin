using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DiningRoom
{
    public partial class ProductsDialog : Form
    {
        public Product SelectedProduct { get; set; }
        public uint Quantity { get; set; }

        private List<Product> _products;

        public ProductsDialog(List<Product> products)
        {
            InitializeComponent();
            _products = products;
            InitializeProductsList();
        }

        private void InitializeProductsList()
        {
            _products.ForEach(product => cmbBoxProducts.Items.Add(product.Description));

            cmbBoxProducts.SelectedIndex = 0;
        }

        private void cmbBoxProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = cmbBoxProducts.SelectedIndex;

            SelectedProduct = _products[selectedIndex];

            txtBoxPrice.Text = SelectedProduct.Price + "€";

            if (numQuantity.Value != 0)
                btnConfirm.Enabled = true;
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            btnConfirm.Enabled = numQuantity.Value != 0;

            Quantity = Convert.ToUInt32(numQuantity.Value);
        }
    }
}