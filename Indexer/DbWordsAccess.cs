using System;
using System.Data.SQLite;
using Models;

namespace DbAccess
{
    public class DbWordsAccess
    {
        // Not in use.
        public static void GetWords(SQLiteConnection con)
        {
            SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM Words", con);

            try
            {
                var sqlite_datareader = select_statement.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    object idReader = sqlite_datareader.GetValue(0);
                    string wordReader = sqlite_datareader.GetString(1);

                    Console.WriteLine($"Id: {idReader} Word: {wordReader}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Words GetWordInWordsBasedOnWord(string word)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=SearchEngineDb.db;"))
            {
                c.Open();

                SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM Words WHERE word = (@word)", c);
                select_statement.Parameters.AddWithValue("@word", word);

                using (select_statement)
                {
                    using (SQLiteDataReader rdr = select_statement.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            return new Words
                            {
                                id = rdr.GetInt32(0),
                                word = rdr.GetString(1) 
                            };
                        }
                    }
                }
            }

            return new Words{};
        }

        public static bool CheckIfWordExistsInWords(string word)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=SearchEngineDb.db;"))
            {
                c.Open();

                SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM Words WHERE word = (@word)", c);
                select_statement.Parameters.AddWithValue("@word", word);

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

        public static void InsertIntoWords(string word)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=SearchEngineDb.db;"))
            {
                c.Open();

                SQLiteCommand insert_statement = new SQLiteCommand("INSERT INTO Words (word) VALUES (@word)", c);
                insert_statement.Parameters.AddWithValue("@word", word);

                using (insert_statement)
                {
                    insert_statement.ExecuteNonQuery();
                }
            }
        }

        // Not in use.
        public static void UpdateWords(SQLiteConnection con)
        {
            Console.WriteLine("Write the word to update.");
            string eWord = Console.ReadLine();

            Console.WriteLine("Write what it should be updated to.");
            string uWord = Console.ReadLine();

            SQLiteCommand update_statement = new SQLiteCommand("UPDATE Words SET word = (@uWord) WHERE word = (@eWord)", con);
            update_statement.Parameters.AddWithValue("@uWord", uWord);
            update_statement.Parameters.AddWithValue("@eWord", eWord);

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
        public static void DeleteFromWords(SQLiteConnection con)
        {
            Console.WriteLine("Write the word to delete.");
            string dWord = Console.ReadLine();

            SQLiteCommand delete_statement = new SQLiteCommand("DELETE FROM Words WHERE word = (@word)", con);
            delete_statement.Parameters.AddWithValue("@word", dWord);

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
