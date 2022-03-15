using ClassLibrary.Models;

namespace RssWebApp.Models
{
    public class IndexViewModel
    {
        public IEnumerable<News> Feed { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }

}
