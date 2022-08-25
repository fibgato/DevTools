using Microsoft.EntityFrameworkCore;

namespace NetDevTools.Core.Framework
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResult<T>> GetPagination<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            if (pageSize <= 0) pageSize = 100;
            if (pageSize > 100) pageSize = 100;

            var result = new PagedResult<T>();
            result.PageIndex = page;
            result.PageSize = pageSize;
            result.TotalResults = await query.CountAsync();

            var pageCount = (double)result.TotalResults / pageSize;
            result.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.List = await query.AsNoTrackingWithIdentityResolution().Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }

    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
    }
}
