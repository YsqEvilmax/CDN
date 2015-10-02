using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;

namespace CDN
{
    class CDNServer : CDNNetWork
    {
        public CDNServer(IPEndPoint point)
            : base(point)
        {
            root.Scan();
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
                                newMsg.content = Serializer<DirectoryNode>.Serialize<SoapFormatter>(root);
                                Console.WriteLine("{0}", newMsg.content.Length);
                            }
                            break;
                        case CDNMessage.MSGID.DOWNLOAD:
                            {

                            }
                            break;
                        default:
                            break;
                    }
                    String context = Serializer<CDNMessage>.Serialize<SoapFormatter>(newMsg);
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
