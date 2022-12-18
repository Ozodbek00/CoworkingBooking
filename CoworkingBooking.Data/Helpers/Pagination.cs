namespace CoworkingBooking.Data.Helpers
{
    public static class Pagination
    {
        /// <summary>
        /// Paginates DbSet according to the pageIndex and PageSize.
        /// </summary>
        public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> source, int pageIndex, int pageSize)
        {
            if (pageSize > 0)
            {
                return pageIndex > 0
                ? source.Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                : source.Take(pageSize);
            }

            return pageIndex > 0
                ? source.Skip((pageIndex - 1) * PageSize)
                        .Take(PageSize)
                : source.Take(PageSize);
        }

        /// <summary>
        /// Size of the page.
        /// </summary>
        public const int PageSize = 20;
    }
}
