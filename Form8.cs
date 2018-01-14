using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace armipsSimpleGui
{
    public partial class Form8 : Form
    {
        private main m;
        private int yPos = 0;
        public Form8(main m)
        {
            InitializeComponent();
            this.m = m;
            createBoxes();
        }
        
        private void createBoxes()
        {
            panel_boxes.Controls.Clear();
            yPos = 0;

            for (int i = 0; i < m.executables.Count; i++)
                createBox(m.executables[i], i, m.executables.Count);
        }

        private void updateBoxesIfMajorChange(int oldSize, int newSize)
        {
            bool AddScrollBars = ((oldSize * 60) < panel_boxes.Height && (newSize * 60) > panel_boxes.Height);
            bool RemoveScrollBars = ((oldSize * 60) > panel_boxes.Height && (newSize * 60) < panel_boxes.Height);

            if (AddScrollBars || RemoveScrollBars)
            {
                //Console.WriteLine("Updating scrollbars!");
                updateBoxList();
            }
        }

        private void updateBoxList()
        {
            EXECUTABLE[] exes = getExecutablesFromBoxes();
            panel_boxes.Controls.Clear();
            yPos = 0;

            for (int i = 0; i < exes.Length; i++)
                createBox(exes[i], i, exes.Length);
        }

        private void createBox(EXECUTABLE exe, int i, int new_total)
        {
            bool scrollBarWillShow = (new_total * 60) > panel_boxes.Height;
            Panel newPanel = new Panel();
            newPanel.BorderStyle = BorderStyle.FixedSingle;
            newPanel.Location = new Point(1, yPos);
            newPanel.Size = new Size(panel_boxes.Width - (scrollBarWillShow ? 20 : 2), 60);
            panel_boxes.Controls.Add(newPanel);
            newPanel.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);

            if (!exe.Enabled)
                newPanel.BackColor = Color.LightGray;

            Label nameLabel = new Label();
            nameLabel.Text = exe.Name;
            if (nameLabel.Text.Length > 30)
                nameLabel.Text = nameLabel.Text.Substring(0, 30) + "...";
            nameLabel.Location = new Point(1, 1);
            nameLabel.BorderStyle = BorderStyle.FixedSingle;
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Courier New", 14, FontStyle.Bold);
            newPanel.Controls.Add(nameLabel);
            nameLabel.Tag = exe.Path;

            Button editButton = new Button();
            editButton.Text = "Edit";
            editButton.Location = new Point(nameLabel.Width + 2, 1);
            editButton.Size = new Size(40, nameLabel.Height);
            editButton.Tag = i;
            editButton.Click += EditButton_Click;
            if (nameLabel.Text.ToUpper().Equals("ARMIPS"))
                editButton.Visible = false;
            newPanel.Controls.Add(editButton);

            Label argsLabel = new Label();
            argsLabel.Text = "Arguments:";
            argsLabel.Location = new Point(1, nameLabel.Height + 10);
            argsLabel.Font = new Font("Consolas", 9, FontStyle.Bold);
            argsLabel.AutoSize = true;
            if (nameLabel.Text.ToUpper().Equals("ARMIPS"))
                argsLabel.Visible = false;
            newPanel.Controls.Add(argsLabel);

            TextBox argsBox = new TextBox();
            argsBox.Text = exe.Arguments;
            argsBox.Location = new Point(argsLabel.Width + 2, nameLabel.Height + 8);
            argsBox.Size = new Size(panel_boxes.Width - 80 - argsBox.Location.X, argsLabel.Height);

            if (nameLabel.Text.ToUpper().Equals("ARMIPS"))
                argsBox.Visible = false;
            newPanel.Controls.Add(argsBox);

            Button moveUp = new Button();
            moveUp.Text = "▲";
            moveUp.AutoSize = false;
            moveUp.Size = new Size(20, 20);
            moveUp.Location = new Point(newPanel.Width - 26, 8);
            moveUp.Tag = i;
            moveUp.Click += moveEntryUp;
            if (i == 0)
                moveUp.Enabled = false;
            newPanel.Controls.Add(moveUp);

            Button moveDown = new Button();
            moveDown.Text = "▼";
            moveDown.AutoSize = false;
            moveDown.Size = new Size(20, 20);
            moveDown.Location = new Point(newPanel.Width - 26, 28);
            moveDown.Tag = i;
            moveDown.Click += moveEntryDown;
            if (i == m.executables.Count - 1)
                moveDown.Enabled = false;
            newPanel.Controls.Add(moveDown);

            int cb_xOff = (argsBox.Location.X + argsBox.Width);
            cb_xOff += (moveUp.Location.X - cb_xOff) / 2;
            CheckBox enableExecCB = new CheckBox();
            enableExecCB.Checked = exe.Enabled;
            enableExecCB.AutoSize = false;
            enableExecCB.Size = new Size(13, 20);
            enableExecCB.Location = new Point(cb_xOff - (enableExecCB.Width / 2), 18);
            enableExecCB.Tag = i;
            enableExecCB.CheckedChanged += EnableExecCB_CheckedChanged;
            if (nameLabel.Text.ToUpper().Equals("ARMIPS"))
                enableExecCB.Visible = false;
            newPanel.Controls.Add(enableExecCB);

            yPos += newPanel.Height;
        }

        private void EnableExecCB_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int index = (int)cb.Tag;

            if (cb.Checked)
                panel_boxes.Controls[index].BackColor = Color.Transparent;
            else
                panel_boxes.Controls[index].BackColor = Color.LightGray;
        }

        private void UpdateUpDownButtons()
        {
            for (int i = 0; i < panel_boxes.Controls.Count; i++)
            {
                if (i == 0)
                    panel_boxes.Controls[i].Controls[4].Enabled = false;
                else
                    panel_boxes.Controls[i].Controls[4].Enabled = true;

                if (i == panel_boxes.Controls.Count - 1)
                    panel_boxes.Controls[i].Controls[5].Enabled = false;
                else
                    panel_boxes.Controls[i].Controls[5].Enabled = true;
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string exe_path = panel_boxes.Controls[(int)b.Tag].Controls[0].Tag.ToString();
            Form9 edit_exe = new Form9(exe_path);
            edit_exe.ShowDialog();
            if (edit_exe.do_delete)
            {
                b.Parent.Parent.Controls.RemoveAt((int)b.Tag);
                updateBoxList();
                UpdateUpDownButtons();
            }
            else
            {
                Label label = (Label)panel_boxes.Controls[(int)b.Tag].Controls[0];
                label.Tag = edit_exe.path_string;

                if (label.Tag.ToString().LastIndexOf("/") != -1)
                    label.Text = edit_exe.path_string.Substring(edit_exe.path_string.LastIndexOf("/") + 1);
                else
                    label.Text = edit_exe.path_string;

                panel_boxes.Controls[(int)b.Tag].Controls[1].Location =
                    new Point(label.Width + 2, panel_boxes.Controls[(int)b.Tag].Controls[1].Location.Y);
            }
        }

        private void addExeButton_Click(object sender, System.EventArgs e)
        {
            EXECUTABLE add = new EXECUTABLE();

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Executable files (*.exe, *.bat) | *.exe; *.bat |Any files |*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                add.Path = file.FileName.Replace("\\", "/");
                string current_dir = Directory.GetCurrentDirectory().Replace("\\", "/");
                if (add.Path.StartsWith(current_dir))
                    add.Path = add.Path.Replace(current_dir, ".");
                //Console.WriteLine(add.Path);
                add.Arguments = "";
                add.Enabled = true;
                if (add.Path.LastIndexOf("/") != -1)
                    add.Name = add.Path.Substring(add.Path.LastIndexOf("/") + 1);
                else
                    add.Name = add.Path;
            }
            else
                return;

            int oldSize = panel_boxes.Controls.Count;
            createBox(add, oldSize, oldSize + 1);
            updateBoxesIfMajorChange(oldSize, oldSize + 1);
            UpdateUpDownButtons();
        }

        private EXECUTABLE[] getExecutablesFromBoxes()
        {
            EXECUTABLE[] exe_arr = new EXECUTABLE[panel_boxes.Controls.Count];
            for(int i = 0; i < panel_boxes.Controls.Count; i++)
            {
                EXECUTABLE exe = new EXECUTABLE();
                exe.Name = panel_boxes.Controls[i].Controls[0].Text;
                exe.Path = panel_boxes.Controls[i].Controls[0].Tag.ToString();
                exe.Arguments = panel_boxes.Controls[i].Controls[3].Text;
                exe.Enabled = ((CheckBox)panel_boxes.Controls[i].Controls[6]).Checked;

                exe_arr[i] = exe;
            }
            return exe_arr;
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            m.executables.Clear();
            m.executables.AddRange(getExecutablesFromBoxes());
            exeConfig.writeExecutables(m.executables);
            Hide();
        }

        private void swapEntries(int index0, int index1)
        {
            if (index0 < 0 || index1 < 0 || index0 >= panel_boxes.Controls.Count 
                || index1 >= panel_boxes.Controls.Count)
                return;

            Control ctrl_0 = panel_boxes.Controls[index0];
            Control ctrl_1 = panel_boxes.Controls[index1];

            string name_temp = ctrl_0.Controls[0].Text;
            string path_temp = ctrl_0.Controls[0].Tag.ToString();
            string args_temp = ctrl_0.Controls[3].Text;
            bool enabled_temp = ((CheckBox)ctrl_0.Controls[6]).Checked;
            Point editbut_pos_temp = ctrl_0.Controls[1].Location;

            ctrl_0.Controls[0].Text = ctrl_1.Controls[0].Text;
            ctrl_0.Controls[0].Tag = ctrl_1.Controls[0].Tag.ToString();
            ctrl_0.Controls[3].Text = ctrl_1.Controls[3].Text;
            ((CheckBox)ctrl_0.Controls[6]).Checked = ((CheckBox)ctrl_1.Controls[6]).Checked;
            ctrl_0.Controls[1].Location = ctrl_1.Controls[1].Location;

            ctrl_1.Controls[0].Text = name_temp;
            ctrl_1.Controls[0].Tag = path_temp;
            ctrl_1.Controls[3].Text = args_temp;
            ((CheckBox)ctrl_1.Controls[6]).Checked = enabled_temp;
            ctrl_1.Controls[1].Location = editbut_pos_temp;


            bool edit_but_vis_temp = ctrl_0.Controls[1].Visible;
            ctrl_0.Controls[1].Visible = ctrl_1.Controls[1].Visible;
            ctrl_0.Controls[2].Visible = ctrl_1.Controls[1].Visible;
            ctrl_0.Controls[3].Visible = ctrl_1.Controls[1].Visible;
            ctrl_0.Controls[6].Visible = ctrl_1.Controls[1].Visible;
            ctrl_1.Controls[1].Visible = edit_but_vis_temp;
            ctrl_1.Controls[2].Visible = edit_but_vis_temp;
            ctrl_1.Controls[3].Visible = edit_but_vis_temp;
            ctrl_1.Controls[6].Visible = edit_but_vis_temp;
        }

        private void moveEntryUp(object sender, System.EventArgs e)
        {
            int index = (int)((Button)sender).Tag;
            swapEntries(index, index - 1);
        }

        private void moveEntryDown(object sender, System.EventArgs e)
        {
            int index = (int)((Button)sender).Tag;
            swapEntries(index, index + 1);
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            Hide();
        }
    }
}
