using System;
using System.Data.SQLite;
using Models;

namespace DbAccess
{
    public class DbDocumentsAccess
    {
        // Not in use.
        public static void GetDocuments(SQLiteConnection con)
        {
            SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM Documents", con);

            try
            {
                var sqlite_datareader = select_statement.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    object idReader = sqlite_datareader.GetValue(0);
                    string documentName = sqlite_datareader.GetString(1);
                    string documentPath = sqlite_datareader.GetString(2);

                    Console.WriteLine($"Id: {idReader} Document name: {documentName} Document path: {documentPath}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Documents GetDocumentInDocumentsBasedOnDocument(string documentName, string documentPath)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=SearchEngineDb.db;"))
            {
                c.Open();

                SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM Documents WHERE documentName = (@documentName) AND documentPath = (@documentPath)", c);
                select_statement.Parameters.AddWithValue("@documentName", documentName);
                select_statement.Parameters.AddWithValue("@documentPath", documentPath);

                using (select_statement)
                {
                    using (SQLiteDataReader rdr = select_statement.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            return new Documents
                            {
                                id = rdr.GetInt32(0),
                                documentName = rdr.GetString(1),
                                documentPath = rdr.GetString(2)
                            };
                        }
                    }
                }
            }

            return new Documents { };
        }

        public static bool CheckIfDocumentExistsInDocumentss(string documentName, string documentPath)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=SearchEngineDb.db;"))
            {
                c.Open();

                SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM Documents WHERE documentName = (@documentName) AND documentPath = (@documentPath)", c);
                select_statement.Parameters.AddWithValue("@documentName", documentName);
                select_statement.Parameters.AddWithValue("@documentPath", documentPath);

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

        public static void InsertIntoDocuments(string documentName, string documentPath)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=SearchEngineDb.db;"))
            {
                c.Open();

                SQLiteCommand insert_statement = new SQLiteCommand("INSERT INTO Documents (documentName, documentPath) VALUES (@documentName, @documentPath)", c);
                insert_statement.Parameters.AddWithValue("@documentName", documentName);
                insert_statement.Parameters.AddWithValue("@documentPath", documentPath);

                using (insert_statement)
                {
                    insert_statement.ExecuteNonQuery();
                }
            }
        }

        // Not in use.
        public static void UpdateDocuments(SQLiteConnection con)
        {
            Console.WriteLine("Write the document name to update.");
            string eDocument = Console.ReadLine();

            Console.WriteLine("Write what the name should be updated to.");
            string uDocument = Console.ReadLine();

            Console.WriteLine("Write what the path should be updated to.");
            string pDocument = Console.ReadLine();

            SQLiteCommand update_statement = new SQLiteCommand("UPDATE Documents SET documentName = (@uDocument), documentPath = (@pDocument) WHERE documentName = (@eDocument)", con);
            update_statement.Parameters.AddWithValue("@uDocument", uDocument);
            update_statement.Parameters.AddWithValue("@pDocument", pDocument);
            update_statement.Parameters.AddWithValue("@eDocument", eDocument);

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
        public static void DeleteFromDocuments(SQLiteConnection con)
        {
            Console.WriteLine("Write the document to delete.");
            string dDocument = Console.ReadLine();

            SQLiteCommand delete_statement = new SQLiteCommand("DELETE FROM Documents WHERE documentName = (@document)", con);
            delete_statement.Parameters.AddWithValue("@document", dDocument);

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
