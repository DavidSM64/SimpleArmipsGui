namespace armipsSimpleGui
{
    partial class Form8
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form8));
            this.panel_buttons = new System.Windows.Forms.Panel();
            this.panel_boxes = new System.Windows.Forms.Panel();
            this.addExeButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_buttons
            // 
            this.panel_buttons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_buttons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_buttons.Controls.Add(this.saveButton);
            this.panel_buttons.Controls.Add(this.cancelButton);
            this.panel_buttons.Controls.Add(this.addExeButton);
            this.panel_buttons.Location = new System.Drawing.Point(0, 0);
            this.panel_buttons.Name = "panel_buttons";
            this.panel_buttons.Size = new System.Drawing.Size(496, 24);
            this.panel_buttons.TabIndex = 12;
            // 
            // panel_boxes
            // 
            this.panel_boxes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_boxes.AutoScroll = true;
            this.panel_boxes.BackColor = System.Drawing.Color.Transparent;
            this.panel_boxes.Location = new System.Drawing.Point(0, 25);
            this.panel_boxes.Name = "panel_boxes";
            this.panel_boxes.Size = new System.Drawing.Size(496, 304);
            this.panel_boxes.TabIndex = 13;
            // 
            // addExeButton
            // 
            this.addExeButton.Location = new System.Drawing.Point(1, 1);
            this.addExeButton.Name = "addExeButton";
            this.addExeButton.Size = new System.Drawing.Size(91, 23);
            this.addExeButton.TabIndex = 0;
            this.addExeButton.Text = "Add Executable";
            this.addExeButton.UseVisualStyleBackColor = true;
            this.addExeButton.Click += new System.EventHandler(this.addExeButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(441, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(54, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(379, 1);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(63, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 329);
            this.Controls.Add(this.panel_boxes);
            this.Controls.Add(this.panel_buttons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Executables";
            this.panel_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_buttons;
        private System.Windows.Forms.Panel panel_boxes;
        private System.Windows.Forms.Button addExeButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}