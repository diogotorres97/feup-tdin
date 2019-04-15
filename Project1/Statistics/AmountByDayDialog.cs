using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Statistics
{
    public partial class AmountByDayDialog : Form
    {
        private ConcurrentDictionary<string, double> _AmountByDay;
        public AmountByDayDialog(ConcurrentDictionary<string, double> AmountByDay)
        {
            InitializeComponent();
            _AmountByDay = AmountByDay;
            InitializeAmountsList();
        }

        private void InitializeAmountsList()
        {
            foreach (KeyValuePair<string, double> it in _AmountByDay) {
                ListViewItem lvItem = new ListViewItem(new[]
                {
                    it.Key.ToString(), it.Value.ToString()
                });
                listViewAmountByDay.Items.Add(lvItem);
            }
        }




    }
}
