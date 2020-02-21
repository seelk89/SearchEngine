using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Models;

namespace DbAccess
{
    public class DbDocumentsWordsAccess
    {
        // Not in use.
        public static void GetDocumentsWords(SQLiteConnection con)
        {
            SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM DocumentsWords", con);

            try
            {
                var sqlite_datareader = select_statement.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    object idReader = sqlite_datareader.GetValue(0);
                    object wordsId = sqlite_datareader.GetValue(1);
                    object documentsId = sqlite_datareader.GetValue(2);
                    object lineNumber = sqlite_datareader.GetValue(3);

                    Console.WriteLine($"Id: {idReader} wordsId: {wordsId} DocumentsId: {documentsId} Line: {lineNumber}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Not in use.
        public static List<DocumentsWords> GetDocumentsWordsBasedOnWordsIdAndDocumentsId(SQLiteConnection con, int wordsId, int documentsId)
        {
            SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM DocumentsWords WHERE wordsId = (@wordsId) AND documentsId = (@documentsId)", con);
            select_statement.Parameters.AddWithValue("@wordsId", wordsId);
            select_statement.Parameters.AddWithValue("@documentsId", documentsId);

            try
            {
                var sqlite_datareader = select_statement.ExecuteReader();

                List<DocumentsWords> returnList = new List<DocumentsWords>();

                while (sqlite_datareader.Read())
                {
                    returnList.Add(new DocumentsWords
                    {
                        id = sqlite_datareader.GetInt32(0),
                        wordsId = sqlite_datareader.GetInt32(1),
                        documentsId = sqlite_datareader.GetInt32(2),
                        line = sqlite_datareader.GetInt32(3)
                    });
                }

                return returnList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool CheckIfWordsDocumentLineExistsInWordsDocuments(int wordsId, int documentsId, int line)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=SearchEngineDb.db;"))
            {
                c.Open();

                SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM DocumentsWords WHERE wordsId = (@wordsId) AND documentsId = (@documentsId) AND line = (@line)", c);
                select_statement.Parameters.AddWithValue("@wordsId", wordsId);
                select_statement.Parameters.AddWithValue("@documentsId", documentsId);
                select_statement.Parameters.AddWithValue("@line", line);

                using (select_statement)
                {
                    using (SQLiteDataReader rdr = select_statement.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static void InsertIntoDocumentsWords(int wordsId, int documentsId, int line)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=SearchEngineDb.db;"))
            {
                c.Open();

                SQLiteCommand insert_statement = new SQLiteCommand("INSERT INTO DocumentsWords (wordsId, documentsId, line) VALUES (@wId, @dId, @line)", c);
                insert_statement.Parameters.AddWithValue("@wId", wordsId);
                insert_statement.Parameters.AddWithValue("@dId", documentsId);
                insert_statement.Parameters.AddWithValue("@line", line);

                using (insert_statement)
                {
                    insert_statement.ExecuteNonQuery();
                }
            }
        }

        // Not in use.
        public static void UpdateDocumentsWords(SQLiteConnection con)
        {
            Console.WriteLine("Write the id to update.");
            string id = Console.ReadLine();

            Console.WriteLine("Write what the wordsId should be updated to.");
            string wId = Console.ReadLine();

            Console.WriteLine("Write what the documentsId should be updated to.");
            string dId = Console.ReadLine();

            Console.WriteLine("Write what the line number should be updated to.");
            string lineNumber = Console.ReadLine();

            SQLiteCommand update_statement = new SQLiteCommand("UPDATE DocumentsWords SET wordsId = (@wId), documentsId = (@dId), line = (@lineNumber) WHERE id = (@id)", con);
            update_statement.Parameters.AddWithValue("@id", id);
            update_statement.Parameters.AddWithValue("@wId", wId);
            update_statement.Parameters.AddWithValue("@dId", dId);
            update_statement.Parameters.AddWithValue("@lineNumber", lineNumber);

            try
            {
                update_statement.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Not in use.
        public static void DeleteFromDocumentsWords(SQLiteConnection con)
        {
            Console.WriteLine("Write the id to delete.");
            string id = Console.ReadLine();

            SQLiteCommand delete_statement = new SQLiteCommand("DELETE FROM DocumentsWords WHERE id = (@id)", con);
            delete_statement.Parameters.AddWithValue("@id", id);

            try
            {
                delete_statement.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
