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
    public partial class Form9 : Form
    {
        public string path_string = "";
        public bool do_delete = false;

        public Form9(string path)
        {
            InitializeComponent();
            path_string = path;
            textBox_path.Text = path;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you absoutely sure you want to delete this executable reference?", "Delete reference", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                do_delete = true;
                Hide();
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            path_string = textBox_path.Text;
            Hide();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            string dir = textBox_path.Text.Substring(0, textBox_path.Text.LastIndexOf("/") + 1).Replace("/", "\\");
            if (dir.StartsWith(".\\"))
                dir = Directory.GetCurrentDirectory() + dir.Substring(1);
            file.InitialDirectory = dir;
            //Console.WriteLine(textBox_path.Text.Substring(0, textBox_path.Text.LastIndexOf("/")));
            file.Filter = "Executable files (*.exe, *.bat) | *.exe; *.bat |Any files |*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox_path.Text = file.FileName;
                textBox_path.Text = file.FileName.Replace("\\", "/");
                string current_dir = Directory.GetCurrentDirectory().Replace("\\", "/");
                if (textBox_path.Text.StartsWith(current_dir))
                    textBox_path.Text = textBox_path.Text.Replace(current_dir, ".");
            }
        }
    }
}
