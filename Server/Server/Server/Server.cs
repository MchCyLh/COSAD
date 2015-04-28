using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data.SqlClient;

namespace ServerEnd
{
    
    
    public class Server
    {
        // --- 私有成员 ---
        TcpListener tcpListener;
        // 数据库管理员
        Database database;

        public Server(int nativePort)
        {
            tcpListener = new TcpListener(nativePort);
            database = new Database();
            Console.WriteLine("创建服务器.");
        }

        // 构造函数
        public Server(IPAddress nativeIPAddress, int nativePort)
        {
            tcpListener = new TcpListener(nativeIPAddress, nativePort);
            database = new Database();
            Console.WriteLine("创建服务器.");
        }

        // 开始监听
        public void Start()
        {
            tcpListener.Start();

            Console.WriteLine("开启服务器.");
        }

        // 等待客户端的连接请求
        public TcpClient Accept()
        {
            Console.WriteLine("等待客户端连接请求.");
            return tcpListener.AcceptTcpClient();
        }

        // 服务器增加一个用户
        public void AddTcpClient(TcpClient tcpClient)
        {
            ServedClient servedClient = new ServedClient(tcpClient);

            servedClient.Serve();

            Console.WriteLine("服务器添加一个需要提供服务的Tcp客户.");
        }
    }

}
