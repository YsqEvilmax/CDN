using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDN
{
    public partial class CDNClientForm : Form
    {
        public CDNClientForm()
        {
            InitializeComponent();
            server = new CDNClient(CDNNetWork.GetLocalIPPoint(), this);
            server.Start();
            server.Idle();
            this.localIPAddressControl.Value = server.LocalEndpoint as IPEndPoint;
            this.localIPAddressControl.ReadOnly = true;
            this.listTreeView.Nodes.Add(server.root);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(targetIPAddressControl.Value);
                    if (client.Connected)
                    {
                        NetworkStream ns = client.GetStream();
                        using (StreamWriter sw = new StreamWriter(ns))
                        {
                            CDNMessage msg = new CDNMessage(server.LocalEndpoint);
                            msg.Fill(CDNMessage.MSGID.TEST, "");
                            sw.Write(Serializer<CDNMessage>.Serialize<SoapFormatter>(msg));
                        }
                    }
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show("Fail to connect:" + exp.Message);
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(targetIPAddressControl.Value);
                    if (client.Connected)
                    {
                        NetworkStream ns = client.GetStream();
                        using (StreamWriter sw = new StreamWriter(ns))
                        {
                            CDNMessage msg = new CDNMessage(server.LocalEndpoint);
                            msg.Fill(CDNMessage.MSGID.SHOW, "");
                            sw.Write(Serializer<CDNMessage>.Serialize<SoapFormatter>(msg));
                        }
                    }
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show("Fail to refresh:" + exp.Message);
            }
        }

        public TreeView list
        {
            get { return this.listTreeView; }
            set { this.listTreeView = value; }
        }

        private CDNClient server;
    }
}
