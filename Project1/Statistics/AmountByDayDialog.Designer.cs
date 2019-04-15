namespace Statistics
{
    partial class AmountByDayDialog
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
            this.listViewAmountByDay = new System.Windows.Forms.ListView();
            this.day = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.amount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewAmountByDay
            // 
            this.listViewAmountByDay.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.day,
            this.amount});
            this.listViewAmountByDay.Location = new System.Drawing.Point(100, 29);
            this.listViewAmountByDay.MultiSelect = false;
            this.listViewAmountByDay.Name = "listViewAmountByDay";
            this.listViewAmountByDay.Size = new System.Drawing.Size(344, 345);
            this.listViewAmountByDay.TabIndex = 0;
            this.listViewAmountByDay.UseCompatibleStateImageBehavior = false;
            this.listViewAmountByDay.View = System.Windows.Forms.View.Details;
            // 
            // day
            // 
            this.day.Text = "Day";
            this.day.Width = 100;
            // 
            // amount
            // 
            this.amount.Text = "Amount";
            // 
            // AmountByDayDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 413);
            this.Controls.Add(this.listViewAmountByDay);
            this.Name = "AmountByDayDialog";
            this.Text = "AmountByDayDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewAmountByDay;
        private System.Windows.Forms.ColumnHeader day;
        private System.Windows.Forms.ColumnHeader amount;
    }
}