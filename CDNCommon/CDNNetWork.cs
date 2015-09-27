using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CDN
{
    [Serializable]
    public class CDNMessage : ISerializable
    {
        public enum MSGID : int { SHOW, DOWNLOAD};

        public CDNMessage()
        {
        }

        public CDNMessage(MSGID id, String content)
            : this()
        {
            this.id = id;
            this.content = content;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            info.AddValue("from", this.from);
            info.AddValue("to", this.to);
            info.AddValue("id", this.id);
            info.AddValue("content", this.content);
        }

        public MSGID id { get; set; }
        public String content { get; set; }
        [XmlIgnore]
        public IPEndPoint from { get; set; }
        [XmlIgnore]
        public IPEndPoint to { get; set; }
    }

    public class Serializer<T>
    where T : class, ISerializable, new()
    {
        static public String Serialize(T t)
        {
            Type x = t.GetType();
            XmlSerializer ser = new XmlSerializer(t.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, t);
            return sb.ToString();
        }

        static public T Deserialize(String s)
        {
            T t = new T();       
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(s);
            XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
            XmlSerializer ser = new XmlSerializer(t.GetType());
            object obj = ser.Deserialize(reader);
            return obj as T;
        }
    }

    public class CDNNetWork : TcpListener
    {
        public CDNNetWork(IPEndPoint point)
            : base(point)
        {
            fsd = new FileSystemDepository("./");
        }

        public static IPEndPoint GetLocalIPPoint()
        {
            IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return new IPEndPoint(localAddress, 0);
        }

        public async Task Idle()
        {
            while(true)
            {
                TcpClient client = await AcceptTcpClientAsync();
                NetworkStream ns = client.GetStream();
                using (StreamReader sr = new StreamReader(ns))
                {
                    String context = await sr.ReadToEndAsync();
                    CDNMessage msg = Serializer<CDNMessage>.Deserialize(context);
                    Handle(msg);
                }
                ns.Close();
                client.Close();
            }

        }

        protected virtual async Task Handle(CDNMessage msg)
        {
        }

        public override string ToString()
        {
            return "IP:\r\n" + base.LocalEndpoint.ToString() + "\r\n" 
                + "FileSystemDepository:\r\n"+ fsd.ToString() + "\r\n";
        }
        public FileSystemDepository fsd { get; protected set; }
    }
}
