using System;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler
{
    class Program
    {
        private static string[] crawled;
        static void Main(string[] args)
        {
            StartCrawlerAsync();
            Console.ReadLine();
        }

        private static async Task StartCrawlerAsync()
        {
            var url = "https://www.google.com/";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
        }
    }
}
