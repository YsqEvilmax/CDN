using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDN
{
    //[Serializable]
    //public class FileSystemDepository : ISerializable
    //{
    //    public FileSystemDepository()
    //    {
    //    }

    //    public FileSystemDepository(String path)
    //        : this()
    //    {
    //        root = new DirectoryNode(path);
    //        root.Scan();
    //    }

    //    protected FileSystemDepository(SerializationInfo info, StreamingContext context)
    //    {
    //        if (info == null) { throw new System.ArgumentNullException("info"); }
    //        this.root = info.GetValue("root", typeof(DirectoryNode)) as DirectoryNode;
    //    }

    //    public override string ToString()
    //    {
    //        return root.ToString();
    //    }

    //    public void GetObjectData(SerializationInfo info, StreamingContext context)
    //    {
    //        if (info == null) { throw new System.ArgumentNullException("info"); }
    //        info.AddValue("root", this.root);
    //    }

    //    public DirectoryNode root { get; protected set; }
    //}

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

        protected FileSystemInfo info;
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
    }

    [Serializable]
    public class FileNode : CommonNode
    {
        public FileNode()
            : base()
        {
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
        }

        protected override void Serialize(SerializationInfo si, StreamingContext context)
        {
            base.Serialize(si, context);
            si.AddValue("FileInfo", this.info);
        }
    }
}
