namespace @interface
{
    partial class ClientWindow
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBoxID = new System.Windows.Forms.TextBox();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.txtBoxAddress = new System.Windows.Forms.TextBox();
            this.txtBoxEmail = new System.Windows.Forms.TextBox();
            this.listViewSells = new System.Windows.Forms.ListView();
            this.sellUUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sellQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sellTotalPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sellBookTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 444);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "List of Sells";
            // 
            // listViewOrders
            // 
            this.listViewOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.orderUUID,
            this.orderQuantity,
            this.orderTotalPrice,
            this.orderState,
            this.orderStateDate,
            this.orderBookTitle});
            this.listViewOrders.FullRowSelect = true;
            this.listViewOrders.GridLines = true;
            this.listViewOrders.Location = new System.Drawing.Point(82, 185);
            this.listViewOrders.MultiSelect = false;
            this.listViewOrders.Name = "listViewOrders";
            this.listViewOrders.Size = new System.Drawing.Size(912, 244);
            this.listViewOrders.TabIndex = 13;
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
            this.orderStateDate.Width = 82;
            // 
            // orderBookTitle
            // 
            this.orderBookTitle.Text = "Book Title";
            this.orderBookTitle.Width = 180;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "List of Orders";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Email";
            // 
            // txtBoxID
            // 
            this.txtBoxID.Enabled = false;
            this.txtBoxID.Location = new System.Drawing.Point(189, 18);
            this.txtBoxID.Name = "txtBoxID";
            this.txtBoxID.Size = new System.Drawing.Size(215, 22);
            this.txtBoxID.TabIndex = 18;
            // 
            // txtBoxName
            // 
            this.txtBoxName.Enabled = false;
            this.txtBoxName.Location = new System.Drawing.Point(189, 50);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(215, 22);
            this.txtBoxName.TabIndex = 19;
            // 
            // txtBoxAddress
            // 
            this.txtBoxAddress.Enabled = false;
            this.txtBoxAddress.Location = new System.Drawing.Point(189, 82);
            this.txtBoxAddress.Name = "txtBoxAddress";
            this.txtBoxAddress.Size = new System.Drawing.Size(215, 22);
            this.txtBoxAddress.TabIndex = 20;
            // 
            // txtBoxEmail
            // 
            this.txtBoxEmail.Enabled = false;
            this.txtBoxEmail.Location = new System.Drawing.Point(189, 117);
            this.txtBoxEmail.Name = "txtBoxEmail";
            this.txtBoxEmail.Size = new System.Drawing.Size(215, 22);
            this.txtBoxEmail.TabIndex = 21;
            // 
            // listViewSells
            // 
            this.listViewSells.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sellUUID,
            this.sellQuantity,
            this.sellTotalPrice,
            this.sellBookTitle});
            this.listViewSells.FullRowSelect = true;
            this.listViewSells.GridLines = true;
            this.listViewSells.Location = new System.Drawing.Point(82, 497);
            this.listViewSells.MultiSelect = false;
            this.listViewSells.Name = "listViewSells";
            this.listViewSells.Size = new System.Drawing.Size(912, 244);
            this.listViewSells.TabIndex = 22;
            this.listViewSells.UseCompatibleStateImageBehavior = false;
            this.listViewSells.View = System.Windows.Forms.View.Details;
            // 
            // sellUUID
            // 
            this.sellUUID.Text = "UUID";
            // 
            // sellQuantity
            // 
            this.sellQuantity.Text = "Quantity";
            // 
            // sellTotalPrice
            // 
            this.sellTotalPrice.Text = "Total Price";
            // 
            // sellBookTitle
            // 
            this.sellBookTitle.Text = "Book Title";
            this.sellBookTitle.Width = 180;
            // 
            // ClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.listViewSells);
            this.Controls.Add(this.txtBoxEmail);
            this.Controls.Add(this.txtBoxAddress);
            this.Controls.Add(this.txtBoxName);
            this.Controls.Add(this.txtBoxID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listViewOrders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ClientWindow";
            this.Text = "ClientWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListView listViewOrders;
    private System.Windows.Forms.ColumnHeader orderUUID;
    private System.Windows.Forms.ColumnHeader orderQuantity;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxID;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.TextBox txtBoxAddress;
        private System.Windows.Forms.TextBox txtBoxEmail;
        private System.Windows.Forms.ColumnHeader orderTotalPrice;
        private System.Windows.Forms.ColumnHeader orderState;
        private System.Windows.Forms.ColumnHeader orderStateDate;
        private System.Windows.Forms.ColumnHeader orderBookTitle;
        private System.Windows.Forms.ListView listViewSells;
        private System.Windows.Forms.ColumnHeader sellUUID;
        private System.Windows.Forms.ColumnHeader sellQuantity;
        private System.Windows.Forms.ColumnHeader sellTotalPrice;
        private System.Windows.Forms.ColumnHeader sellBookTitle;
    }
}