namespace @interface
{
    partial class StoreWindow
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
        if (disposing && (components != null)) {
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
            this.btnSell = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnViewClient = new System.Windows.Forms.Button();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnNotifications = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewClients = new System.Windows.Forms.ListView();
            this.idClient = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewBooks = new System.Windows.Forms.ListView();
            this.idBook = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stock = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnSell
            // 
            this.btnSell.Location = new System.Drawing.Point(666, 503);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(123, 84);
            this.btnSell.TabIndex = 0;
            this.btnSell.Text = "Sell Book";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(857, 503);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(123, 84);
            this.btnOrder.TabIndex = 3;
            this.btnOrder.Text = "Order Book";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnViewClient
            // 
            this.btnViewClient.Location = new System.Drawing.Point(104, 503);
            this.btnViewClient.Name = "btnViewClient";
            this.btnViewClient.Size = new System.Drawing.Size(123, 84);
            this.btnViewClient.TabIndex = 4;
            this.btnViewClient.Text = "View Client";
            this.btnViewClient.UseVisualStyleBackColor = true;
            this.btnViewClient.Click += new System.EventHandler(this.btnViewClient_Click);
            // 
            // btnStatistics
            // 
            this.btnStatistics.Location = new System.Drawing.Point(672, 33);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(191, 40);
            this.btnStatistics.TabIndex = 5;
            this.btnStatistics.Text = "Statistics";
            this.btnStatistics.UseVisualStyleBackColor = true;
            // 
            // btnNotifications
            // 
            this.btnNotifications.Location = new System.Drawing.Point(898, 33);
            this.btnNotifications.Name = "btnNotifications";
            this.btnNotifications.Size = new System.Drawing.Size(191, 40);
            this.btnNotifications.TabIndex = 6;
            this.btnNotifications.Text = "Notifications";
            this.btnNotifications.UseVisualStyleBackColor = true;
            this.btnNotifications.Click += new System.EventHandler(this.btnNotifications_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "List of Clients";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(761, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "List of Books";
            // 
            // listViewClients
            // 
            this.listViewClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idClient,
            this.name});
            this.listViewClients.FullRowSelect = true;
            this.listViewClients.GridLines = true;
            this.listViewClients.HideSelection = false;
            this.listViewClients.Location = new System.Drawing.Point(37, 219);
            this.listViewClients.MultiSelect = false;
            this.listViewClients.Name = "listViewClients";
            this.listViewClients.Size = new System.Drawing.Size(275, 244);
            this.listViewClients.TabIndex = 9;
            this.listViewClients.UseCompatibleStateImageBehavior = false;
            this.listViewClients.View = System.Windows.Forms.View.Details;
            // 
            // idClient
            // 
            this.idClient.Text = "ID";
            // 
            // name
            // 
            this.name.Text = "name";
            this.name.Width = 180;
            // 
            // listViewBooks
            // 
            this.listViewBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idBook,
            this.title,
            this.author,
            this.price,
            this.stock});
            this.listViewBooks.FullRowSelect = true;
            this.listViewBooks.GridLines = true;
            this.listViewBooks.Location = new System.Drawing.Point(443, 219);
            this.listViewBooks.MultiSelect = false;
            this.listViewBooks.Name = "listViewBooks";
            this.listViewBooks.Size = new System.Drawing.Size(727, 244);
            this.listViewBooks.TabIndex = 10;
            this.listViewBooks.UseCompatibleStateImageBehavior = false;
            this.listViewBooks.View = System.Windows.Forms.View.Details;
            // 
            // idBook
            // 
            this.idBook.Text = "ID";
            // 
            // title
            // 
            this.title.Text = "Title";
            this.title.Width = 180;
            // 
            // author
            // 
            this.author.Text = "Author";
            this.author.Width = 180;
            // 
            // price
            // 
            this.price.Text = "Price";
            // 
            // stock
            // 
            this.stock.Text = "Stock";
            // 
            // StoreWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.listViewBooks);
            this.Controls.Add(this.listViewClients);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNotifications);
            this.Controls.Add(this.btnStatistics);
            this.Controls.Add(this.btnViewClient);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.btnSell);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StoreWindow";
            this.Text = "StoreWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnSell;
    private System.Windows.Forms.Button btnOrder;
    private System.Windows.Forms.Button btnViewClient;
    private System.Windows.Forms.Button btnStatistics;
    private System.Windows.Forms.Button btnNotifications;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewClients;
        private System.Windows.Forms.ColumnHeader idClient;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ListView listViewBooks;
        private System.Windows.Forms.ColumnHeader idBook;
        private System.Windows.Forms.ColumnHeader title;
        private System.Windows.Forms.ColumnHeader author;
        private System.Windows.Forms.ColumnHeader price;
        private System.Windows.Forms.ColumnHeader stock;
    }
}