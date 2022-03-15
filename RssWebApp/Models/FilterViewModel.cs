using ClassLibrary.Models;
using System.Web.Mvc;

namespace RssWebApp.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<News> feed, string title, string? source)
        {
            Feed = new SelectList(feed, "Id", "Name", title);
            SelectedName = title;
            SelectedSource = source??"";
        }
        public SelectList Feed { get; private set; }
        public string SelectedName { get; private set; }
        public string SelectedSource { get; private set; }
    }

}
