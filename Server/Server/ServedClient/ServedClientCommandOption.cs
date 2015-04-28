using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NDatabase;
using Entity.Util;

namespace ServerEnd
{
    public partial class ServedClient
    {
        public void Register(string registerJson)
        {
            RegisterInfo registerInfo = JsonConvert.DeserializeObject<RegisterInfo>(registerJson);
            using (var odb = OdbFactory.Open(UserInfoDB))
            {
                var users = from user in odb.AsQueryable<UserInfo>()
                            where
                                user.Username.Equals(registerInfo.Username)
                            select user;

                RegisterResult registerResult = new RegisterResult();
                if (users.Count() > 0)
                {
                    Console.WriteLine("用户已经{0}注册...", registerInfo.Username);
                    registerResult.Result = "Fail";
                }
                else
                {
                    //odb.Store<UserInfo>(registerInfo);
                    odb.Store(registerInfo);
                    Console.WriteLine("用户{0}成功注册...", registerInfo.Username);
                    registerResult.Result = "Success";
                }
                string registerResultJson = JsonConvert.SerializeObject(registerResult);
                SendCommand("RegisterResult", registerResultJson);
            }
        }

        public void Login(string loginJson)
        {
            LoginInfo loginInfo = JsonConvert.DeserializeObject<LoginInfo>(loginJson);
            using (var odb = OdbFactory.Open(UserInfoDB))
            {
                var users = from user in odb.AsQueryable<UserInfo>()
                            where
                                user.Username.Equals(loginInfo.Username) &&
                                user.Password.Equals(loginInfo.Password)
                            select user;

                LoginResult loginResult = new LoginResult();
                if (users.Count() > 0)
                {
                    Console.WriteLine("用户{0}登录成功...", loginInfo.Username);
                    loginResult.Result = "Success";
                }
                else
                {
                    Console.WriteLine("用户{0}登录失败...", loginInfo.Username);
                    loginResult.Result = "Fail";
                }
                string loginResultJson = JsonConvert.SerializeObject(loginResult);
                SendCommand("LoginResult", loginResultJson);
            }
        }

        public void Publish(string publishJson)
        {
            PublishInfo publishInfo = JsonConvert.DeserializeObject<PublishInfo>(publishJson, new ImageConverter());
            //string filename = string.Format("E:\\图包\\temp\\{0}-{1}.jpeg", publishInfo.Publisher, publishInfo.PublishTime);
            PublishStore(publishInfo);

            using (var odb = OdbFactory.Open(UserInfoDB))
            {
                odb.Store<PublishInfo>(publishInfo);

                //Console.WriteLine(publishInfo);
                PublishResult publishResult = new PublishResult("Success");
                string publishResultJson = JsonConvert.SerializeObject(publishResult);
                SendCommand("PublishResult", publishResultJson);
            }

            //Console.WriteLine("所有的发布信息...");
            //using (var odb = OdbFactory.Open(UserInfoDB))
            //{
            //    var publishes = from publish in odb.AsQueryable<PublishInfo>()
            //                    where
            //                        true
            //                    select publish;

            //    Console.WriteLine("图片数量: {0}", publishes.Count());
            //    ImageHelper ih2 = new ImageHelper ();
            //    foreach (var publish in publishes)
            //    {
            //        publish.helpInfo.Photo = ih2.GetImage(publish.Publisher, publish.PublishTime);
            //        publish.helpInfo.Photo.Save(@"E:\图包\temp\img.jpeg");
            //    }
            //}
        }

        #region 缓存恢复PublishInfo.helpInfo.Photo的辅助方法
        public void PublishStore(PublishInfo publishInfo)
        {
            ImageHelper imageHelper = new ImageHelper();
            imageHelper.StoreImage(publishInfo.helpInfo.Photo, publishInfo.Publisher, publishInfo.PublishTime);
        }
        public void PublishRecover(List<PublishInfo> publishInfo)
        {
            ImageHelper imageHelper = new ImageHelper();
            foreach (var publish in publishInfo)
            {
                publish.helpInfo.Photo = imageHelper.GetImage(publish.Publisher, publish.PublishTime);
            }
        }
        #endregion

        public void GetPublish(string optionJson)
        {
            GetPublishOption option = JsonConvert.DeserializeObject<GetPublishOption>(optionJson);

            List<PublishInfo> publishList = new List<PublishInfo>();
            
            using (var odb = OdbFactory.Open(UserInfoDB))
            {
                var publishes = from publish in odb.AsQueryable<PublishInfo>()
                                where
                                    (option.Publisher == null || option.Publisher.Length == 0 || publish.Publisher.Equals(option.Publisher)) &&
                                    (option.Title == null || option.Title.Length == 0 || publish.Publisher.Equals(option.Title))
                                select publish;

                foreach (var publish in publishes)
                {
                    publishList.Add(publish);
                }
            }

            // 移除所有除了最后option.Count个
            if (option.Count != 0)
            {
                int remove = Math.Max(0, publishList.Count - option.Count);
                publishList.RemoveRange(0, remove);
            }

            Console.WriteLine("PublishList Length : {0}", publishList.Count);

            PublishRecover(publishList);

            string getPublishResultJson = JsonConvert.SerializeObject(publishList, new ImageConverter());
            SendCommand("GetPublishResult", getPublishResultJson);
        }
    }
}
