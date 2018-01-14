using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace armipsSimpleGui
{
    public partial class Form4 : Form
    {
        private main m;
        public Form4(main m)
        {
            InitializeComponent();
            this.m = m;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            libs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            libs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            fileRamBox.Text = Settings.fileRAM.ToString("X");
            useASMasRootDir.Checked = Settings.useASMasROOT;
            showSuccessBox_CB.Checked = Settings.showSuccessMessageBox;

            Settings.loadPrePostASM();

            string libsPath = Directory.GetCurrentDirectory() + "\\data\\libs";
            
            if (Directory.Exists(libsPath))
            {
                foreach (string dir in Directory.GetDirectories(libsPath))
                {
                    string libName = dir.Remove(0, libsPath.Length + 1);
                    ListViewItem libItem = libs.Items.Add(libName);
                    foreach (String use in Settings.uselibs)
                    {
                        if (use.Equals(libName))
                        {
                            libItem.Checked = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Error: Could not find libs folder!", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Remove checked items (for sorting)
            List<ListViewItem> tempItems = new List<ListViewItem>();
            foreach (ListViewItem checkLib in libs.CheckedItems)
            {
                ListViewItem temp = (ListViewItem)checkLib.Clone();
                tempItems.Add(temp);
                libs.Items.Remove(checkLib);
            }

            // Add checked items back in sorted order.
            int uncheckedCount = libs.Items.Count;
            for (int i = 0; i < Settings.uselibs.Count; i++)
            {
                foreach (ListViewItem item in tempItems)
                {
                    if (Settings.uselibs[i].Equals(item.Text))
                    {
                        libs.Items.Insert(libs.Items.Count - uncheckedCount, item);
                        break;
                    }
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            uint.TryParse(fileRamBox.Text,
                    System.Globalization.NumberStyles.HexNumber,
                    null, out Settings.fileRAM);
            Settings.useASMasROOT = useASMasRootDir.Checked;
            Settings.showSuccessMessageBox = showSuccessBox_CB.Checked;
            Settings.uselibs.Clear();
            foreach (ListViewItem item in libs.Items) {
                if(item.Checked)
                    Settings.uselibs.Add(item.Text);
            }
            Settings.SaveSettings();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string item = fileRamBox.Text;
            int n = 0;
            if (!int.TryParse(item, System.Globalization.NumberStyles.HexNumber, 
                System.Globalization.NumberFormatInfo.CurrentInfo, out n) &&
                item != String.Empty)
            {
                fileRamBox.Text = item.Remove(item.Length - 1, 1);
                fileRamBox.SelectionStart = fileRamBox.Text.Length;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form5().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form6().ShowDialog();
        }

        private void downBut_Click(object sender, EventArgs e)
        {
            foreach (int itemIndex in libs.SelectedIndices)
            {
                if(itemIndex < libs.Items.Count - 1)
                {
                    ListViewItem cpy = (ListViewItem)libs.Items[itemIndex].Clone();
                    libs.Items.RemoveAt(itemIndex);
                    libs.Items.Insert(itemIndex+1, cpy);
                    libs.Items[itemIndex+1].Selected = true;
                    libs.Select();
                }
            }
        }

        private void upBut_Click(object sender, EventArgs e)
        {
            foreach (int itemIndex in libs.SelectedIndices)
            {
                if (itemIndex > 0)
                {
                    ListViewItem cpy = (ListViewItem)libs.Items[itemIndex].Clone();
                    libs.Items.RemoveAt(itemIndex);
                    libs.Items.Insert(itemIndex - 1, cpy);
                    libs.Items[itemIndex - 1].Selected = true;
                    libs.Select();
                }
            }
        }

        private void libs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (libs.SelectedIndices.Count > 0)
            {
                upBut.Enabled = true;
                downBut.Enabled = true;
            }
            else
            {
                upBut.Enabled = false;
                downBut.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Update Armips
            ArmipsUpdater.Run(false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Form8(m).ShowDialog();
        }
    }
}
