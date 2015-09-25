using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CDNCommon;
using System.Net.Sockets;
using System.IO;

namespace CDN
{
    class CDNClient : CDNNetWork
    {
        public CDNClient(IPAddress localaddr, int port)
            : base(localaddr, port)
        { }

        protected override async Task Handle(CDNMessage msg)
        {
            try
            {
                switch (msg.id)
                {
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
