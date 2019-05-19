namespace @interface
{
    partial class WarehouseWindow
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
            this.btnShip = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewRequests = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bookTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnShip
            // 
            this.btnShip.Location = new System.Drawing.Point(107, 488);
            this.btnShip.Name = "btnShip";
            this.btnShip.Size = new System.Drawing.Size(123, 84);
            this.btnShip.TabIndex = 0;
            this.btnShip.Text = "Ship stock";
            this.btnShip.UseVisualStyleBackColor = true;
            this.btnShip.Click += new System.EventHandler(this.btnShip_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "List of Requests";
            // 
            // listViewRequests
            // 
            this.listViewRequests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.quantity,
            this.processedDate,
            this.bookTitle});
            this.listViewRequests.FullRowSelect = true;
            this.listViewRequests.GridLines = true;
            this.listViewRequests.Location = new System.Drawing.Point(104, 211);
            this.listViewRequests.MultiSelect = false;
            this.listViewRequests.Name = "listViewRequests";
            this.listViewRequests.Size = new System.Drawing.Size(727, 244);
            this.listViewRequests.TabIndex = 10;
            this.listViewRequests.UseCompatibleStateImageBehavior = false;
            this.listViewRequests.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "ID";
            // 
            // quantity
            // 
            this.quantity.Text = "Quantity";
            this.quantity.Width = 180;
            // 
            // processedDate
            // 
            this.processedDate.Text = "Processed Date";
            this.processedDate.Width = 180;
            // 
            // bookTitle
            // 
            this.bookTitle.Text = "Book Title";
            this.bookTitle.Width = 180;
            // 
            // WarehouseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.listViewRequests);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WarehouseWindow";
            this.Text = "WarehouseWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewRequests;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader quantity;
        private System.Windows.Forms.ColumnHeader processedDate;
        private System.Windows.Forms.ColumnHeader bookTitle;
    }
}