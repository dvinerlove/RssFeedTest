namespace RssWebApp.Models
{
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

}
