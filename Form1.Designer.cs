namespace armipsSimpleGui
{
    partial class main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.asmTextBox = new System.Windows.Forms.TextBox();
            this.browseASM = new System.Windows.Forms.Button();
            this.browseROM = new System.Windows.Forms.Button();
            this.romTextBox = new System.Windows.Forms.TextBox();
            this.assemble = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.xmlTweakButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboSelectProfile_label = new System.Windows.Forms.Label();
            this.comboSelectProfile = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ROM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "ASM File:";
            // 
            // asmTextBox
            // 
            this.asmTextBox.Location = new System.Drawing.Point(77, 80);
            this.asmTextBox.Name = "asmTextBox";
            this.asmTextBox.ReadOnly = true;
            this.asmTextBox.Size = new System.Drawing.Size(314, 20);
            this.asmTextBox.TabIndex = 2;
            this.asmTextBox.TabStop = false;
            // 
            // browseASM
            // 
            this.browseASM.Location = new System.Drawing.Point(400, 75);
            this.browseASM.Name = "browseASM";
            this.browseASM.Size = new System.Drawing.Size(110, 32);
            this.browseASM.TabIndex = 3;
            this.browseASM.TabStop = false;
            this.browseASM.Text = "Browse...";
            this.browseASM.UseVisualStyleBackColor = true;
            this.browseASM.Click += new System.EventHandler(this.browseASM_Click);
            // 
            // browseROM
            // 
            this.browseROM.Location = new System.Drawing.Point(400, 34);
            this.browseROM.Name = "browseROM";
            this.browseROM.Size = new System.Drawing.Size(110, 32);
            this.browseROM.TabIndex = 4;
            this.browseROM.TabStop = false;
            this.browseROM.Text = "Browse...";
            this.browseROM.UseVisualStyleBackColor = true;
            this.browseROM.Click += new System.EventHandler(this.browseROM_Click);
            // 
            // romTextBox
            // 
            this.romTextBox.Location = new System.Drawing.Point(77, 40);
            this.romTextBox.Name = "romTextBox";
            this.romTextBox.ReadOnly = true;
            this.romTextBox.Size = new System.Drawing.Size(314, 20);
            this.romTextBox.TabIndex = 0;
            this.romTextBox.TabStop = false;
            // 
            // assemble
            // 
            this.assemble.Location = new System.Drawing.Point(77, 117);
            this.assemble.Name = "assemble";
            this.assemble.Size = new System.Drawing.Size(314, 40);
            this.assemble.TabIndex = 6;
            this.assemble.TabStop = false;
            this.assemble.Text = "Assemble!";
            this.assemble.UseVisualStyleBackColor = true;
            this.assemble.Click += new System.EventHandler(this.assemble_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(400, 117);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(110, 40);
            this.button4.TabIndex = 7;
            this.button4.TabStop = false;
            this.button4.Text = "Additional Arguments";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(11, 117);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(59, 40);
            this.button5.TabIndex = 8;
            this.button5.TabStop = false;
            this.button5.Text = "About";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("settingsButton.BackgroundImage")));
            this.settingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.settingsButton.Location = new System.Drawing.Point(3, 2);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(20, 20);
            this.settingsButton.TabIndex = 9;
            this.settingsButton.TabStop = false;
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // xmlTweakButton
            // 
            this.xmlTweakButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xmlTweakButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.xmlTweakButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlTweakButton.ForeColor = System.Drawing.Color.Black;
            this.xmlTweakButton.Location = new System.Drawing.Point(25, 2);
            this.xmlTweakButton.Name = "xmlTweakButton";
            this.xmlTweakButton.Size = new System.Drawing.Size(45, 20);
            this.xmlTweakButton.TabIndex = 10;
            this.xmlTweakButton.TabStop = false;
            this.xmlTweakButton.Text = "<XML>";
            this.xmlTweakButton.UseVisualStyleBackColor = true;
            this.xmlTweakButton.Click += new System.EventHandler(this.xmlTweakButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.comboSelectProfile_label);
            this.panel1.Controls.Add(this.comboSelectProfile);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.settingsButton);
            this.panel1.Controls.Add(this.xmlTweakButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(519, 24);
            this.panel1.TabIndex = 11;
            // 
            // comboSelectProfile_label
            // 
            this.comboSelectProfile_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSelectProfile_label.AutoSize = true;
            this.comboSelectProfile_label.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboSelectProfile_label.Location = new System.Drawing.Point(416, 6);
            this.comboSelectProfile_label.Name = "comboSelectProfile_label";
            this.comboSelectProfile_label.Size = new System.Drawing.Size(55, 13);
            this.comboSelectProfile_label.TabIndex = 13;
            this.comboSelectProfile_label.Text = "Profile:";
            // 
            // comboSelectProfile
            // 
            this.comboSelectProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSelectProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSelectProfile.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboSelectProfile.FormattingEnabled = true;
            this.comboSelectProfile.ItemHeight = 13;
            this.comboSelectProfile.Location = new System.Drawing.Point(472, 1);
            this.comboSelectProfile.MaxDropDownItems = 10;
            this.comboSelectProfile.Name = "comboSelectProfile";
            this.comboSelectProfile.Size = new System.Drawing.Size(46, 21);
            this.comboSelectProfile.TabIndex = 12;
            this.comboSelectProfile.TabStop = false;
            this.comboSelectProfile.DropDown += new System.EventHandler(this.comboSelectProfile_DropDown);
            this.comboSelectProfile.DropDownClosed += new System.EventHandler(this.comboSelectProfile_DropDownClosed);
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(72, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 20);
            this.button1.TabIndex = 11;
            this.button1.TabStop = false;
            this.button1.Text = "Armips Readme";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(519, 167);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.assemble);
            this.Controls.Add(this.romTextBox);
            this.Controls.Add(this.browseROM);
            this.Controls.Add(this.browseASM);
            this.Controls.Add(this.asmTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Armips GUI v1.3";
            this.Load += new System.EventHandler(this.main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox asmTextBox;
        private System.Windows.Forms.Button browseASM;
        private System.Windows.Forms.Button browseROM;
        private System.Windows.Forms.TextBox romTextBox;
        private System.Windows.Forms.Button assemble;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button xmlTweakButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboSelectProfile;
        private System.Windows.Forms.Label comboSelectProfile_label;
    }
}

