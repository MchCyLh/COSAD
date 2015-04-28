using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ServerEnd
{
    // 数据库类
    /// <summary>
    /// 使用方法:创建Database对象,连接数据库,使用语句
    /// </summary>
    public class Database
    {
        // --- 私有变量 ---
        // 带连接的数据库信息
        private string connectionString;
        // 连接到数据库的桥梁
        private SqlConnection sqlConnection;
        // sql命令控制器
        private SqlCommand sqlCommand;
        // sql命令语句
        private string commandString;


        // --- 共有属性 ---
        
        public string CommandString
        {
            get
            {
                return commandString;
            }
            set
            {
                commandString = value;
                // 更行命令字符串
                sqlCommand.CommandText = commandString;
            }
        }
        // --- 共有方法 ---
        // 构造函数
        public Database()
        {
            connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\DataFromGithub\COSAD\Server\Server\DB.mdf;Integrated Security=True;Connect Timeout=30";
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connectionString;
            sqlCommand = new SqlCommand();
            // 绑定连接器和命令器
            sqlCommand.Connection = sqlConnection;
        }

        // 启动数据库连接
        public void Open()
        {
            sqlConnection.Open();

        }
        // 关闭数据库连接
        public void Close()
        {
            sqlConnection.Close();
        }

        // 执行sql语句,对数据库进行操作,共三种方法.
        public void ExecuteNonQuery()
        {
            sqlCommand.ExecuteNonQuery();
        }
        public object ExecuteScalar()
        {
            // 执行查询，并返回查询所返回的结果集中第一行的第一列。 忽略其他列或行。
            return sqlCommand.ExecuteScalar();
        }
        public SqlDataReader ExecuteReader()
        {
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            return sqlDataReader;
        }

        // 设置参数
        public void AddParameter(string parameter, Object value)
        {
            sqlCommand.Parameters.AddWithValue(parameter, value);
        }
    }
}
