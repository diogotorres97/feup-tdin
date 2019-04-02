using System;
using System.Drawing;
using System.Windows.Forms;

namespace DiningRoom
{
    public partial class DiningRoomWindow : Form
    {
        private DiningRoomController _diningRoomController;

        public DiningRoomWindow()
        {
            _diningRoomController = new DiningRoomController();
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            LoadTableButtonsList();
        }

        private void LoadTableButtonsList()
        {
            int numTables = _diningRoomController.Tables.Count;

            int rowCount = 0, columnCount = 4;

            GenerateNumberRowsAndCols(numTables, ref rowCount, columnCount);

            tableLayoutPanel1.ColumnCount = columnCount;
            tableLayoutPanel1.RowCount = rowCount;

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();



            for (int i = 0; i < columnCount; i++) {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / columnCount));
            }

            if(rowCount < 3) {
                for (int i = 0; i < rowCount; i++) {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                }
            } else {
                for (int i = 0; i < rowCount; i++) {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
                }
            }
            


            foreach(Table t in _diningRoomController.Tables){
                Console.WriteLine(t);
                uint id = t.Id;
                Button b = new Button {
                    Text = "Table" + id + (t.Availability ? "\nAvailable" : ""),
                    Name = string.Format("table{0}", id),
                };
                b.Click += TableButtonClick;
                b.Dock = DockStyle.Fill;
                b.Margin = new Padding(40);
                tableLayoutPanel1.Controls.Add(b);
            }

            
        }

        private void GenerateNumberRowsAndCols(int numTables, ref int rowCount, int columnCount)
        {
            
            rowCount = (int) Math.Ceiling(numTables / (columnCount * 1.0));
        }

        private void TableButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != null) {
                //this.Hide();
                DiningRoomTable form = new DiningRoomTable(btn.Name, _diningRoomController);
                form.Show();
            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo)) {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }
    }
}