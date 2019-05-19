namespace @interface
{
    partial class NotificationsWindow
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
            this.btnDispatch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewReceiveStock = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bookTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnDispatch
            // 
            this.btnDispatch.Location = new System.Drawing.Point(96, 467);
            this.btnDispatch.Name = "btnDispatch";
            this.btnDispatch.Size = new System.Drawing.Size(123, 84);
            this.btnDispatch.TabIndex = 0;
            this.btnDispatch.Text = "Dispatch Order";
            this.btnDispatch.UseVisualStyleBackColor = true;
            this.btnDispatch.Click += new System.EventHandler(this.btnDispatch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "List of Receive Stock";
            // 
            // listViewReceiveStock
            // 
            this.listViewReceiveStock.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.quantity,
            this.processedDate,
            this.bookTitle});
            this.listViewReceiveStock.FullRowSelect = true;
            this.listViewReceiveStock.GridLines = true;
            this.listViewReceiveStock.Location = new System.Drawing.Point(96, 197);
            this.listViewReceiveStock.MultiSelect = false;
            this.listViewReceiveStock.Name = "listViewReceiveStock";
            this.listViewReceiveStock.Size = new System.Drawing.Size(727, 244);
            this.listViewReceiveStock.TabIndex = 10;
            this.listViewReceiveStock.UseCompatibleStateImageBehavior = false;
            this.listViewReceiveStock.View = System.Windows.Forms.View.Details;
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
            // NotificationsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.listViewReceiveStock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDispatch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NotificationsWindow";
            this.Text = "NotificationsWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDispatch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewReceiveStock;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader quantity;
        private System.Windows.Forms.ColumnHeader processedDate;
        private System.Windows.Forms.ColumnHeader bookTitle;
    }
}