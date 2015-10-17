using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Soap;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDN
{
    class CDNCache : CDNNetWork
    {
        public CDNCache(IPEndPoint point)
            : base(point, "./cache")
        {
            type = CNDTYPE.CACHE;
            localRoot.Scan();
        }

        public CDNCache(IPEndPoint point, Form ui)
            : this(point)
        {
            this.ui = ui;
        }
        protected override async Task Handle(CDNMessage msg)
        {
            try
            {
                //This message comes from the Server
                if(msg.from == CNDTYPE.SERVER)
                {
                    HandleServer(msg);
                }
                //This message comes from the Client
                else if(msg.from == CNDTYPE.CLIENT)
                {
                    HandleClient(msg);
                }
                else
                {
                    throw new Exception("Wrong msg route!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private  void HandleClient(CDNMessage msg)
        {
            msg.to = CNDTYPE.SERVER;
            switch (msg.id)
            {
                case CDNMessage.MSGID.TEST:
                    {
                        Send(new IPEndPoint(IPAddress.Parse(msg.From().address), msg.From().port), msg);
                    }
                    break;
                case CDNMessage.MSGID.SHOW:
                    {
                        Send((ui as CDNCacheForm).remoteIpAddressControl.Value, msg);
                    }
                    break;
                case CDNMessage.MSGID.DOWNLOAD:
                    {
                        //FileNode node = Serializer<FileNode>.Deserialize<SoapFormatter>(msg.content);
                        //(ui as CDNCacheForm).logListBox.Items.Add("User request: file " + node.info.Name + " at " + DateTime.Now);
                        //FileNode localNode = localRoot.Find(node.Text) as FileNode;
                        //if (localNode != null)
                        //{
                        //    using (FileStream fs = File.OpenRead(localNode.info.FullName))
                        //    {
                        //        using (StreamReader sr = new StreamReader(fs))
                        //        {
                        //            msg.content = localNode.info.Name + "|||" + sr.ReadToEnd();

                        //            Send(new IPEndPoint(IPAddress.Parse(msg.From().address), msg.From().port), msg);
                        //        }
                        //    }
                        //    (ui as CDNCacheForm).logListBox.Items.Add("Response: cached file " + localNode.info.Name);
                        //}
                        //else
                        //{
                        //    Send((ui as CDNCacheForm).remoteIpAddressControl.Value, msg);
                        //    (ui as CDNCacheForm).logListBox.Items.Add("Response: file " + node.info.Name + "downloaded from the server");
                        //}   
                        msg.id = CDNMessage.MSGID.PREPARE;
                        Send((ui as CDNCacheForm).remoteIpAddressControl.Value, msg);
                    }
                    break;

                default:
                    break;
            }
        }

        private void HandleServer(CDNMessage msg)
        {
            msg.to = CNDTYPE.CLIENT;
            switch (msg.id)
            {
                case CDNMessage.MSGID.TEST:
                    {
                        MessageBox.Show("Successfully connected!");
                    }
                    break;
                case CDNMessage.MSGID.SHOW:
                    {
                        Send(new IPEndPoint(IPAddress.Parse(msg.To().address), msg.To().port), msg);
                    }
                    break;
                case CDNMessage.MSGID.DOWNLOAD:
                    {
                        //download the fragments
                        Block newBlock = Serializer<Block>.Deserialize<SoapFormatter>(msg.content);
                        using (FileStream fs = new FileStream(localRoot.FullPath + "\\" + newBlock.name, FileMode.Create, FileAccess.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                sw.Write(newBlock.content);
                            }
                        }
                    }
                    break;
                case CDNMessage.MSGID.PREPARE:
                    {
                        FileNode node = Serializer<FileNode>.Deserialize<SoapFormatter>(msg.content);
                        msg.id = CDNMessage.MSGID.DOWNLOAD;
                        List<String> cacheNeed = Ready(node.fileTemplate);
                        if (cacheNeed.Count == 0)
                        {
                            msg.content = node.Name;
                            foreach(String s in node.fileTemplate)
                            {
                                using (FileStream fs = File.OpenRead(s))
                                {
                                    using (StreamReader sr = new StreamReader(fs))
                                    {
                                        msg.content += sr.ReadToEnd();
                                    }
                                }
                            }
                            Send(new IPEndPoint(IPAddress.Parse(msg.To().address), msg.To().port), msg);
                            (ui as CDNCacheForm).logListBox.Items.Add("User request: file " + node.info.Name + " at " + DateTime.Now);
                            (ui as CDNCacheForm).logListBox.Items.Add("Response:" + node.cachedPercentage.ToString("P") 
                                + " of file " + node.info.Name + " was constructed with cached data");
                        }
                        else
                        {
                            node.fileTemplate = cacheNeed;                    
                            msg.content = Serializer<FileNode>.Serialize<SoapFormatter>(node);
                            Send((ui as CDNCacheForm).remoteIpAddressControl.Value, msg);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private List<String> Ready(List<String> template)
        {
            localRoot.Scan();
            List<String> fegments = new List<string>();
            foreach (TreeNode t in localRoot.Nodes)
            {
                fegments.Add(t.Text);
            }
            List<String> cacheNeed = template.Except(fegments).ToList();
            return cacheNeed;
        }
        private Form ui;
    }
}
