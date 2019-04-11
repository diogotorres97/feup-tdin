﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace DiningRoom
{
    public partial class DiningRoomWindow : Form
    {
        private DiningRoomController _diningRoomController;

        private OperationEventRepeater<Table> _evTableRepeater;
        private delegate void ChangeTableStateDelegate(Table table);

        public DiningRoomWindow()
        {
            _diningRoomController = new DiningRoomController();
            InitializeComponent();
            _evTableRepeater = new OperationEventRepeater<Table>();
            _evTableRepeater.OperationEvent += DoTableAlterations;
            _diningRoomController.AddTableAlterEvent(_evTableRepeater.Repeater);
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
            foreach (Control ctr in tableLayoutPanel1.Controls) {
                if (ctr is Button) {
                    Button btn = ((Button)ctr);
                    if (btn.Name.Equals(string.Format("btnTable{0}", table.Id)))
                        btn.Text = "Table" + table.Id + (table.Availability ? "\nAvailable" : "");
                }
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            LoadTableButtonsList();
        }

        private void DiningRoomWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _evTableRepeater.OperationEvent -= DoTableAlterations;
            _diningRoomController.RemoveTableAlterEvent(_evTableRepeater.Repeater);
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
                uint id = t.Id;
                Button b = new Button {
                    Text = "Table" + id + (t.Availability ? "\nAvailable" : ""),
                    Name = string.Format("btnTable{0}", id),
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
        }

       
    }
}