using System;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace CDN
{
    class CDNClient : CDNNetWork
    {
        public CDNClient(IPEndPoint point)
            : base(point, "./client")
        {
            type = CNDTYPE.CLIENT;
            localRoot.Scan();
        }

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
                            remoteRoot = Serializer<DirectoryNode>.Deserialize<SoapFormatter>(msg.content);
                            TreeView v = (ui as CDNClientForm).serverTreeView;
                            v.Nodes.Clear();
                            v.Nodes.Add(remoteRoot);
                        }
                        break;
                    case CDNMessage.MSGID.DOWNLOAD:
                        {
                            String fileName = msg.content.Substring(0, msg.content.IndexOf("|||"));                    
                            String fileContent = msg.content.Substring(msg.content.IndexOf("|||") + 3);
                            using (FileStream fs = new FileStream(localRoot.info.FullName +"\\"+ fileName, FileMode.Create, FileAccess.Write))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    sw.Write(fileContent);
                                }
                            }
                            localRoot.Scan();
                            TreeView v = (ui as CDNClientForm).clientTreeView;
                            v.Nodes.Clear();
                            v.Nodes.Add(localRoot);
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
        public DirectoryNode remoteRoot { get; protected set; }
        private Form ui;
    }
}
