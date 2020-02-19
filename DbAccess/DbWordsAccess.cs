using System;
using System.Data.SQLite;

namespace DbAccess
{
    public class DbWordsAccess
    {
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

        public static void InsertIntoWords(SQLiteConnection con)
        {
            Console.WriteLine("Write the word to store.");
            string word = Console.ReadLine();

            SQLiteCommand insert_statement = new SQLiteCommand("INSERT INTO Words (word) VALUES (@word)", con);
            insert_statement.Parameters.AddWithValue("@word", word);

            try
            {
                insert_statement.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
