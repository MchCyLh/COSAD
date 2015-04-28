using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using NDatabase;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;


namespace ServerEnd
{
    public class Tester
    {
        public static void Main(string[] args)
        {
            //string nativeHost = "172.19.50.21";
            int nativePort = 8888;
            //IPAddress nativeIPAddress = IPAddress.Parse(nativeHost);
            //Server server = new Server(nativeIPAddress, nativePort);
            Server server = new Server(nativePort);
            server.Start();
            while (true)
            {
                TcpClient tcpClient = server.Accept();
                server.AddTcpClient(tcpClient);
            }
        }

        public static void Main55(string[] args)
        {
            string filename = "test2.db";
            using (var odb = OdbFactory.Open(filename))
            {
                var alls = from publishInfo in odb.AsQueryable<PublishInfo>()
                           where
                               true
                           select publishInfo;
                foreach (var all in alls)
                {
                    odb.Delete<PublishInfo>(all);
                }
            }

        }

        public static void Main1(string[] args)
        {
            RegisterInfo registerInfo = new RegisterInfo("mchcylh", "mchcylh");
            string filename = "test.db";
            using (var odb = OdbFactory.Open(filename))
            {
                odb.Store(registerInfo);
            }

            using (var odb = OdbFactory.Open(filename))
            {
                var query = odb.Query<UserInfo>();
                query.Descend("Username").Constrain("mchcylh").Equal();
                var users = query.Execute<UserInfo>();

                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
            }
            Console.ReadLine();
        }

        // 测试某个表的所有数据
        public static void MainAllUsers(string[] args)
        {
            string filename = "test.db";
            using (var odb = OdbFactory.Open(filename))
            {
                var users = from user in odb.AsQueryable<UserInfo>()
                            where
                                true
                            select user;
                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
            }
            Console.ReadLine();
        }

        public static void Main10(string[] args)
        {
            IMAGE image = new IMAGE(@"E:\图包\1_120414093742_1.jpg");
            Console.WriteLine("image: {0}", image);
            string json = JsonConvert.SerializeObject(image);
            Console.WriteLine("json: {0}", json);
            IMAGE image2 = JsonConvert.DeserializeObject<IMAGE>(json);
            Console.WriteLine("image2: {0}", image2);
            Console.WriteLine("image == image2 ?  {0}", image == image2);
            Console.ReadLine();
        }

        public static void Main20(string[] args)
        {
            using (FileStream fs = File.Open(@"E:\图包\1_120414093742_1.jpg", FileMode.Open))
            {
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static void Main13(string[] args)
        {

            Image image = Image.FromFile(@"E:\图包\1_120414093742_1.jpg");
            string testFile = @"imagetest.db";
            using (var odb = OdbFactory.Open(testFile))
            {
                odb.Store(image);
                Console.WriteLine(image);
            }

            using (var odb = OdbFactory.Open(testFile))
            {
                var images = from img in odb.AsQueryable<Image>()
                             where
                                true
                             select img;
                foreach (var img in images)
                    Console.WriteLine(img.GetType());
            }

            Console.ReadLine();
        }

        public static void Main14(string[] args)
        {
            STRING str = new STRING(1);
            string json = JsonConvert.SerializeObject(str);
            Console.WriteLine(json);
            STRING STR = JsonConvert.DeserializeObject<STRING>(json);
            Console.WriteLine(STR.nest.Int);
            Console.ReadLine();

        }

    }

    public class NEST
    {
        public NEST(int i)
        {
            Int = i;
        }
        public int Int { set; get; }
    }
    public class STRING
    {
        public STRING(int i)
        {
            nest = new NEST(i);
        }
        public NEST nest { set; get; }
    }

    public class IMAGE
    {
        public IMAGE() { }
        public IMAGE(string path)
        {
            image = Image.FromFile(path);
        }
        public Image image { set; get; }
    }
}
