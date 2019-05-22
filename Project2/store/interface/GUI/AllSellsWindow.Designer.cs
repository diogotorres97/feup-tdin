namespace @interface
{
    partial class AllSellsWindow
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
            this.listViewSells = new System.Windows.Forms.ListView();
            this.sellUUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sellQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sellTotalPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sellBookTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.sellClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewSells
            // 
            this.listViewSells.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sellUUID,
            this.sellQuantity,
            this.sellTotalPrice,
            this.sellBookTitle,
            this.sellClientName});
            this.listViewSells.FullRowSelect = true;
            this.listViewSells.GridLines = true;
            this.listViewSells.Location = new System.Drawing.Point(43, 204);
            this.listViewSells.MultiSelect = false;
            this.listViewSells.Name = "listViewSells";
            this.listViewSells.Size = new System.Drawing.Size(912, 244);
            this.listViewSells.TabIndex = 24;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "List of Sells";
            // 
            // sellClientName
            // 
            this.sellClientName.Text = "Client Name";
            this.sellClientName.Width = 180;
            // 
            // AllSellsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 599);
            this.Controls.Add(this.listViewSells);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AllSellsWindow";
            this.Text = "AllSellsWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView listViewSells;
    private System.Windows.Forms.ColumnHeader sellUUID;
    private System.Windows.Forms.ColumnHeader sellQuantity;
    private System.Windows.Forms.ColumnHeader sellTotalPrice;
    private System.Windows.Forms.ColumnHeader sellBookTitle;
    private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader sellClientName;
    }
}