namespace NetCoreBackend.Models
{
    /// <summary>
    /// 文章查询参数
    /// </summary>
    public class ArticleQueryParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        /// <summary>
        /// 页码，从1开始
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 每页大小，默认10，最大50
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        /// <summary>
        /// 搜索关键词（标题）
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// 排序字段（CreatedAt, UpdatedAt, ViewCount）
        /// </summary>
        public string? OrderBy { get; set; } = "CreatedAt";

        /// <summary>
        /// 是否降序排序
        /// </summary>
        public bool Descending { get; set; } = true;
    }
}
