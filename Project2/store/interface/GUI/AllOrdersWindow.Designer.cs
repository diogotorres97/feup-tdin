namespace @interface
{
    partial class AllOrdersWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.listViewOrders = new System.Windows.Forms.ListView();
            this.orderUUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderTotalPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderStateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderBookTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "List of Orders";
            // 
            // listViewOrders
            // 
            this.listViewOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.orderUUID,
            this.orderQuantity,
            this.orderTotalPrice,
            this.orderState,
            this.orderStateDate,
            this.orderBookTitle,
            this.orderClientName});
            this.listViewOrders.FullRowSelect = true;
            this.listViewOrders.GridLines = true;
            this.listViewOrders.Location = new System.Drawing.Point(36, 214);
            this.listViewOrders.MultiSelect = false;
            this.listViewOrders.Name = "listViewOrders";
            this.listViewOrders.Size = new System.Drawing.Size(912, 244);
            this.listViewOrders.TabIndex = 26;
            this.listViewOrders.UseCompatibleStateImageBehavior = false;
            this.listViewOrders.View = System.Windows.Forms.View.Details;
            // 
            // orderUUID
            // 
            this.orderUUID.Text = "UUID";
            // 
            // orderQuantity
            // 
            this.orderQuantity.Text = "Quantity";
            // 
            // orderTotalPrice
            // 
            this.orderTotalPrice.Text = "Total Price";
            this.orderTotalPrice.Width = 79;
            // 
            // orderState
            // 
            this.orderState.Text = "State";
            // 
            // orderStateDate
            // 
            this.orderStateDate.Text = "State Date";
            this.orderStateDate.Width = 140;
            // 
            // orderBookTitle
            // 
            this.orderBookTitle.Text = "Book Title";
            this.orderBookTitle.Width = 120;
            // 
            // orderClientName
            // 
            this.orderClientName.Text = "Client Name";
            this.orderClientName.Width = 180;
            // 
            // AllOrdersWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 599);
            this.Controls.Add(this.listViewOrders);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AllOrdersWindow";
            this.Text = "AllOrdersWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewOrders;
        private System.Windows.Forms.ColumnHeader orderUUID;
        private System.Windows.Forms.ColumnHeader orderQuantity;
        private System.Windows.Forms.ColumnHeader orderTotalPrice;
        private System.Windows.Forms.ColumnHeader orderState;
        private System.Windows.Forms.ColumnHeader orderStateDate;
        private System.Windows.Forms.ColumnHeader orderBookTitle;
        private System.Windows.Forms.ColumnHeader orderClientName;
    }
}