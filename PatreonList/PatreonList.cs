/*
 * PatreonList - https://github.com/Rosalie241/PatreonList
 *  Copyright (C) 2021 Rosalie Wanders <rosalie@mailbox.org>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3.
 *  You should have received a copy of the GNU General Public License
 *  along with this program. If not, see <https://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using CsvHelper;

namespace PatreonList
{
    public partial class PatreonList : Form
    {
        public PatreonList()
        {
            InitializeComponent();
        }

        private List<Patron> _PatronList = new List<Patron>();

        private void ReadFileIntoList(string file)
        {
            _PatronList.Clear();

            using (var stream = new StreamReader(file))
            {
                using (var reader = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    _PatronList = reader.GetRecords<Patron>().ToList();
                }
            }
        }

        private void SortList()
        {
            _PatronList = _PatronList.OrderBy(p => p.LifetimeAmount)
                .OrderBy(p => p.Amount).Reverse().ToList();
        }

        private void DisplayList()
        {
            textBox1.Clear();

            string line;
            foreach (Patron p in _PatronList)
            {
                if ((p.Status != "Active patron") || p.Amount < 5)
                    continue;

                if (checkBox1.Checked)
                {
                    line = $"{p.Name}: {p.Amount}: {p.LifetimeAmount}";
                }
                else
                {
                    line = p.Name;
                }

                textBox1.Text += line + Environment.NewLine;
            }
        }

        private void BlockGUI(bool value)
        {
            button1.Enabled = !value;
            button2.Enabled = !value;
            checkBox1.Enabled = !value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv"
            };

            dialog.ShowDialog();

            if (String.IsNullOrEmpty(dialog.FileName))
                return;

            BlockGUI(true);
            ReadFileIntoList(dialog.FileName);
            SortList();
            DisplayList();
            BlockGUI(false);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            BlockGUI(true);
            DisplayList();
            BlockGUI(false);
        }

        private void PatreonList_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
    }
}
