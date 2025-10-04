using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using visualprogramming.lab2;

namespace visualprogramming.Lab7
{
    public partial class LengthDBForm : Form
    {
        public LengthDBForm()
        {
            InitializeComponent();
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var l in LengthDatabase.lengths)
                listBox1.Items.Add($"{l.LengthInMeters} м");
        }

        private void filterLessThanKm_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            
            var filtered = LengthDatabase.lengths
                .Where(x => x.LengthInKilometers < 1)
                .OrderBy(x => x.LengthInMeters);
            
            foreach (var l in filtered)
                listBox1.Items.Add($"{l.LengthInMeters} м");
        }

        private void filterMoreThanMile_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var l in LengthDatabase.lengths.Where(x => x.LengthInMiles > 1))
                listBox1.Items.Add($"{l.LengthInMeters} м");
        }

        private void groupButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var grouped = LengthDatabase.lengths.GroupBy(x => (int)x.LengthInKilometers);
            foreach (var group in grouped)
            {
                listBox1.Items.Add($"Километров: {group.Key}");
                foreach (var l in group)
                    listBox1.Items.Add($"   {l.LengthInMeters} м");
            }
        }

        private void minMaxButton_Click(object sender, EventArgs e)
        {
            var min = LengthDatabase.lengths.Min(x => x.LengthInMeters);
            var max = LengthDatabase.lengths.Max(x => x.LengthInMeters);

            listBox1.Items.Clear();
            listBox1.Items.Add($"Минимум: {min} м");
            listBox1.Items.Add($"Максимум: {max} м");
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var l in LengthDatabase.lengths.OrderBy(x => x.LengthInMeters))
                listBox1.Items.Add($"{l.LengthInMeters} м");
        }
    }
}
