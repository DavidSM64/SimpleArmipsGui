using System;
using System.Windows.Forms;

namespace armipsSimpleGui
{
    public partial class Form3 : Form
    {
        private main form;

        public Form3(main f)
        {
            InitializeComponent();
            form = f;
            addParams.Text = form.getAdditionalParameters();
        }

        private void okBut_Click(object sender, EventArgs e)
        {
            form.setAdditionalParameters(addParams.Text);
            Close();
        }

        private void canBut_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
