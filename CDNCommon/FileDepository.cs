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
    public class FileSystemDepository : ISerializable
    {
        public FileSystemDepository()
        {
        }

        public FileSystemDepository(String path)
            : this()
        {
            root = new DirectoryNode(path);
            root.Scan();
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            info.AddValue("from", this.root);
        }

        public DirectoryNode root { get; protected set; }
    }

    [Serializable]
    public abstract class CommonNode : TreeNode
    {       
        public override string ToString()
        {
            String prefix = new String(' ', Level);
            String result = prefix + info.Name + "\r\n";           
            foreach(TreeNode tn in Nodes)
            {
                result += (tn as CommonNode).ToString();
            }
            return result;
        }

        protected FileSystemInfo info;
    }

    public class DirectoryNode : CommonNode
    {
        public DirectoryNode(String path)
        {
            info = new DirectoryInfo(path);
        }

        public void Scan()
        {
            //Get all the files in this directory, not these directories
            FileInfo[] fileList = (info as DirectoryInfo).GetFiles();
            foreach(FileInfo fi in fileList)
            {
                FileNode subNode = new FileNode(fi.FullName);
                this.Nodes.Add(subNode);
            }
            //Get all the directories in this directory, not these files
            DirectoryInfo[] dirList = (info as DirectoryInfo).GetDirectories();
            foreach(DirectoryInfo di in dirList)
            {
                DirectoryNode subNode = new DirectoryNode(di.FullName);
                this.Nodes.Add(subNode);
                subNode.Scan();
            }
        }
    }

    public class FileNode : CommonNode
    {
        public FileNode(String path)
        {
            info = new FileInfo(path);
        }
    }
}
