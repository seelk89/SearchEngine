using System;

namespace Indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            string line;

            string[] fileEntries = System.IO.Directory.GetFiles("C:\\Users\\Jesper\\Desktop\\VisualStudioProjects\\SearchEngine\\Indexer\\Texts");
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\Jesper\\Desktop\\VisualStudioProjects\\SearchEngine\\Indexer\\Texts\\haiku1.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("display"))
                {
                    Console.WriteLine(counter.ToString() + ": " + line);
                }

                counter++;
            }

            file.Close();
        }

        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }
    }
}
