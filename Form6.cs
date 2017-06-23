using System.Windows.Forms;

namespace armipsSimpleGui
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            textBox2.Text = Settings.postASM;
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.postASM = textBox2.Text;
        }
    }
}
