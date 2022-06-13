using maxcare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCommon
{
    public class Connector
    {
        private static Connector instance;

        public static Connector Instance
        {
            get { if (instance == null) instance = new Connector(); return Connector.instance; }
            private set { Connector.instance = value; }
        }
        private string connectionSTR = "Data Source="+Base.pathDataBase+"\\database\\db_maxcare.sqlite;Version=3;";

        private Connector() { }

        SQLiteConnection connection = null;
        void CheckConnectServer()
        {
            try
            {
                if (connection == null)
                    connection = new SQLiteConnection(connectionSTR);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(ex, "CheckConnectServer");
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();
            try
            {
                CheckConnectServer();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                adapter.Fill(data);
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "ExecuteQuery");
            }

            return data;
        }
        public DataTable ExecuteQuery(List<string> lstQuery)
        {
            DataTable data = new DataTable();
            try
            {
                CheckConnectServer();
                for (int i = 0; i < lstQuery.Count; i++)
                {
                    string query = lstQuery[i];
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "ExecuteQuery");
            }

            return data;
        }
        public int ExecuteNonQuery(List<string> lstQuery)
        {
            int data = 0;
            try
            {
                CheckConnectServer();
                for (int i = 0; i < lstQuery.Count; i++)
                {
                    string query = lstQuery[i];
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    data = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "ExecuteNonQuery");
            }

            return data;
        }

        
        public int ExecuteNonQuery(string query)
        {
            int data = 0;
            try
            {
                CheckConnectServer();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                data = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "ExecuteNonQuery: " + query);
            }
            return data;
        }

        public int ExecuteScalar(string query)
        {
            int data = 0;
            try
            {
                CheckConnectServer();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                Int64 LastRowID64 = (Int64)command.ExecuteScalar();
                data = (int)LastRowID64;
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "ExecuteScalar: " + query);
            }
            return data;
        }
    }
}
