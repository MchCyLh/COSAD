using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Entity.Util;

namespace ClientEnd
{
    public partial class Client
    {
        public void DealResponse()
        {
            string commandString = ReceiveMessage();
            Command command = new Command(commandString);

            if (command.Action == "RegisterResult")
            {
                RegisterResult registerResult = JsonConvert.DeserializeObject<RegisterResult>(command.Content);
                if (registerResult.Result == "Success")
                {
                    Console.WriteLine("注册成功...");
                }
                else
                {
                    Console.WriteLine("注册失败...");
                }
            }
            else if (command.Action == "LoginResult")
            {
                LoginResult loginResult = JsonConvert.DeserializeObject<LoginResult>(command.Content);
                if (loginResult.Result == "Success")
                {
                    Console.WriteLine("登录成功...");
                }
                else
                {
                    Console.WriteLine("登录失败...");
                }
            }
            else if (command.Action == "PublishResult")
            {
                PublishResult publishResult = JsonConvert.DeserializeObject<PublishResult>(command.Content);
                if (publishResult.Result == "Success")
                {
                    Console.WriteLine("发布成功...");
                }
                else
                {
                    Console.WriteLine("发布失败...");
                }
            }
            else if (command.Action == "GetPublishResult")
            {

                List<PublishInfo> publishList = JsonConvert.DeserializeObject< List<PublishInfo> >(command.Content, new ImageConverter());
                ImageHelper imageHelper = new ImageHelper();
                imageHelper.StorePath = @"E:\图包\temp";
                foreach (var publish in publishList)
                {
                    imageHelper.StoreImage(publish.helpInfo.Photo, publish.Publisher, publish.PublishTime);
                }
            }
            else
            {
                Console.WriteLine("Command.Action Matched Fail...");
                Console.WriteLine("Current Command.Action : {0}", command.Action);
            }
        }
    }
}
