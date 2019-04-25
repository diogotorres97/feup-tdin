using System;
using System.Windows.Forms;

namespace Payment
{
    public partial class PaymentWindow : Form
    {
        private PaymentController _paymentController;

        private OperationEventRepeater<Table> _evTableRepeater;

        private delegate void ChangeTableStateDelegate(Table table);

        public PaymentWindow()
        {
            _paymentController = new PaymentController();
            InitializeComponent();
            _evTableRepeater = new OperationEventRepeater<Table>();
            _evTableRepeater.OperationEvent += DoTableAlterations;
            _paymentController.AddTableAlterEvent(_evTableRepeater.Repeater);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        private void DoTableAlterations(Operation op, Table table)
        {
            ChangeTableStateDelegate changeState = ChangeTableAvailability;
            BeginInvoke(changeState, table);
        }

        private void ChangeTableAvailability(Table table)
        {
            foreach (Control ctr in tableLayoutPanel1.Controls)
            {
                if (ctr is Button)
                {
                    Button btn = (Button) ctr;
                    if (btn.Name.Equals($"btnTable{table.Id}"))
                    {
                        btn.Text = "Table" + table.Id + (table.Availability ? "\nAvailable" : "");
                        break;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTableButtonsList();
        }

        private void PaymentWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evTableRepeater.OperationEvent -= DoTableAlterations;
            _paymentController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
        }


        private void LoadTableButtonsList()
        {
            int numTables = _paymentController.Tables.Count;

            int rowCount = 0, columnCount = 4;

            GenerateNumberRowsAndCols(numTables, ref rowCount, columnCount);

            tableLayoutPanel1.ColumnCount = columnCount;
            tableLayoutPanel1.RowCount = rowCount;

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();


            for (int i = 0; i < columnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columnCount));
            }

            if (rowCount < 3)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                }
            }
            else
            {
                for (int i = 0; i < rowCount; i++)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
                }
            }


            foreach (Table t in _paymentController.Tables)
            {
                uint id = t.Id;
                Button b = new Button
                {
                    Text = "Table" + id + (t.Availability ? "\nAvailable" : ""),
                    Name = $"btnTable{id}"
                };
                b.Click += TableButtonClick;
                b.Dock = DockStyle.Fill;
                b.Margin = new Padding(40);
                tableLayoutPanel1.Controls.Add(b);
            }
        }

        private static void GenerateNumberRowsAndCols(int numTables, ref int rowCount, int columnCount)
        {
            rowCount = (int) Math.Ceiling(numTables / (columnCount * 1.0));
        }

        private void TableButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button) sender;
            if (btn == null) return;
            PaymentTable form = new PaymentTable(btn.Name, _paymentController);
            form.Show();
        }
    }
}