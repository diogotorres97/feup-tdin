namespace @interface
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
            this.label2 = new System.Windows.Forms.Label();
            this.listViewBooks = new System.Windows.Forms.ListView();
            this.idBook = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxTotalSold = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Top Books";
            // 
            // listViewBooks
            // 
            this.listViewBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idBook,
            this.title,
            this.author,
            this.price,
            this.total});
            this.listViewBooks.FullRowSelect = true;
            this.listViewBooks.GridLines = true;
            this.listViewBooks.Location = new System.Drawing.Point(99, 219);
            this.listViewBooks.MultiSelect = false;
            this.listViewBooks.Name = "listViewBooks";
            this.listViewBooks.Size = new System.Drawing.Size(727, 244);
            this.listViewBooks.TabIndex = 12;
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
            // total
            // 
            this.total.Text = "Total";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 530);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Total Sold";
            // 
            // txtBoxTotalSold
            // 
            this.txtBoxTotalSold.Enabled = false;
            this.txtBoxTotalSold.Location = new System.Drawing.Point(211, 527);
            this.txtBoxTotalSold.Name = "txtBoxTotalSold";
            this.txtBoxTotalSold.Size = new System.Drawing.Size(100, 22);
            this.txtBoxTotalSold.TabIndex = 14;
            // 
            // StatisticsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.txtBoxTotalSold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewBooks);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StatisticsWindow";
            this.Text = "StatisticsWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListView listViewBooks;
    private System.Windows.Forms.ColumnHeader idBook;
    private System.Windows.Forms.ColumnHeader title;
    private System.Windows.Forms.ColumnHeader author;
    private System.Windows.Forms.ColumnHeader price;
    private System.Windows.Forms.ColumnHeader total;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtBoxTotalSold;
}
}