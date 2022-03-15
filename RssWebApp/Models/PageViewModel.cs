namespace RssWebApp.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = count;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 0);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return PageNumber < (TotalPages -1);
            }
        }
    }

}
