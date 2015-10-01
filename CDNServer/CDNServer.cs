using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CDN;

namespace CDN
{
    class CDNServer : CDNNetWork
    {
        public CDNServer(IPEndPoint point)
            : base(point)
        {
        }

        protected override async Task Handle(CDNMessage msg)
        {
            try
            {
                TcpClient client = new TcpClient(msg.address, msg.port);
                CDNMessage newMsg = msg.Clone() as CDNMessage;            
                NetworkStream ns = client.GetStream();
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    switch (msg.id)
                    {
                        case CDNMessage.MSGID.TEST:
                            {
                            }
                            break;
                        case CDNMessage.MSGID.SHOW:
                            {
                                newMsg.content = Serializer<FileSystemDepository>.Serialize(fsd);
                            }
                            break;
                        case CDNMessage.MSGID.DOWNLOAD:
                            {

                            }
                            break;
                        default:
                            break;
                    }
                    String context = Serializer<CDNMessage>.Serialize(newMsg);
                    sw.Write(context);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
