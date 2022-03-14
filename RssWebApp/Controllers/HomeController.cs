using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Index(string? name, int page = 1,
                 SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;
            using (ApplicationContext db = new ApplicationContext())
            {
                name = (name ?? "");
                //фильтрация
                List<News> users = db.Feed.ToList();

                if (!string.IsNullOrEmpty(name))
                {
                    users = users.Where(p => p.Title.Contains(name)).ToList();
                }

                // сортировка
                switch (sortOrder)
                {
                    case SortState.NameDesc:
                        users = users.OrderByDescending(s => s.Title).ToList();
                        break;
                    default:
                        users = users.OrderBy(s => s.Title).ToList();
                        break;
                }

                // пагинация
                var count = users.Count();
                var items = users.Skip((page - 1) * pageSize).Take(pageSize).Where(p => p.Title.Contains(name)).ToList();

                // формируем модель представления
                IndexViewModel viewModel = new IndexViewModel
                {
                    PageViewModel = new PageViewModel(count, page, pageSize),
                    SortViewModel = new SortViewModel(sortOrder),
                    FilterViewModel = new FilterViewModel(items, name),
                    Feed = items
                };
                return View(viewModel);
            }

        }
    }
}