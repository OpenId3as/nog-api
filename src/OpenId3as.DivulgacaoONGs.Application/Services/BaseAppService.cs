using OpenId3as.DivulgacaoONGs.Application.DataTransferObject.PagedSearch;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Application.Services
{
    public class BaseAppService
    {
        protected int ValidateLimitRows(int limitRows)
        {
            if (limitRows < 0 && limitRows > 500)
                limitRows = 50;
            return limitRows;
        }

        protected int ValidatePageNumber(int page)
        {
            if (page < 0)
                page = 0;
            return page;
        }

        protected List<SortItemDTO> ValidateSort(Dictionary<string, string> sortableColumns, List<SortItemDTO> sortItems)
        {
            return sortItems.Select(x => new SortItemDTO()
            {
                Direction = x.Direction.ToUpper() == "ASC" ? "ASC" : "DESC",
                Field = sortableColumns.Where(c => c.Key.ToLower().Contains(x.Field.ToLower())).FirstOrDefault().Value,
                Priority = x.Priority
            }).OrderBy(x => x.Priority).ToList();

        }
    }
}
