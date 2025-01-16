namespace NetCoreBackend.Models
{
    /// <summary>
    /// 分页结果
    /// </summary>
    public class PagedList<T>
    {
        /// <summary>
        /// 当前页数据
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrevious => PageNumber > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNext => PageNumber < TotalPages;

        public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
