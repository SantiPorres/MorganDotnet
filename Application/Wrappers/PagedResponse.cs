namespace Application.Wrappers
{
    public class PagedResponse<T> : Response<T> where T : class
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int? NextPageNumber { get; set; }
        public int? PreviousPageNumber { get; set; }

        public PagedResponse(
            T data,
            string? message,
            int totalCount,
            int pageSize,
            int currentPage,
            bool hasNextPage,
            bool hasPreviousPage,
            int? nextPageNumber,
            int? previousPageNumber
        ) : base(data, message)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;
            NextPageNumber = nextPageNumber;
            PreviousPageNumber = previousPageNumber;
        }
    }
}
