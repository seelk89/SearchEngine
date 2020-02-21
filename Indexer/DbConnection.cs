using System;
using System.Data.SQLite;

namespace DbAccess
{
    public class DbConnection
    {
        public static SQLiteConnection GetConnection()
        {
            string cs = "Data Source=SearchEngineDb.db;";

            var con = new SQLiteConnection(cs);
            con.Open();

            var cmd = new SQLiteCommand("SELECT SQLITE_VERSION()", con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");

            return con;
        }
    }
}
