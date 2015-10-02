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
    public class CDNMessage : ISerializable, ICloneable
    {
        public enum MSGID : int { TEST, SHOW, DOWNLOAD };

        public CDNMessage()
        {
        }

        public CDNMessage(EndPoint point)
            : this()
        {
            String[] ip = point.ToString().Split(':');
            this.address = ip[0];
            this.port = int.Parse(ip[1]);
        }

        #region Serialization Control
        //This function is necessary for soap Serialization
        protected CDNMessage(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            this.id = (MSGID)info.GetUInt32("id");
            this.content = info.GetString("content");
            this.address = info.GetString("address");
            this.port = info.GetInt32("port");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) { throw new System.ArgumentNullException("info"); }
            info.AddValue("id", this.id);
            info.AddValue("content", this.content);
            info.AddValue("address", this.address);
            info.AddValue("port", this.port);
        }
        #endregion

        public void Fill(MSGID id, String content)
        {
            this.id = id;
            this.content = content;
        }

        public object Clone()
        {
            CDNMessage msg = new CDNMessage();
            msg.id = this.id;
            msg.content = this.content;
            msg.address = this.address;
            msg.port = this.port;
            return msg;
        }

        public MSGID id { get; set; }
        public String content { get; set; }
        public String address { get; set; }
        public int port { get; set; }
    }

    public class Serializer<T>
    where T : class, ISerializable, new()
    {
        static public String Serialize(T t)
        {
            String result = "";
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    IFormatter ser = new SoapFormatter();
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

        static public T Deserialize(String s)
        {
            T t = null;
            try
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
                {
                    IFormatter ser = new SoapFormatter();
                    t = ser.Deserialize(stream) as T;
                }
            }
            catch (Exception e)
            { Console.WriteLine(e); }
            return t;
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
            while (true)
            {
                TcpClient client = await AcceptTcpClientAsync();
                NetworkStream ns = client.GetStream();
                using (StreamReader sr = new StreamReader(ns))
                {
                    String context = await sr.ReadToEndAsync();
                    CDNMessage msg = Serializer<CDNMessage>.Deserialize(context);
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
                + "FileSystemDepository:\r\n" + fsd.ToString() + "\r\n";
        }
        public FileSystemDepository fsd { get; protected set; }
    }
}
