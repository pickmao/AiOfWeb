using System.ComponentModel.DataAnnotations;

namespace NetCoreBackend.Models
{
    /// <summary>
    /// 文章实体类
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(200, ErrorMessage = "标题长度不能超过200个字符")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 文章内容（Markdown格式）
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }
    }
}
