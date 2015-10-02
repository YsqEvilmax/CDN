using System;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Soap;

namespace CDN
{
    class CDNClient : CDNNetWork
    {
        public CDNClient(IPEndPoint point)
            : base(point)
        { }

        public CDNClient(IPEndPoint point, Form ui)
            : this(point)
        {
            this.ui = ui;
        }
        protected override async Task Handle(CDNMessage msg)
        {
            try
            {
                switch (msg.id)
                {
                    case CDNMessage.MSGID.TEST:
                        {
                            MessageBox.Show("Successfully connected!");
                        }
                        break;
                    case CDNMessage.MSGID.SHOW:
                        {                       
                            root = Serializer<DirectoryNode>.Deserialize<SoapFormatter>(msg.content);
                            TreeView v = (ui as CDNClientForm).list;
                            v.Nodes.Clear();
                            v.Nodes.Add(root);
                        }
                        break;
                    case CDNMessage.MSGID.DOWNLOAD:
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private Form ui;
    }
}
