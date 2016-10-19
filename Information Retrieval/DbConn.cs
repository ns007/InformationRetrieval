using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Information_Retrieval
{
    class DbConn
    {
        public static MySqlConnection conn;
        public static string connectionString;
        public static string username = "ysapir";
        public static string password = "password"; 
        public static string ipAddress = "localhost";


        public static MySqlConnection connect_to_MySQL()
        {
            try
            {
                //string connectionString = "Server=192.168.10.53;Database=gegeit;Uid=nshilo;Pwd=nis135790;";
                //ez_CONN = @"Server = 159.122.141.74:3306; Database = lidim_db; Uid = nshilo; Pwd = nis135790;";
                connectionString = "Server=" + ipAddress + "; Database=info_retrieval_db; Uid=" + username + "; Pwd=" + password + ";";
                DbConn.conn = new MySqlConnection(connectionString);
                DbConn.conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                return conn;
            }
        }
    }
}
