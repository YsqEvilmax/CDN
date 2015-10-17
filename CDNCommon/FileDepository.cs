using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class MyExtensions
{
    public static String Serialize(this List<String> list)
    {
        String result = "";
        foreach(String s in list)
        {
            result += ";" + s;
        }
        return result.Substring(result.Length > 0 ? 1 : 0);
    }

    public static List<String> Deserialize(this List<String> list, String s)
    {
        String[] result = s.Split(';');
        return result.ToList().Where(x => x != "").ToList();
    }
}


namespace CDN
{
    [Serializable]
    public abstract class CommonNode : TreeNode
    {
        public CommonNode()
            : base()
        { }

        protected CommonNode(SerializationInfo si, StreamingContext context)
            : base(si, context)
        {
        }

        protected override void Serialize(SerializationInfo si, StreamingContext context)
        {
            base.Serialize(si, context);
        }

        public CommonNode Find(String text)
        {
            CommonNode node = null;
            if(this.Text == text) { node = this; }
            else
            {
                foreach (CommonNode n in Nodes)
                {
                    node = n.Find(text);
                    if (node != null) break;
                }
            }
            return node;
        }

        public override string ToString()
        {
            String prefix = new String(' ', Level);
            String result = prefix + info.Name + "\r\n";
            foreach (TreeNode tn in Nodes)
            {
                result += (tn as CommonNode).ToString();
            }
            return result;
        }

        public FileSystemInfo info { get; protected set; }
    }

    [Serializable]
    public class DirectoryNode : CommonNode
    {
        public DirectoryNode()
            : base()
        { }

        public DirectoryNode(String path)
            : this()
        {
            info = new DirectoryInfo(path);
            Text = info.Name;
        }

        protected DirectoryNode(SerializationInfo si, StreamingContext context)
            : base(si, context)
        {
            this.info = si.GetValue("DirectoryInfo", typeof(DirectoryInfo)) as DirectoryInfo;
        }

        protected override void Serialize(SerializationInfo si, StreamingContext context)
        {
            base.Serialize(si, context);
            si.AddValue("DirectoryInfo", this.info);
        }

        public void Scan()
        {
            this.Nodes.Clear();
            //Get all the files in this directory, not these directories
            FileInfo[] fileList = (info as DirectoryInfo).GetFiles();
            foreach (FileInfo fi in fileList)
            {
                FileNode subNode = new FileNode(fi.FullName);
                this.Nodes.Add(subNode);
            }
            //Get all the directories in this directory, not these files
            DirectoryInfo[] dirList = (info as DirectoryInfo).GetDirectories();
            foreach (DirectoryInfo di in dirList)
            {
                DirectoryNode subNode = new DirectoryNode(di.FullName);
                this.Nodes.Add(subNode);
                subNode.Scan();
            }
        }

        public void Clear()
        {
            try
            {
                FileInfo[] files = (info as DirectoryInfo).GetFiles();
                foreach (FileInfo file in files)
                {
                    File.Delete(file.FullName);
                }
                Scan();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }
    }

    [Serializable]
    public class FileNode : CommonNode
    {
        public FileNode()
            : base()
        {
            cachedPercentage = -1;
            fileTemplate = new List<string>();
        }

        public FileNode(String path)
            : this()
        {
            info = new FileInfo(path);
            Text = info.Name;
        }

        protected FileNode(SerializationInfo si, StreamingContext context)
            : base(si, context)
        {
            this.info = si.GetValue("FileInfo", typeof(FileInfo)) as FileInfo;
            this.fileTemplate = new List<string>();
            this.fileTemplate = this.fileTemplate.Deserialize(si.GetString("fileTemplate"));
            this.cachedPercentage = si.GetDouble("cachedPercentage");
        }

        protected override void Serialize(SerializationInfo si, StreamingContext context)
        {
            base.Serialize(si, context);
            si.AddValue("FileInfo", this.info);
            si.AddValue("fileTemplate", this.fileTemplate.Serialize());
            si.AddValue("cachedPercentage", this.cachedPercentage);
        }

        private readonly static int windowSize = 3;
        public List<Block> Partition()
        {
            List<Block> result = new List<Block>();
            using (FileStream fs = File.OpenRead(info.FullName))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    String content = sr.ReadToEnd();
                    RabinPrintfinger rabin = new RabinPrintfinger();
                    int j = 0;
                    for (int i = 0; i < content.Length - windowSize; i++)
                    {
                        String window = content.Substring(i, windowSize);
                        if (rabin.PrintFinger(window) == 0)
                        {
                            Block b = new Block(this, content.Substring(j, i + windowSize - j));
                            b.percentage = b.content.Length / content.Length;
                            j = i;
                            result.Add(b);
                            fileTemplate.Add(b.ToString());
                        }
                    }
                    Block lastb = new Block(this, content.Substring(j, content.Length - j));
                    lastb.percentage = lastb.content.Length / content.Length;
                    result.Add(lastb);
                    fileTemplate.Add(lastb.name);
                }
            }

            return result;
        }

        //public String fileTemplate { get; private set; }
        public List<String> fileTemplate { get; set; }
        public double cachedPercentage { get; set; }

    }

    [Serializable]
    public class Block : ISerializable
    {
        public Block()
        { }

        public Block(FileNode parent, String content)
            : this()
        {
            this.parent = parent;
            this.content = content;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Unicode.GetBytes(content));
            name = "Segment"+System.Text.Encoding.Unicode.GetString(result)+".part";
        }
        #region Serialization Control
        //This function is necessary for soap Deserialization
        protected Block(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            this.name = info.GetString("name");
            this.percentage = info.GetDouble("percentage");
            this.content = info.GetString("content");
            this.parent = info.GetValue("parent", typeof(FileNode)) as FileNode;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            info.AddValue("name", this.name);
            info.AddValue("percentage", this.percentage);
            info.AddValue("content", this.content);
            info.AddValue("parent", this.parent);
        }
        #endregion

        public string name{ get; private set; }

        public double percentage { get; set; }

        public String content { get; private set; }

        public FileNode parent { get; private set; }


        public override string ToString()
        {
            return name + "|||" + percentage.ToString() + "%%%"  + content;
        }
    }
}
