using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS
{
    public class PagedSearch<T>
    {
        public PagedSearch()
        {

        }

        public PagedSearch(int currentPage, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PagedSearch(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PagedSearch(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
            Filters = filters;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortDirections { get; set; }
        public string SortFields { get; set; }
        public Dictionary<string, object> Filters { get; set; }
        public List<T> List { get; set; }

        public int GetCurrentPage()
        {
            return CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize;
        }
    }
}
