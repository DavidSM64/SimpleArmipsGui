namespace armipsSimpleGui
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.label1 = new System.Windows.Forms.Label();
            this.libs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.fileRamBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.downBut = new System.Windows.Forms.Button();
            this.upBut = new System.Windows.Forms.Button();
            this.useASMasRootDir = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Library includes";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // libs
            // 
            this.libs.AutoArrange = false;
            this.libs.CheckBoxes = true;
            this.libs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.libs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.libs.HideSelection = false;
            this.libs.LabelWrap = false;
            this.libs.Location = new System.Drawing.Point(7, 69);
            this.libs.MultiSelect = false;
            this.libs.Name = "libs";
            this.libs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.libs.ShowGroups = false;
            this.libs.Size = new System.Drawing.Size(448, 100);
            this.libs.TabIndex = 0;
            this.libs.TabStop = false;
            this.libs.TileSize = new System.Drawing.Size(100, 16);
            this.libs.UseCompatibleStateImageBehavior = false;
            this.libs.View = System.Windows.Forms.View.List;
            this.libs.SelectedIndexChanged += new System.EventHandler(this.libs_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 160;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "File RAM address: 0x";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // fileRamBox
            // 
            this.fileRamBox.Location = new System.Drawing.Point(108, 9);
            this.fileRamBox.MaxLength = 8;
            this.fileRamBox.Name = "fileRamBox";
            this.fileRamBox.Size = new System.Drawing.Size(67, 20);
            this.fileRamBox.TabIndex = 4;
            this.fileRamBox.TabStop = false;
            this.fileRamBox.Text = "00000000";
            this.fileRamBox.WordWrap = false;
            this.fileRamBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(275, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Pre-Lib ASM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(380, 175);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(300, 175);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(368, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Post-Lib ASM";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // downBut
            // 
            this.downBut.Enabled = false;
            this.downBut.Location = new System.Drawing.Point(88, 175);
            this.downBut.Name = "downBut";
            this.downBut.Size = new System.Drawing.Size(75, 23);
            this.downBut.TabIndex = 9;
            this.downBut.Text = "Move Down";
            this.downBut.UseVisualStyleBackColor = true;
            this.downBut.Click += new System.EventHandler(this.downBut_Click);
            // 
            // upBut
            // 
            this.upBut.Enabled = false;
            this.upBut.Location = new System.Drawing.Point(7, 175);
            this.upBut.Name = "upBut";
            this.upBut.Size = new System.Drawing.Size(75, 23);
            this.upBut.TabIndex = 10;
            this.upBut.Text = "Move Up";
            this.upBut.UseVisualStyleBackColor = true;
            this.upBut.Click += new System.EventHandler(this.upBut_Click);
            // 
            // useASMasRootDir
            // 
            this.useASMasRootDir.AutoSize = true;
            this.useASMasRootDir.Checked = true;
            this.useASMasRootDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useASMasRootDir.Location = new System.Drawing.Point(7, 32);
            this.useASMasRootDir.Name = "useASMasRootDir";
            this.useASMasRootDir.Size = new System.Drawing.Size(194, 17);
            this.useASMasRootDir.TabIndex = 11;
            this.useASMasRootDir.Text = "Use ASM file folder as root directory";
            this.useASMasRootDir.UseVisualStyleBackColor = true;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 205);
            this.Controls.Add(this.useASMasRootDir);
            this.Controls.Add(this.upBut);
            this.Controls.Add(this.downBut);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fileRamBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.libs);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView libs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileRamBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button downBut;
        private System.Windows.Forms.Button upBut;
        private System.Windows.Forms.CheckBox useASMasRootDir;
    }
}