using System;
using System.Windows.Forms;

namespace armipsSimpleGui
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            textBox1.Text = Settings.preASM;
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.preASM = textBox1.Text;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
