using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ServerEnd
{
    public interface UserInfo
    {
        string Username { set; get; }
        string Password { set; get; }
    }

    public class LoginInfo : UserInfo
    {
        public LoginInfo() {}
        public LoginInfo(string username, string password)
        {
            Username = username;
            Password = password;
        }
        
        #region Implementation of UserInfo
        
        public string Username { set; get; }
        public string Password { set; get; }

        #endregion

        public override string ToString()
        {
            return string.Format("[LoginInfo] Username:{0}, Password: {1}", Username, Password);
        }
    }

    public class LoginResult
    {
        public LoginResult() { }
        public LoginResult(string result)
        {
            Result = result;
        }
        public string Result { set; get; }

    }

    public class RegisterInfo : UserInfo
    {
        public RegisterInfo() { }
        public RegisterInfo(string username, string password)
        {
            Username = username;
            Password = password;
        }

        #region Implementation of UserInfo

        public string Username { set; get; }
        public string Password { set; get; }
        
        #endregion

        public override string ToString()
        {
            return string.Format("[RegisterInfo] Username:{0}, Password: {1}", Username, Password);
        }
    }

    public class RegisterResult
    {
        public RegisterResult() { }
        public RegisterResult(string result)
        {
            Result = result;
        }
        public string Result { set; get; }

    }


    public class Command
    {
        public Command(string commandString)
        {
            string commandPattern = @"^<(\w+)>(.*)</(\1)>$";

            Match commandMatch = Regex.Match(commandString, commandPattern);
            if (commandMatch.Success)
            {
                Action = commandMatch.Groups[1].Value;
                Content = commandMatch.Groups[2].Value;
                Console.WriteLine("Command: Matched Success...");
            }
            else
            {
                Console.WriteLine("Command : Matched Fail...");
            }

        }
        public string Action { set; get; }
        public string Content { set; get; }
    }


    public class HelpInfo
    {
        public string Title { set; get; }
        public Bitmap Photo { set; get; }
        public DateTime Time { set; get; }
        public string Place { set; get; }
        public string ContactWay { set; get; }
        public string Contacter { set; get; }
        public string More { set; get; }

        public override string ToString()
        {
            return string.Format("[HelpInfo] Title: {0}, Photo: {1}, Time: {2}, Place: {3}, ContactWay: {4}, Contacter: {5}, More: {6}", Title, Photo, Time, Place, ContactWay, Contacter, More);
        }
    }

    public class PublishInfo
    {
        public PublishInfo()
        {
            helpInfo = new HelpInfo();
            PublishTime = DateTime.Now;
        }
        public PublishInfo(HelpInfo hi, string publisher, DateTime publishTime)
        {
            helpInfo = hi;
            Publisher = publisher;
            PublishTime = publishTime;
        }
        public HelpInfo helpInfo { set; get; }
        public string Publisher { set; get; }
        public DateTime PublishTime { set; get; }

        public override string ToString()
        {
            return string.Format("[PublishInfo] Publisher: {0}, PublishTime: {1} -{2}", Publisher, PublishTime, helpInfo);
        }
    }

    public class PublishResult
    {
        public PublishResult() { }
        public PublishResult(string result)
        {
            Result = result;
        }

        public string Result { set; get; }
    }

    public class GetPublishOption
    {
        public GetPublishOption() { }
        public GetPublishOption(string publisher, string title, int count)
        {
            Publisher = publisher;
            Title = title;
            Count = count;
        }

        public string Publisher { set; get; }
        public string Title { set; get; }
        public int Count { set; get; }
    }

}
