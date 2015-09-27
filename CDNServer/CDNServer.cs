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
                CDNMessage newMsg = new CDNMessage();
                newMsg.id = msg.id;
                newMsg.from = LocalEndpoint as IPEndPoint;
                newMsg.to = msg.from;
                switch (msg.id)
                {
                    case CDNMessage.MSGID.SHOW:
                        {
                            TcpClient client = new TcpClient(msg.from);
                            NetworkStream ns = client.GetStream();
                            using (StreamWriter sw = new StreamWriter(ns))
                            {
                                newMsg.content = Serializer<FileSystemDepository>.Serialize(fsd);
                                String context = Serializer<CDNMessage>.Serialize(newMsg);
                                await sw.WriteAsync(context);
                            }
                            ns.Close();
                            client.Close();
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

            }
        }
    }
}
