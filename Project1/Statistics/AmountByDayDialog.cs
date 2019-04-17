using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Statistics
{
    public partial class AmountByDayDialog : Form
    {
        private ConcurrentDictionary<string, double> _amountByDay;

        public AmountByDayDialog(ConcurrentDictionary<string, double> amountByDay)
        {
            InitializeComponent();
            _amountByDay = amountByDay;
            InitializeAmountsList();
        }

        private void InitializeAmountsList()
        {
            foreach (KeyValuePair<string, double> it in _amountByDay)
            {
                ListViewItem lvItem = new ListViewItem(new[]
                {
                    it.Key, it.Value.ToString()
                });
                listViewAmountByDay.Items.Add(lvItem);
            }
        }
    }
}