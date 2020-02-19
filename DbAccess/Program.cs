using System;
using System.Data.SQLite;

namespace DbAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = "Data Source=SearchEngineDb.db;";

            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT SQLITE_VERSION()";
            using var cmd = new SQLiteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");



            bool q = true;
            while (q)
            {
                Console.WriteLine("Write GET for DB contents. INSERT for inserting data. UPDATE for updating data. DELETE for deleting data. Q to quit");
                string user_input = Console.ReadLine();

                switch (user_input)
                {
                    case "GET":
                        //DbWordsAccess.GetWords(con);
                        //DbDocumentsAccess.GetDocuments(con);
                        DbDocumentsWordsAccess.GetDocumentsWords(con);
                        break;

                    case "INSERT":
                        //DbWordsAccess.InsertIntoWords(con);
                        //DbDocumentsAccess.InsertIntoDocuments(con);
                        DbDocumentsWordsAccess.InsertIntoDocumentsWords(con);
                        break;

                    case "UPDATE":
                        //DbWordsAccess.UpdateWords(con);
                        //DbDocumentsAccess.UpdateDocuments(con);
                        DbDocumentsWordsAccess.UpdateDocumentsWords(con);
                        break;

                    case "DELETE":
                        //DbWordsAccess.DeleteFromWords(con);
                        //DbDocumentsAccess.DeleteFromDocuments(con);
                        DbDocumentsWordsAccess.DeleteFromDocumentsWords(con);
                        break;

                    case "Q":
                        q = false;
                        break;
                }
            }  
        }
    }
}
