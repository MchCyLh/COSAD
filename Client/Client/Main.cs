using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;
using Entity.Util;

namespace ClientEnd
{
    public class Tester
    {
        public static void Main(string[] args)
        {
            Client client = new Client();
            string remoteHost = "127.0.0.1";
            int port = 8888;
            IPAddress remoteIPAddress = IPAddress.Parse(remoteHost);
            client.Connect(remoteIPAddress, port);

            string username = "mchcylh";
            string password = "mchcylh";
            RegisterInfo registerInfo = new RegisterInfo(username, password);
            client.Register(registerInfo);
            LoginInfo loginInfo = new LoginInfo(username, password);
            client.Login(loginInfo);

            HelpInfo helpInfo = new HelpInfo();
            helpInfo.Title = "title";
            Bitmap bitmap = new Bitmap(@"E:\Memory\picture\野良神\wow.jpeg");
            helpInfo.Photo = bitmap;
            helpInfo.Time = new DateTime(2015, 1, 16);
            helpInfo.Place = "place";
            helpInfo.More = "more!!!";
            helpInfo.ContactWay = "contactway";
            helpInfo.Contacter = "contacter";
            string publisher = "publisher";
            DateTime publishtime = DateTime.Now;
            PublishInfo publishInfo = new PublishInfo(helpInfo, publisher, publishtime);
            client.Publish(publishInfo);
            client.GetPublish(new GetPublishOption());

            Console.ReadLine();
        }

        public static void Main3(string[] args)
        {
            Bitmap bitmap = new Bitmap(@"E:\图包\1_120414093742_1.jpg");
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, bitmap.RawFormat);
            byte[] bt1 = ms.ToArray();

            string json = JsonConvert.SerializeObject(bt1);

            byte[] ans = JsonConvert.DeserializeObject<byte[]>(json);

            Console.WriteLine("bt1.Length : {0} -- ans.Length : {1}", bt1.Length, ans.Length);

            string str = System.Text.Encoding.Default.GetString(bt1);

            byte[] bt2 = System.Text.Encoding.Default.GetBytes(str);
            MemoryStream ms2 = new MemoryStream(ans);
            Bitmap bitmap2 = new Bitmap(ms2);


            Console.WriteLine("LENGTH : bt1 == bt2 ? {0}", bt1.Length == bt2.Length);
            Console.WriteLine("bt.Length = {0} -- bt2.Length = {1}", bt1.Length, bt2.Length);
            bool flag = true;
            for (int i = 0; i < bt1.Length && i < ans.Length; i++)
            {
                if (bt1[i] != ans[i])
                {
                    flag = false;
                    break;
                }
            }
            Console.WriteLine("bt1 == ans ? {0}", flag);


            Console.ReadKey();
        }

        public static void Main999(string[] args)
        {
            string deletePart = string.Format("{0}Client{0}bin{0}Debug", Path.DirectorySeparatorChar);
            string StorePath = Path.Combine(Directory.GetCurrentDirectory().Replace(deletePart, ""), "ImageData");
            Console.ReadKey();
        }
    }
}
