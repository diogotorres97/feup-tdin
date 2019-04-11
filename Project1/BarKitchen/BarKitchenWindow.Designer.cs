namespace BarKitchen
{
    partial class BarKitchenWindow
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
            this.notPickedListView = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.state = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrepare = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.inPreparationListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReady = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // notPickedListView
            // 
            this.notPickedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.type,
            this.name,
            this.quantity,
            this.state});
            this.notPickedListView.FullRowSelect = true;
            this.notPickedListView.GridLines = true;
            this.notPickedListView.HideSelection = false;
            this.notPickedListView.Location = new System.Drawing.Point(24, 53);
            this.notPickedListView.Margin = new System.Windows.Forms.Padding(4);
            this.notPickedListView.MultiSelect = false;
            this.notPickedListView.Name = "notPickedListView";
            this.notPickedListView.Size = new System.Drawing.Size(438, 457);
            this.notPickedListView.TabIndex = 3;
            this.notPickedListView.UseCompatibleStateImageBehavior = false;
            this.notPickedListView.View = System.Windows.Forms.View.Details;
            // 
            // type
            // 
            this.type.Text = "ID";
            // 
            // name
            // 
            this.name.Text = "Product";
            this.name.Width = 120;
            // 
            // quantity
            // 
            this.quantity.Text = "Quantity";
            // 
            // state
            // 
            this.state.Text = "State";
            this.state.Width = 160;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Not Picked";
            // 
            // btnPrepare
            // 
            this.btnPrepare.Location = new System.Drawing.Point(507, 241);
            this.btnPrepare.Name = "btnPrepare";
            this.btnPrepare.Size = new System.Drawing.Size(75, 70);
            this.btnPrepare.TabIndex = 6;
            this.btnPrepare.Text = "Prepare";
            this.btnPrepare.UseVisualStyleBackColor = true;
            this.btnPrepare.Click += new System.EventHandler(this.btnPrepare_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(633, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "In preparation";
            // 
            // inPreparationListView
            // 
            this.inPreparationListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.inPreparationListView.FullRowSelect = true;
            this.inPreparationListView.GridLines = true;
            this.inPreparationListView.HideSelection = false;
            this.inPreparationListView.Location = new System.Drawing.Point(636, 53);
            this.inPreparationListView.Margin = new System.Windows.Forms.Padding(4);
            this.inPreparationListView.MultiSelect = false;
            this.inPreparationListView.Name = "inPreparationListView";
            this.inPreparationListView.Size = new System.Drawing.Size(448, 457);
            this.inPreparationListView.TabIndex = 7;
            this.inPreparationListView.UseCompatibleStateImageBehavior = false;
            this.inPreparationListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Product";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Quantity";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "State";
            this.columnHeader4.Width = 160;
            // 
            // btnReady
            // 
            this.btnReady.Location = new System.Drawing.Point(1095, 241);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(75, 70);
            this.btnReady.TabIndex = 9;
            this.btnReady.Text = "Make Ready";
            this.btnReady.UseVisualStyleBackColor = true;
            this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
            // 
            // BarKitchenWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.btnReady);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inPreparationListView);
            this.Controls.Add(this.btnPrepare);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.notPickedListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BarKitchenWindow";
            this.Text = "BarKitchenWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BarKitchenWindow_FormClosed);
            this.Load += new System.EventHandler(this.BarKitchenWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView notPickedListView;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader quantity;
        private System.Windows.Forms.ColumnHeader state;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrepare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView inPreparationListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnReady;
    }
}
