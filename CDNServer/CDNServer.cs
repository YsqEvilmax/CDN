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
            : base(point, "./server")
        {
            localRoot.Scan();
        }

        protected override async Task Handle(CDNMessage msg)
        {
            try
            {
                String content = "";
                switch (msg.id)
                {
                    case CDNMessage.MSGID.TEST:
                        {
                        }
                        break;
                    case CDNMessage.MSGID.SHOW:
                        {
                            localRoot.Scan();
                            content = Serializer<DirectoryNode>.Serialize<SoapFormatter>(localRoot);
                        }
                        break;
                    case CDNMessage.MSGID.DOWNLOAD:
                        {
                            FileNode node = Serializer<FileNode>.Deserialize<SoapFormatter>(msg.content);
                            using (FileStream fs = File.OpenRead(node.info.FullName))
                            {
                                using (StreamReader sr = new StreamReader(fs))
                                {
                                    content = node.info.Name + "|||";
                                    content += sr.ReadToEnd();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
                Send(new IPEndPoint(IPAddress.Parse(msg.address), msg.port), msg.id, content);
            }
            catch(Exception e)
           {
                Console.WriteLine(e);
            }
        }
    }
}
