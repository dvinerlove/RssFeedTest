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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
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

        public IActionResult Index(string? name, string? source, int page = 1,
                 SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;
            using (ApplicationContext db = new ApplicationContext())
            {
                name = (name ?? "");

                List<News> users = db.Feed.ToList();

                if (!string.IsNullOrEmpty(name))
                {
                    users = users.Where(p => p.Title.Contains(name)).ToList();
                }

                if (!string.IsNullOrEmpty(source))
                {
                    users = users.Where(p => p.Source.Contains(source)).ToList();
                }

                switch (sortOrder)
                {
                    case SortState.NameDesc:
                        users = users.OrderByDescending(s => s.Title).ToList();
                        break;

                    case SortState.DateDesc:
                        users = users.OrderByDescending(s => s.PublishDate).ToList();
                        break;
                    case SortState.DateAsc:
                        users = users.OrderBy(s => s.PublishDate).ToList();
                        break;
                    case SortState.SourceAsc:
                        users = users.OrderByDescending(s => s.PublishDate).ToList();
                        break;
                    case SortState.SourceDesc:
                        users = users.OrderBy(s => s.PublishDate).ToList();
                        break;
                    default:
                        users = users.OrderBy(s => s.Title).ToList();
                        break;
                }

                var count = users.Count();
                var items = users.Skip((page - 1) * pageSize).Take(pageSize).Where(p => p.Title.Contains(name)).ToList();

                IndexViewModel viewModel = new IndexViewModel
                {
                    PageViewModel = new PageViewModel(count, page, pageSize),
                    SortViewModel = new SortViewModel(sortOrder),
                    FilterViewModel = new FilterViewModel(items, name, source),
                    Feed = items
                };
                return View(viewModel);
            }

        }



        [HttpPost]
        public List<News> GetData(string title, string source, int page)
        {
            //return number1 + number2;
            using (var db = new ApplicationContext())
            {
                List<News> list = db.Feed.Skip(page * 10).Take(10).ToList();
                return list;
            }
        }

        [HttpGet]
        public int GetPagesCount()
        {
            using (var db = new ApplicationContext())
            {
                return db.Feed.ToList().Count/10;
            }
        }
    }
}