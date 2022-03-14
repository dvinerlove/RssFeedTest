using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace RssWebApp.Models
{
    public class IndexViewModel
    {
        public IEnumerable<News> Feed { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
    public class SortViewModel
    {
        public SortState NameSort { get; private set; }
        public SortState DateSort { get; private set; }  
        public SortState SourceSort { get; private set; } 
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            SourceSort = sortOrder == SortState.SourceAsc ? SortState.SourceDesc : SortState.SourceAsc;
            Current = sortOrder;
        }
    }
    public enum SortState
    {
        NameAsc,
        NameDesc,
        DateAsc,
        DateDesc,
        SourceAsc,
        SourceDesc,
    }
    public class FilterViewModel
    {
        public FilterViewModel(List<News> companies, string title)
        {
            Companies = new SelectList(companies, "Id", "Name", title);
            SelectedName = title;
        }
        public SelectList Companies { get; private set; } 
        public string SelectedName { get; private set; } 
    }

}
