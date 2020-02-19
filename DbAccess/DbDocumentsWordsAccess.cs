using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Models;

namespace DbAccess
{
    class DbDocumentsWordsAccess
    {
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

        public static List<DocumentsWords> GetDocumentsWordsBasedOnWordsIdAndDocumentsId(SQLiteConnection con, string wordsId, string documentsId)
        {
            SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM DocumentsWords WHERE wordsId = (@wordsId) AND documentsId = (@documentsId)", con);
            select_statement.Parameters.AddWithValue("@wId", wordsId);
            select_statement.Parameters.AddWithValue("@dId", documentsId);

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

        public static void InsertIntoDocumentsWords(SQLiteConnection con)
        {
            Console.WriteLine("Write the words id to store.");
            string wId = Console.ReadLine();

            Console.WriteLine("Write the documents id to store.");
            string dId = Console.ReadLine();

            Console.WriteLine("Write the line number to store.");
            string lineNumber = Console.ReadLine();

            SQLiteCommand insert_statement = new SQLiteCommand("INSERT INTO DocumentsWords (wordsId, documentsId, line) VALUES (@wId, @dId, @lineNumber)", con);
            insert_statement.Parameters.AddWithValue("@wId", wId);
            insert_statement.Parameters.AddWithValue("@dId", dId);
            insert_statement.Parameters.AddWithValue("@lineNumber", lineNumber);

            try
            {
                insert_statement.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
