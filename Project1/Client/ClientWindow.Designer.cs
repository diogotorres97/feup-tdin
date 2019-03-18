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
    this.itemListView = new System.Windows.Forms.ListView();
    this.type = new System.Windows.Forms.ColumnHeader();
    this.name = new System.Windows.Forms.ColumnHeader();
    this.comment = new System.Windows.Forms.ColumnHeader();
    this.label2 = new System.Windows.Forms.Label();
    this.nameTB = new System.Windows.Forms.TextBox();
    this.addItemButton = new System.Windows.Forms.Button();
    this.changeCommentButton = new System.Windows.Forms.Button();
    this.closeButton = new System.Windows.Forms.Button();
    this.SuspendLayout();
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
    this.itemListView.Location = new System.Drawing.Point(13, 13);
    this.itemListView.MultiSelect = false;
    this.itemListView.Name = "itemListView";
    this.itemListView.Size = new System.Drawing.Size(610, 372);
    this.itemListView.TabIndex = 1;
    this.itemListView.UseCompatibleStateImageBehavior = false;
    this.itemListView.View = System.Windows.Forms.View.Details;
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
    // label2
    // 
    this.label2.AutoSize = true;
    this.label2.Location = new System.Drawing.Point(154, 402);
    this.label2.Name = "label2";
    this.label2.Size = new System.Drawing.Size(38, 13);
    this.label2.TabIndex = 0;
    this.label2.Text = "Name:";
    // 
    // nameTB
    // 
    this.nameTB.Location = new System.Drawing.Point(198, 399);
    this.nameTB.Name = "nameTB";
    this.nameTB.Size = new System.Drawing.Size(270, 20);
    this.nameTB.TabIndex = 3;
    // 
    // addItemButton
    // 
    this.addItemButton.Location = new System.Drawing.Point(498, 395);
    this.addItemButton.Name = "addItemButton";
    this.addItemButton.Size = new System.Drawing.Size(125, 27);
    this.addItemButton.TabIndex = 4;
    this.addItemButton.Text = "Add Item";
    this.addItemButton.UseVisualStyleBackColor = true;
    this.addItemButton.Click += new System.EventHandler(this.addItemButton_Click);
    // 
    // changeCommentButton
    // 
    this.changeCommentButton.Location = new System.Drawing.Point(135, 446);
    this.changeCommentButton.Name = "changeCommentButton";
    this.changeCommentButton.Size = new System.Drawing.Size(119, 32);
    this.changeCommentButton.TabIndex = 5;
    this.changeCommentButton.Text = "Change Comment";
    this.changeCommentButton.UseVisualStyleBackColor = true;
    this.changeCommentButton.Click += new System.EventHandler(this.changeCommentButton_Click);
    // 
    // closeButton
    // 
    this.closeButton.Location = new System.Drawing.Point(374, 446);
    this.closeButton.Name = "closeButton";
    this.closeButton.Size = new System.Drawing.Size(119, 32);
    this.closeButton.TabIndex = 6;
    this.closeButton.Text = "Close";
    this.closeButton.UseVisualStyleBackColor = true;
    this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
    // 
    // ClientWindow
    // 
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.ClientSize = new System.Drawing.Size(635, 508);
    this.Controls.Add(this.closeButton);
    this.Controls.Add(this.changeCommentButton);
    this.Controls.Add(this.addItemButton);
    this.Controls.Add(this.nameTB);
    this.Controls.Add(this.label2);
    this.Controls.Add(this.itemListView);
    this.Name = "ClientWindow";
    this.Text = "Client";
    this.Load += new System.EventHandler(this.ClientWindow_Load);
    this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientWindow_FormClosed);
    this.ResumeLayout(false);
    this.PerformLayout();

  }

  #endregion

  private System.Windows.Forms.ListView itemListView;
  private System.Windows.Forms.ColumnHeader type;
  private System.Windows.Forms.ColumnHeader name;
  private System.Windows.Forms.ColumnHeader comment;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.TextBox nameTB;
  private System.Windows.Forms.Button addItemButton;
  private System.Windows.Forms.Button changeCommentButton;
  private System.Windows.Forms.Button closeButton;
}

