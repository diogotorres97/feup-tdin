namespace BarKitchen
{
    partial class BarKitchenChooseMenu
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
            this.btnBar = new System.Windows.Forms.Button();
            this.btnKitchen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBar
            // 
            this.btnBar.Location = new System.Drawing.Point(457, 215);
            this.btnBar.Name = "btnBar";
            this.btnBar.Size = new System.Drawing.Size(115, 103);
            this.btnBar.TabIndex = 0;
            this.btnBar.Text = "Open Bar";
            this.btnBar.UseVisualStyleBackColor = true;
            this.btnBar.Click += new System.EventHandler(this.btnBar_Click);
            // 
            // btnKitchen
            // 
            this.btnKitchen.Location = new System.Drawing.Point(649, 215);
            this.btnKitchen.Name = "btnKitchen";
            this.btnKitchen.Size = new System.Drawing.Size(115, 103);
            this.btnKitchen.TabIndex = 1;
            this.btnKitchen.Text = "Open Kitchen";
            this.btnKitchen.UseVisualStyleBackColor = true;
            this.btnKitchen.Click += new System.EventHandler(this.btnKitchen_Click);
            // 
            // BarKitchenChooseMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.btnKitchen);
            this.Controls.Add(this.btnBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BarKitchenChooseMenu";
            this.Text = "BarKitchenChooseMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBar;
        private System.Windows.Forms.Button btnKitchen;
    }
}