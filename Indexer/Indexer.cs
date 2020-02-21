using System;
using System.Collections.Generic;

namespace DbAccess
{
    class Indexer
    {
        static void Main(string[] args)
        {
            string[] fileEntries = System.IO.Directory.GetFiles("C:\\Users\\Jesper\\Desktop\\VisualStudioProjects\\SearchEngine\\Indexer\\Texts");
            InsertNewDocumentsAndWords(fileEntries);
            InsertWordAndDocumentId(fileEntries);
        }

        private static void InsertNewDocumentsAndWords(string[] fileEntries)
        {
            List<string> newWords = new List<string>();
            for (int i = 0; i < fileEntries.Length; i++)
            {
                string filePath = fileEntries[i];
                string[] subString = filePath.Split("\\");
                if (!DbDocumentsAccess.CheckIfDocumentExistsInDocumentss(subString[subString.Length - 1], filePath))
                {
                    Console.WriteLine(filePath);
                    DbDocumentsAccess.InsertIntoDocuments(subString[subString.Length - 1], filePath);
                }

                foreach (string word in GetNewWordsFromFile(filePath))
                {
                    if (!newWords.Contains(word))
                        newWords.Add(word);
                }
            }

            foreach (string word in newWords)
            {
                DbWordsAccess.InsertIntoWords(word);
            }
        }

        private static List<string> GetNewWordsFromFile(string path)
        {
            List<string> newWords = new List<string>();

            // Read the contents of the file into a stream
            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                var fileContent = file.ReadToEnd();

                // Clean up word from words and check if the word already exists.
                string[] words = fileContent.Split();
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Replace(".", "");
                    words[i] = words[i].Replace(",", "");
                    words[i] = words[i].Replace("?", "");
                    words[i] = words[i].Replace("!", "");

                    if (!DbWordsAccess.CheckIfWordExistsInWords(words[i]) && !newWords.Contains(words[i]))
                    {
                        Console.WriteLine(words[i]);
                        newWords.Add(words[i]);
                    }
                }
            }

            return newWords;
        }

        private static void InsertWordAndDocumentId(string[] fileEntries)
        {
            int counter = 0;
            string line;

            for (int i = 0; i < fileEntries.Length; i++)
            {
                string filePath = fileEntries[i];

                string[] subString = filePath.Split("\\");
                var document = DbDocumentsAccess.GetDocumentInDocumentsBasedOnDocument(subString[subString.Length - 1], filePath);

                // Read the file line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    // Clean up word from words and check if the word already exists.
                    string[] words = line.Split();
                    for (int j = 0; j < words.Length; j++)
                    {
                        words[j] = words[j].Replace(".", "");
                        words[j] = words[j].Replace(",", "");
                        words[j] = words[j].Replace("?", "");
                        words[j] = words[j].Replace("!", "");

                        var word = DbWordsAccess.GetWordInWordsBasedOnWord(words[j]);

                        if (!DbDocumentsWordsAccess.CheckIfWordsDocumentLineExistsInWordsDocuments(word.id, document.id, counter))
                        {
                            DbDocumentsWordsAccess.InsertIntoDocumentsWords(word.id, document.id, counter);
                        }   
                    }

                    counter++;
                }

                file.Close();
            } 
        }
    }
}
