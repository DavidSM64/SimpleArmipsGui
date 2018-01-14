namespace armipsSimpleGui
{
    partial class Form7
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            this.listBox_profiles = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.make_xml_patch_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_addBoxes = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_profiles
            // 
            this.listBox_profiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_profiles.FormattingEnabled = true;
            this.listBox_profiles.Location = new System.Drawing.Point(397, 26);
            this.listBox_profiles.Name = "listBox_profiles";
            this.listBox_profiles.Size = new System.Drawing.Size(106, 199);
            this.listBox_profiles.TabIndex = 0;
            this.listBox_profiles.SelectedIndexChanged += new System.EventHandler(this.listBox_profiles_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(397, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select XML profile";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // make_xml_patch_button
            // 
            this.make_xml_patch_button.Enabled = false;
            this.make_xml_patch_button.Location = new System.Drawing.Point(1, 1);
            this.make_xml_patch_button.Name = "make_xml_patch_button";
            this.make_xml_patch_button.Size = new System.Drawing.Size(109, 22);
            this.make_xml_patch_button.TabIndex = 2;
            this.make_xml_patch_button.Text = "Make XML patch";
            this.make_xml_patch_button.UseVisualStyleBackColor = true;
            this.make_xml_patch_button.Click += new System.EventHandler(this.make_xml_patch_button_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.make_xml_patch_button);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 24);
            this.panel1.TabIndex = 3;
            // 
            // panel_addBoxes
            // 
            this.panel_addBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_addBoxes.AutoScroll = true;
            this.panel_addBoxes.BackColor = System.Drawing.Color.Transparent;
            this.panel_addBoxes.Location = new System.Drawing.Point(1, 25);
            this.panel_addBoxes.Name = "panel_addBoxes";
            this.panel_addBoxes.Size = new System.Drawing.Size(395, 200);
            this.panel_addBoxes.TabIndex = 4;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 226);
            this.Controls.Add(this.panel_addBoxes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBox_profiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create XML Patch";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_profiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button make_xml_patch_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_addBoxes;
    }
}