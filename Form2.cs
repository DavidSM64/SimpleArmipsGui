using System;
using System.Drawing;
using System.Windows.Forms;

namespace armipsSimpleGui
{
    public partial class Form2 : Form
    {

        public static string consoleText = "";

        public Form2(string text)
        {
            InitializeComponent();
            consoleText = text;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            consoleTextBox.Text = consoleText;
            int lineCount = 0;
            foreach (var line in consoleTextBox.Lines)
            {
                consoleTextBox.Select(consoleTextBox.GetFirstCharIndexFromLine(lineCount), line.Length);
                consoleTextBox.SelectionBackColor = 
                    (lineCount % 2 == 0) ? Color.FromArgb(255,230,230,230) : Color.FromArgb(255, 215, 215, 215);
                lineCount++;
            }
            consoleTextBox.WordWrap = true;
        }
    }
}
