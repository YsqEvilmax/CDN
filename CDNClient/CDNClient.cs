using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CDN;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace CDN
{
    class CDNClient : CDNNetWork
    {
        public CDNClient(IPEndPoint point)
            : base(point)
        { }

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
                            fsd = Serializer<FileSystemDepository>.Deserialize(msg.content);
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
    }
}
