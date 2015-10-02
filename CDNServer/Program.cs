using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDN;

namespace CDN
{
    class Program
    {
        static void Main(string[] args)
        {
            CDNServer server = new CDNServer(CDNNetWork.GetLocalIPPoint());
            server.Start();
            server.Idle();
            Console.WriteLine(server);
            Console.ReadLine();
        }
    }
}
