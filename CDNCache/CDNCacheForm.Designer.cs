namespace CDN
{
    partial class CDNCacheForm
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
            this.clientTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.refreshButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logListBox = new System.Windows.Forms.ListBox();
            this.remoteIpAddressControl = new CDN.IPAddressControl();
            this.localIpAddressControl = new CDN.IPAddressControl();
            this.claerButton = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientTreeView
            // 
            this.clientTreeView.Location = new System.Drawing.Point(17, 15);
            this.clientTreeView.Name = "clientTreeView";
            this.clientTreeView.Size = new System.Drawing.Size(128, 297);
            this.clientTreeView.TabIndex = 0;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target Address";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.claerButton);
            this.panel2.Controls.Add(this.refreshButton);
            this.panel2.Controls.Add(this.clientTreeView);
            this.panel2.Location = new System.Drawing.Point(7, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(157, 344);
            this.panel2.TabIndex = 10;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(17, 318);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(58, 23);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.remoteIpAddressControl);
            this.panel1.Controls.Add(this.localIpAddressControl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.connectButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(189, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 82);
            this.panel1.TabIndex = 9;
            // 
            // logListBox
            // 
            this.logListBox.FormattingEnabled = true;
            this.logListBox.ItemHeight = 12;
            this.logListBox.Location = new System.Drawing.Point(189, 100);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(517, 256);
            this.logListBox.TabIndex = 12;
            // 
            // remoteIpAddressControl
            // 
            this.remoteIpAddressControl.Location = new System.Drawing.Point(99, 47);
            this.remoteIpAddressControl.Name = "remoteIpAddressControl";
            this.remoteIpAddressControl.ReadOnly = false;
            this.remoteIpAddressControl.Size = new System.Drawing.Size(319, 27);
            this.remoteIpAddressControl.TabIndex = 7;
            this.remoteIpAddressControl.Value = null;
            // 
            // localIpAddressControl
            // 
            this.localIpAddressControl.Location = new System.Drawing.Point(99, 13);
            this.localIpAddressControl.Name = "localIpAddressControl";
            this.localIpAddressControl.ReadOnly = true;
            this.localIpAddressControl.Size = new System.Drawing.Size(319, 27);
            this.localIpAddressControl.TabIndex = 6;
            this.localIpAddressControl.Value = null;
            // 
            // claerButton
            // 
            this.claerButton.Location = new System.Drawing.Point(82, 318);
            this.claerButton.Name = "claerButton";
            this.claerButton.Size = new System.Drawing.Size(63, 23);
            this.claerButton.TabIndex = 2;
            this.claerButton.Text = "Clear";
            this.claerButton.UseVisualStyleBackColor = true;
            this.claerButton.Click += new System.EventHandler(this.claerButton_Click);
            // 
            // CDNCacheForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 368);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CDNCacheForm";
            this.Text = "CDNCache";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView clientTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Panel panel1;
        private CDN.IPAddressControl localIpAddressControl;
        private System.Windows.Forms.ListBox logListBox;
        public IPAddressControl remoteIpAddressControl;
        private System.Windows.Forms.Button claerButton;
    }
}

