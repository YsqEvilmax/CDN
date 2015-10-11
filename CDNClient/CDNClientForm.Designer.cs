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
            this.serverTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clientTreeView = new System.Windows.Forms.TreeView();
            this.refreshButton = new System.Windows.Forms.Button();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.remoteIpAddressControl = new CDN.IPAddressControl();
            this.localIpAddressControl = new CDN.IPAddressControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverTreeView
            // 
            this.serverTreeView.Location = new System.Drawing.Point(17, 15);
            this.serverTreeView.Name = "serverTreeView";
            this.serverTreeView.Size = new System.Drawing.Size(88, 276);
            this.serverTreeView.TabIndex = 0;
            this.serverTreeView.DoubleClick += new System.EventHandler(this.serverFile_DbClick);
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
            this.panel1.Controls.Add(this.remoteIpAddressControl);
            this.panel1.Controls.Add(this.localIpAddressControl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.connectButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(226, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 82);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.clientTreeView);
            this.panel2.Controls.Add(this.refreshButton);
            this.panel2.Controls.Add(this.serverTreeView);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 344);
            this.panel2.TabIndex = 7;
            // 
            // clientTreeView
            // 
            this.clientTreeView.Location = new System.Drawing.Point(111, 17);
            this.clientTreeView.Name = "clientTreeView";
            this.clientTreeView.Size = new System.Drawing.Size(88, 276);
            this.clientTreeView.TabIndex = 2;
            this.clientTreeView.DoubleClick += new System.EventHandler(this.displayFile_DbClick);
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
            // fileTextBox
            // 
            this.fileTextBox.Location = new System.Drawing.Point(227, 101);
            this.fileTextBox.Multiline = true;
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.ReadOnly = true;
            this.fileTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.fileTextBox.Size = new System.Drawing.Size(514, 255);
            this.fileTextBox.TabIndex = 8;
            // 
            // remoteIpAddressControl
            // 
            this.remoteIpAddressControl.Location = new System.Drawing.Point(104, 43);
            this.remoteIpAddressControl.Name = "remoteIpAddressControl";
            this.remoteIpAddressControl.ReadOnly = false;
            this.remoteIpAddressControl.Size = new System.Drawing.Size(319, 27);
            this.remoteIpAddressControl.TabIndex = 7;
            this.remoteIpAddressControl.Value = null;
            // 
            // localIpAddressControl
            // 
            this.localIpAddressControl.Location = new System.Drawing.Point(104, 8);
            this.localIpAddressControl.Name = "localIpAddressControl";
            this.localIpAddressControl.ReadOnly = false;
            this.localIpAddressControl.Size = new System.Drawing.Size(319, 27);
            this.localIpAddressControl.TabIndex = 6;
            this.localIpAddressControl.Value = null;
            // 
            // CDNClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 368);
            this.Controls.Add(this.fileTextBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CDNClientForm";
            this.Text = "CDNClientForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button refreshButton;
        public System.Windows.Forms.TreeView serverTreeView;
        public System.Windows.Forms.TreeView clientTreeView;
        private IPAddressControl remoteIpAddressControl;
        private IPAddressControl localIpAddressControl;
        private System.Windows.Forms.TextBox fileTextBox;
    }
}

