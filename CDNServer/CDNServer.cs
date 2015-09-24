using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CDNServer
{
    class CDNServer : TcpListener
    {
        public CDNServer(IPAddress localaddr, int port)
            : base(localaddr, port)
        {
            fsd = new FileSystemDepository("./");
        }

        public async Task Handle()
        {
            TcpClient client = await AcceptTcpClientAsync();
            NetworkStream ns = new NetworkStream(client.Client);
        }

        private FileSystemDepository fsd;
    }
}
