partial class ClientWindow {
  /// <summary>
  /// Required designer variable.
  /// </summary>
  private System.ComponentModel.IContainer components = null;

  /// <summary>
  /// Clean up any resources being used.
  /// </summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing) {
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
  private void InitializeComponent() {
            this.changeCommentButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.addItemButton = new System.Windows.Forms.Button();
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // changeCommentButton
            // 
            this.changeCommentButton.Location = new System.Drawing.Point(180, 549);
            this.changeCommentButton.Margin = new System.Windows.Forms.Padding(4);
            this.changeCommentButton.Name = "changeCommentButton";
            this.changeCommentButton.Size = new System.Drawing.Size(159, 39);
            this.changeCommentButton.TabIndex = 5;
            this.changeCommentButton.Text = "Change Comment";
            this.changeCommentButton.UseVisualStyleBackColor = true;
            this.changeCommentButton.Click += new System.EventHandler(this.changeCommentButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(499, 549);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(159, 39);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 495);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // nameTB
            // 
            this.nameTB.Location = new System.Drawing.Point(264, 491);
            this.nameTB.Margin = new System.Windows.Forms.Padding(4);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(359, 22);
            this.nameTB.TabIndex = 3;
            // 
            // addItemButton
            // 
            this.addItemButton.Location = new System.Drawing.Point(664, 486);
            this.addItemButton.Margin = new System.Windows.Forms.Padding(4);
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Size = new System.Drawing.Size(167, 33);
            this.addItemButton.TabIndex = 4;
            this.addItemButton.Text = "Add Item";
            this.addItemButton.UseVisualStyleBackColor = true;
            this.addItemButton.Click += new System.EventHandler(this.addItemButton_Click);
            // 
            // type
            // 
            this.type.Text = "Type";
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 218;
            // 
            // comment
            // 
            this.comment.Text = "Comment";
            this.comment.Width = 328;
            // 
            // itemListView
            // 
            this.itemListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.type,
            this.name,
            this.comment});
            this.itemListView.FullRowSelect = true;
            this.itemListView.GridLines = true;
            this.itemListView.HideSelection = false;
            this.itemListView.Location = new System.Drawing.Point(17, 16);
            this.itemListView.Margin = new System.Windows.Forms.Padding(4);
            this.itemListView.MultiSelect = false;
            this.itemListView.Name = "itemListView";
            this.itemListView.Size = new System.Drawing.Size(812, 457);
            this.itemListView.TabIndex = 1;
            this.itemListView.UseCompatibleStateImageBehavior = false;
            this.itemListView.View = System.Windows.Forms.View.Details;
            this.itemListView.SelectedIndexChanged += new System.EventHandler(this.itemListView_SelectedIndexChanged);
            // 
            // ClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 625);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.changeCommentButton);
            this.Controls.Add(this.addItemButton);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.itemListView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ClientWindow";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Text = "Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientWindow_FormClosed);
            this.Load += new System.EventHandler(this.ClientWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

  }

  #endregion
  private System.Windows.Forms.Button changeCommentButton;
  private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox nameTB;
    private System.Windows.Forms.Button addItemButton;
    private System.Windows.Forms.ColumnHeader type;
    private System.Windows.Forms.ColumnHeader name;
    private System.Windows.Forms.ColumnHeader comment;
    private System.Windows.Forms.ListView itemListView;
}

