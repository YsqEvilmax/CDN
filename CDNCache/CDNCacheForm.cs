using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDN
{
    public partial class CDNCacheForm : Form
    {
        public CDNCacheForm()
        {
            InitializeComponent();
            server = new CDNCache(CDNNetWork.GetLocalIPPoint(), this);
            server.Start();
            server.Idle();
            this.localIpAddressControl.Value = server.LocalEndpoint as IPEndPoint;
            this.localIpAddressControl.ReadOnly = true;
            this.clientTreeView.Nodes.Add(server.localRoot);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            CDNMessage msg = new CDNMessage();
            msg.Fill(CDNMessage.MSGID.TEST);
            server.Send(remoteIpAddressControl.Value, msg);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            server.localRoot.Scan();
        }

        private void claerButton_Click(object sender, EventArgs e)
        {
            server.localRoot.Clear();
        }

        private CDNCache server;
    }
}
