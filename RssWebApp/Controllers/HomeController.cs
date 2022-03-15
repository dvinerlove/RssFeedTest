using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RssWebApp.Models;
using System.Diagnostics;

namespace RssWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly int pageSize = 10;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Ajax()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(string name, string source, int page = 0,
                 SortState sortOrder = SortState.DateDesc)
        {
            var list = GetData(name, source, page, sortOrder);
            var count = GetPagesCount(name, source);

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(list, name, source),
                Feed = list
            };
            return View(viewModel);
        }

        [HttpPost]
        public List<News> GetData(string title, string source, int page, SortState sortOrder = SortState.DateDesc)
        {
            return GetFeedList(title, source, page, sortOrder);
        }

        private List<News> GetFeedList(string title, string source, int page, SortState sortOrder)
        {
            using (var db = new ApplicationContext())
            {
                List<News> list = db.Feed.ToList();
                list = Filter(title, source, list);
                return Sort(list, sortOrder).Skip(page * pageSize).Take(pageSize).ToList();
            }
        }

        [HttpPost]
        public int GetPagesCount(string title, string source)
        {
            using (var db = new ApplicationContext())
            {
                List<News> list = db.Feed.ToList();
                list = Filter(title, source, list);
                return (list.Count / 10);
            }
        }

        private static List<News> Filter(string title, string source, List<News> list)
        {
            if (string.IsNullOrEmpty(title) == false)
            {
                list = list.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
            }
            if (string.IsNullOrEmpty(source) == false)
            {
                list = list.Where(x => x.Source.ToLower().Contains(source.ToLower())).ToList();
            }
            return list;
        }

        private List<News> Sort(List<News> list, SortState sortOrder)
        {
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    list = list.OrderByDescending(s => s.Title).ToList();
                    break;
                case SortState.NameAsc:
                    list = list.OrderBy(s => s.Title).ToList();
                    break;
                case SortState.DateDesc:
                    list = list.OrderByDescending(s => s.PublishDate).ToList();
                    break;
                case SortState.DateAsc:
                    list = list.OrderBy(s => s.PublishDate).ToList();
                    break;
                case SortState.SourceAsc:
                    list = list.OrderByDescending(s => s.PublishDate).ToList();
                    break;
                case SortState.SourceDesc:
                    list = list.OrderBy(s => s.PublishDate).ToList();
                    break;
                default:
                    list = list.OrderByDescending(s => s.PublishDate).ToList();
                    break;
            }
            return list;
        }
    }
}