using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CDN
{
    [Serializable]
    public class AddressAndPort : ISerializable
    {
        public AddressAndPort()
        { }

        public AddressAndPort(String add, int port)
            : this()
        {
            this.address = add;
            this.port = port;
        }

        public AddressAndPort(EndPoint point)
            : this()
        {
            String[] ip = point.ToString().Split(':');
            address = ip[0];
            port = int.Parse(ip[1]);
        }
        #region Serialization Control
        //This function is necessary for soap Deserialization
        protected AddressAndPort(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            this.address = info.GetString("address");
            this.port = info.GetInt32("port");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            info.AddValue("address", this.address);
            info.AddValue("port", this.port);
        }
        #endregion

        public override string ToString()
        {
            return address + ":" + port.ToString();
        }

        public String address { get; set; }
        public int port { get; set; }
    }

    [Serializable]
    public class CDNMessage : ISerializable, ICloneable
    {
        public enum MSGID : int { TEST, SHOW, DOWNLOAD};

        public CDNMessage()
        {
        }

        #region Serialization Control
        //This function is necessary for soap Deserialization
        protected CDNMessage(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            this.id = (MSGID)info.GetUInt32("id");
            this.content = info.GetString("content");
            this.from = (CDNNetWork.CNDTYPE)info.GetUInt32("from");
            this.to = (CDNNetWork.CNDTYPE)info.GetUInt32("to");
            this.client = info.GetValue("client", typeof(AddressAndPort)) as AddressAndPort;
            this.cache = info.GetValue("cache", typeof(AddressAndPort)) as AddressAndPort;
            this.server = info.GetValue("server", typeof(AddressAndPort)) as AddressAndPort;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            info.AddValue("id", this.id);
            info.AddValue("content", this.content);
            info.AddValue("from", this.from);
            info.AddValue("to", this.to);
            info.AddValue("client", this.client);
            info.AddValue("cache", this.cache);
            info.AddValue("server", this.server);
        }
        #endregion

        public void Fill(MSGID id, String content = "")
        {
            this.id = id;
            this.content = content;
        }

        public object Clone()
        {
            CDNMessage msg = new CDNMessage();
            msg.id = this.id;
            msg.content = this.content;
            msg.client = this.client;
            msg.cache = this.cache;
            msg.server = this.server;
            msg.from = this.from;
            msg.to = this.to;
            return msg;
        }

        public void AddressUpdate(CDNNetWork sr)
        {
            switch (sr.type)
            {
                case CDNNetWork.CNDTYPE.CLIENT:
                    client = new AddressAndPort(sr.LocalEndpoint);
                    break;
                case CDNNetWork.CNDTYPE.CACHE:
                    cache = new AddressAndPort(sr.LocalEndpoint);
                    break;
                case CDNNetWork.CNDTYPE.SERVER:
                    server = new AddressAndPort(sr.LocalEndpoint);
                    break;
                default:
                    break;
            }
        }

        public AddressAndPort From()
        {
            switch (from)
            {
                case CDNNetWork.CNDTYPE.CLIENT:
                    return client;
                    break;
                case CDNNetWork.CNDTYPE.CACHE:
                    return cache;
                    break;
                case CDNNetWork.CNDTYPE.SERVER:
                    return server;
                    break;
                default:
                    break;
            }
            return null;
        }

        public AddressAndPort To()
        {
            switch (to)
            {
                case CDNNetWork.CNDTYPE.CLIENT:
                    return client;
                    break;
                case CDNNetWork.CNDTYPE.CACHE:
                    return cache;
                    break;
                case CDNNetWork.CNDTYPE.SERVER:
                    return server;
                    break;
                default:
                    break;
            }
            return null;
        }

        public MSGID id { get; set; }
        public String content { get; set; }
        public CDNNetWork.CNDTYPE from { get; set; }
        public CDNNetWork.CNDTYPE to { get; set; }
        private AddressAndPort client;
        private AddressAndPort cache;
        private AddressAndPort server;
    }

    public class Serializer<T>
    where T : class, ISerializable, new()
    {
        static public String Serialize<F>(T t) where F : IFormatter, new()
        {
            String result = "";
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    IFormatter ser = new F();
                    ser.Serialize(stream, t);
                    result = Encoding.UTF8.GetString(stream.GetBuffer());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        static public T Deserialize<F>(String s) where F : IFormatter, new()
        {
            T t = null;
            try
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
                {
                    IFormatter ser = new F();
                    t = ser.Deserialize(stream) as T;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return t;
        }
    }

    public class CDNNetWork : TcpListener
    {
        public enum CNDTYPE : int { CLIENT, SERVER, CACHE, UNKNOWN }

        public CDNNetWork(IPEndPoint point, String folder = "./")
            : base(point)
        {
            name = folder;
            localRoot = new DirectoryNode(folder);
        }

        public static IPEndPoint GetLocalIPPoint()
        {
            IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return new IPEndPoint(localAddress, 0);
        }

        public void Send(IPEndPoint des, CDNMessage msg)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(des);
                    if (client.Connected)
                    {
                        NetworkStream ns = client.GetStream();
                        using (StreamWriter sw = new StreamWriter(ns))
                        {
                            CDNMessage newMsg = msg.Clone() as CDNMessage;
                            newMsg.from = this.type;
                            newMsg.AddressUpdate(this);
                            sw.Write(Serializer<CDNMessage>.Serialize<SoapFormatter>(newMsg));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Fail to send" + msg.ToString() + exp.Message);
            }
        }

        public async Task Idle()
        {
            while (true)
            {
                TcpClient client = await AcceptTcpClientAsync();
                NetworkStream ns = client.GetStream();
                using (StreamReader sr = new StreamReader(ns))
                {
                    String context = await sr.ReadToEndAsync();
                    CDNMessage msg = Serializer<CDNMessage>.Deserialize<SoapFormatter>(context);
                    msg.to = this.type;
                    msg.AddressUpdate(this);
                    Handle(msg);
                }
            }

        }

        protected virtual async Task Handle(CDNMessage msg)
        {
        }

        public override string ToString()
        {
            return "IP:\r\n" + base.LocalEndpoint.ToString() + "\r\n"
                + "FileSystemDepository:\r\n" + localRoot.ToString() + "\r\n";
        }
        public DirectoryNode localRoot { get; protected set; }
        public String name { get; protected set; }
        public CNDTYPE type { get; protected set; }
    }
}
