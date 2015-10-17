using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using System.Linq;
using System.Collections.Generic;

namespace CDN
{
    class CDNServer : CDNNetWork
    {
        public CDNServer(IPEndPoint point)
            : base(point, "./server")
        {
            type = CNDTYPE.SERVER;
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
                    //case CDNMessage.MSGID.DOWNLOAD:
                    //    {
                    //        //如果列表为空，更新列表；如果列表不为空， 则发送block
                    //        List<String> requestSegments = new List<string>();
                    //        requestSegments.Deserialize(msg.content);
                    //        //int part = msg.content.IndexOf("=>");
                    //        //String fileName = msg.content.Substring(0, part);
                    //        //String fragments = msg.content.Substring(part + 2);
                    //        //String[] allFragments = fragments.Split(';');

                    //        //FileNode node = localRoot.Find(fileName) as FileNode;
                    //        //node.Partition();

                    //        //using (FileStream fs = File.OpenRead(node.info.FullName))
                    //        //{
                    //        //    using (StreamReader sr = new StreamReader(fs))
                    //        //    {
                    //        //        content = node.info.Name + "|||";
                    //        //        content += sr.ReadToEnd();
                    //        //    }
                    //        //}
                    //    }
                    //    break;
                    case CDNMessage.MSGID.DOWNLOAD:
                        {
                             FileNode node = Serializer<FileNode>.Deserialize<SoapFormatter>(msg.content);
                            List<String> cacheRequire = node.fileTemplate.ToList();
                            List<Block> segments = node.Partition();
                            //require a template to compare
                            //reqire the rest blocks
                            List<Block> cacheNeed = segments.Where(x => cacheRequire.Exists(y => y == x.name)).ToList();
                            //if (node.cachedPercentage < 0) { node.cachedPercentage = 1 - cacheNeed.Sum(x => x.percentage); }
                            foreach (Block b in cacheNeed)
                            {
                                CDNMessage additionMsg = msg.Clone() as CDNMessage;
                                additionMsg.from = CNDTYPE.CACHE;
                                additionMsg.Fill(CDNMessage.MSGID.DOWNLOAD, Serializer<Block>.Serialize<SoapFormatter>(b));
                                Send(new IPEndPoint(IPAddress.Parse(additionMsg.From().address), additionMsg.From().port), additionMsg);
                            }
                            if(node.cachedPercentage == 1)
                            {
                                node.cachedPercentage = 1 - cacheNeed.Sum(x => x.percentage);
                            }
                            msg.id = CDNMessage.MSGID.PREPARE;
                            content = Serializer<FileNode>.Serialize<SoapFormatter>(node);
                        }
                        break;

                    case CDNMessage.MSGID.PREPARE:
                        {
                            FileNode node = Serializer<FileNode>.Deserialize<SoapFormatter>(msg.content);
                            node.Partition();
                            content = Serializer<FileNode>.Serialize<SoapFormatter>(node);
                        }
                        break;
                    default:
                        break;
                }
                CDNMessage newMsg = msg.Clone() as CDNMessage;
                newMsg.Fill(msg.id, content);
                Send(new IPEndPoint(IPAddress.Parse(msg.From().address), msg.From().port), newMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
