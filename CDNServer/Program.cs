using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDNServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a valid file address to bulid a FileSystem depository!");
            String path = Console.ReadLine();
            FileSystemDepository fsd = new FileSystemDepository(path);
            Console.WriteLine(fsd.ToString());
            Console.ReadLine();
        }
    }
}
