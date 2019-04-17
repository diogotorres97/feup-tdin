namespace Statistics
{
    partial class StatisticsWindow
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
            this.txtBoxNumInvoices = new System.Windows.Forms.TextBox();
            this.productsListView = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnViewAmountByDay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Invoices";
            // 
            // txtBoxNumInvoices
            // 
            this.txtBoxNumInvoices.Enabled = false;
            this.txtBoxNumInvoices.Location = new System.Drawing.Point(618, 63);
            this.txtBoxNumInvoices.Name = "txtBoxNumInvoices";
            this.txtBoxNumInvoices.Size = new System.Drawing.Size(100, 22);
            this.txtBoxNumInvoices.TabIndex = 1;
            // 
            // productsListView
            // 
            this.productsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.type,
            this.name,
            this.price,
            this.count});
            this.productsListView.FullRowSelect = true;
            this.productsListView.GridLines = true;
            this.productsListView.HideSelection = false;
            this.productsListView.Location = new System.Drawing.Point(352, 211);
            this.productsListView.Margin = new System.Windows.Forms.Padding(4);
            this.productsListView.MultiSelect = false;
            this.productsListView.Name = "productsListView";
            this.productsListView.Size = new System.Drawing.Size(690, 307);
            this.productsListView.TabIndex = 4;
            this.productsListView.UseCompatibleStateImageBehavior = false;
            this.productsListView.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "ID";
            // 
            // type
            // 
            this.type.Text = "Type";
            // 
            // name
            // 
            this.name.Text = "Product";
            this.name.Width = 120;
            // 
            // price
            // 
            this.price.Text = "Price";
            this.price.Width = 120;
            // 
            // count
            // 
            this.count.Text = "Count";
            this.count.Width = 160;
            // 
            // btnViewAmountByDay
            // 
            this.btnViewAmountByDay.Location = new System.Drawing.Point(352, 125);
            this.btnViewAmountByDay.Name = "btnViewAmountByDay";
            this.btnViewAmountByDay.Size = new System.Drawing.Size(159, 33);
            this.btnViewAmountByDay.TabIndex = 5;
            this.btnViewAmountByDay.Text = "View Amount by Day";
            this.btnViewAmountByDay.UseVisualStyleBackColor = true;
            this.btnViewAmountByDay.Click += new System.EventHandler(this.btnViewAmountByDay_Click);
            // 
            // StatisticsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.btnViewAmountByDay);
            this.Controls.Add(this.productsListView);
            this.Controls.Add(this.txtBoxNumInvoices);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StatisticsWindow";
            this.Text = "Statistics";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StatisticsWindow_FormClosed);
            this.Load += new System.EventHandler(this.StatisticsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxNumInvoices;
        private System.Windows.Forms.ListView productsListView;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader price;
        private System.Windows.Forms.ColumnHeader count;
        private System.Windows.Forms.Button btnViewAmountByDay;
    }
}

