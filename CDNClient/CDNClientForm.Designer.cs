namespace CDN
{
    partial class CDNClientForm
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
            this.listTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.targetIPAddressControl = new CDN.IPAddressControl();
            this.localIPAddressControl = new CDN.IPAddressControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.refreshButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listTreeView
            // 
            this.listTreeView.Location = new System.Drawing.Point(17, 15);
            this.listTreeView.Name = "listTreeView";
            this.listTreeView.Size = new System.Drawing.Size(88, 276);
            this.listTreeView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target Address";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(429, 8);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(74, 62);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connection Test";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.targetIPAddressControl);
            this.panel1.Controls.Add(this.localIPAddressControl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.connectButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(151, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(522, 82);
            this.panel1.TabIndex = 6;
            // 
            // targetIPAddressControl
            // 
            this.targetIPAddressControl.Location = new System.Drawing.Point(104, 47);
            this.targetIPAddressControl.Name = "targetIPAddressControl";
            this.targetIPAddressControl.ReadOnly = false;
            this.targetIPAddressControl.Size = new System.Drawing.Size(319, 27);
            this.targetIPAddressControl.TabIndex = 7;
            this.targetIPAddressControl.Value = null;
            // 
            // localIPAddressControl
            // 
            this.localIPAddressControl.Location = new System.Drawing.Point(104, 8);
            this.localIPAddressControl.Name = "localIPAddressControl";
            this.localIPAddressControl.ReadOnly = false;
            this.localIPAddressControl.Size = new System.Drawing.Size(319, 27);
            this.localIPAddressControl.TabIndex = 6;
            this.localIPAddressControl.Value = null;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.refreshButton);
            this.panel2.Controls.Add(this.listTreeView);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(123, 344);
            this.panel2.TabIndex = 7;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(17, 305);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(88, 23);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // CDNClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 368);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CDNClientForm";
            this.Text = "CDNClientForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView listTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Panel panel1;
        private IPAddressControl targetIPAddressControl;
        private IPAddressControl localIPAddressControl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button refreshButton;
    }
}

