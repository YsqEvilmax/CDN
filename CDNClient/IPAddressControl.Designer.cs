namespace CDN
{
    partial class IPAddressControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.firstTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.secondTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.thirdTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fourthTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // firstTextBox
            // 
            this.firstTextBox.Location = new System.Drawing.Point(8, 3);
            this.firstTextBox.MaxLength = 3;
            this.firstTextBox.Name = "firstTextBox";
            this.firstTextBox.Size = new System.Drawing.Size(35, 21);
            this.firstTextBox.TabIndex = 0;
            this.firstTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = ".";
            // 
            // secondTextBox
            // 
            this.secondTextBox.Location = new System.Drawing.Point(66, 3);
            this.secondTextBox.MaxLength = 3;
            this.secondTextBox.Name = "secondTextBox";
            this.secondTextBox.Size = new System.Drawing.Size(35, 21);
            this.secondTextBox.TabIndex = 1;
            this.secondTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = ".";
            // 
            // thirdTextBox
            // 
            this.thirdTextBox.Location = new System.Drawing.Point(124, 3);
            this.thirdTextBox.MaxLength = 3;
            this.thirdTextBox.Name = "thirdTextBox";
            this.thirdTextBox.Size = new System.Drawing.Size(35, 21);
            this.thirdTextBox.TabIndex = 2;
            this.thirdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = ":";
            // 
            // fourthTextBox
            // 
            this.fourthTextBox.Location = new System.Drawing.Point(182, 3);
            this.fourthTextBox.MaxLength = 3;
            this.fourthTextBox.Name = "fourthTextBox";
            this.fourthTextBox.Size = new System.Drawing.Size(35, 21);
            this.fourthTextBox.TabIndex = 3;
            this.fourthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(241, 2);
            this.portTextBox.MaxLength = 5;
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(68, 21);
            this.portTextBox.TabIndex = 4;
            this.portTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // IPAddressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fourthTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.thirdTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.secondTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.firstTextBox);
            this.Name = "IPAddressControl";
            this.Size = new System.Drawing.Size(319, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox firstTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox secondTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox thirdTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox fourthTextBox;
        private System.Windows.Forms.TextBox portTextBox;
    }
}
