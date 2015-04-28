using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ClientEnd
{
    public partial class Client
    {
        // --- 私有变量 ---
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        // --- 共有方法 ---
        // 构造函数
        public Client()
        {
            tcpClient = new TcpClient();

            Console.WriteLine("创建客户.");
        }

        // 连接远程服务器
        public void Connect(IPAddress ipAddress, int port)
        {
            // 开始连接
            tcpClient.Connect(ipAddress, port);
            // 设置网络流
            networkStream = tcpClient.GetStream();

            Console.WriteLine("成功连接.");
        }

        // 发送信息
        public void SendMessageSize(int length)
        {
            // 将信息大小记录成字节
            byte[] messageSize = BitConverter.GetBytes(length);
            // 发送大小 
            Console.WriteLine("messageSize.Length = " + messageSize.Length);
            networkStream.Write(messageSize, 0, messageSize.Length);
        }
        public void SendMessageContent(byte[] sendBuffer)
        {
            // 通过网络流发送信息
            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
        }
        public void SendMessage(string message)
        {

            Console.WriteLine();
            // 将 message字符串 转化为 网络字节流
            byte[] sendBuffer = System.Text.Encoding.Default.GetBytes(message);
            // 发送信息大小
            SendMessageSize(sendBuffer.Length);
            // 发送信息内容
            SendMessageContent(sendBuffer);

            //Console.WriteLine("成功发送信息: {0}", message);
            Console.WriteLine("Content Length: {0}", sendBuffer.Length);
        }

        // 接受信息
        public int ReceiveBufferSize()
        {
            // 创建需要接收的信息的大小的缓冲区
            byte[] receiveBuffer = new byte[4];
            // 接收信息的大小
            networkStream.Read(receiveBuffer, 0, 4);
            // 将接收的字节流转换成整型
            int receiveBufferSize = BitConverter.ToInt32(receiveBuffer, 0);
            // 返回需要接收的信息的代大小
            return receiveBufferSize;
        }

        public string ReceiveMessageContent(int receiveBufferSize)
        {
            // 创建接收缓冲区
            byte[] receiveBuffer = new byte[receiveBufferSize];
            // 利用网络流接受信息
            int length = 0;
            int temp = 0;
            while (receiveBufferSize != 0)
            {
                // temp表示该次Read操作，实际获得了多少字节，receiveBufferSize表示想要获取多少字节,length表示已获取多少字节，也表示从receiveBuffer的length位置开始存储字节
                temp = networkStream.Read(receiveBuffer, length, receiveBufferSize);
                receiveBufferSize -= temp;
                length += temp;
            }
            // 将 字节流 转化为 字符串
            string receiveMessage = System.Text.Encoding.Default.GetString(receiveBuffer);
            // 返回信息
            return receiveMessage;
        }
        public string ReceiveMessage()
        {
            // 获取需要接收的信息的大小
            int receiveBufferSize = ReceiveBufferSize();
            // 接收信息
            string receiveMessage = ReceiveMessageContent(receiveBufferSize);
            // 返回信息

            Console.WriteLine("成功接收信息.");
            return receiveMessage;
        }

        // 根据标签发送特定的命令
        public void SendCommand(string tag, string content)
        {
            string commandString = string.Format("<{0}>{1}</{0}>", tag, content);

            SendMessage(commandString);
        }

        // 关闭客户
        public void Close()
        {
            networkStream.Close();
            tcpClient.Close();

            Console.WriteLine("关闭客户.");
        }

    }

}
