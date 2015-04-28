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
        public void Register(RegisterInfo registerInfo)
        {
            string registerJson = JsonConvert.SerializeObject(registerInfo);
            SendCommand("Register", registerJson);
            DealResponse();
        }
        public void Login(LoginInfo loginInfo)
        {
            string loginJson = JsonConvert.SerializeObject(loginInfo);
            SendCommand("Login", loginJson);
            DealResponse();
        }

        public void Publish(PublishInfo publishInfo)
        {
            string publishJson = JsonConvert.SerializeObject(publishInfo, new ImageConverter());
            SendCommand("Publish", publishJson);
            DealResponse();
        }

        public void GetPublish(GetPublishOption option)
        {
            string optionJson = JsonConvert.SerializeObject(option);
            SendCommand("GetPublish", optionJson);
            DealResponse();
        }
    }
}
