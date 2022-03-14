using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ClassLibrary.Models;

namespace TestRss
{
    class Program
    {
        static int timeout = 4000;
        static void Main(string[] args)
        {
            while (true)
            {
                ReadRss("https://habr.com/ru/rss/all/all/", "Хабрахабр");
                ReadRss("http://www.ixbt.com/export/news.rss", "Интерфакс");
                Console.WriteLine($"PAUSE ({timeout})");
                Thread.Sleep(timeout);
            }
        }

        private static void ReadRss(string url,string source)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            foreach (SyndicationItem item in feed.Items)
            {
                Guid id = Guid.NewGuid();

                News news = new News()
                {
                    Id = id,
                    Title = item.Title.Text,
                    Link = item.Id,
                    Source = source,
                    Description = item.Summary.Text,
                    PublishDate = item.PublishDate.DateTime
                };


                using (ApplicationContext db = new ApplicationContext())
                {
                    if (db.IsExists(news))
                        return;
                    db.Feed.Add(news);
                    db.SaveChanges();
                }


                Console.WriteLine("________________________________");
                Console.WriteLine("NEW ITEM ADDED");
                Console.WriteLine($"GUID: {news.Id}");
                Console.WriteLine($"TITLE: {news.Title}");
                Console.WriteLine();
            }
        }
    }
}
