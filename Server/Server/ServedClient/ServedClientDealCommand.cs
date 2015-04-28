using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServerEnd
{
    public partial class ServedClient
    {
        public void DealCommand(string commandString)
        {
            Command command = new Command(commandString);
           
            if (command.Action == "Register")
            {
                Register(command.Content);
            }
            else if (command.Action == "Login")
            {
                Login(command.Content);
            }
            else if (command.Action == "Publish")
            {
                Publish(command.Content);
            }
            else if (command.Action == "GetPublish")
            {
                GetPublish(command.Content);
            }
            else
            {
                Console.WriteLine("Command.Action Matched Fail...");
            }
        }
    }
}
