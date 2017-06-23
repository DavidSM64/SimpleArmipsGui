namespace armipsSimpleGui
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.addParams = new System.Windows.Forms.TextBox();
            this.okBut = new System.Windows.Forms.Button();
            this.canBut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addParams
            // 
            this.addParams.Location = new System.Drawing.Point(13, 38);
            this.addParams.Name = "addParams";
            this.addParams.Size = new System.Drawing.Size(419, 20);
            this.addParams.TabIndex = 0;
            // 
            // okBut
            // 
            this.okBut.Location = new System.Drawing.Point(100, 67);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(75, 29);
            this.okBut.TabIndex = 1;
            this.okBut.Text = "Apply";
            this.okBut.UseVisualStyleBackColor = true;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // canBut
            // 
            this.canBut.Location = new System.Drawing.Point(258, 67);
            this.canBut.Name = "canBut";
            this.canBut.Size = new System.Drawing.Size(75, 29);
            this.canBut.TabIndex = 2;
            this.canBut.Text = "Cancel";
            this.canBut.UseVisualStyleBackColor = true;
            this.canBut.Click += new System.EventHandler(this.canBut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Examples: -temp tempfile.txt -sym symfile.sym -erroronwarning";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 104);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.canBut);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.addParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input Additional Arguments";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox addParams;
        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.Button canBut;
        private System.Windows.Forms.Label label1;
    }
}