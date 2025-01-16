using Microsoft.EntityFrameworkCore;
using NetCoreBackend.Models;

namespace NetCoreBackend.Data
{
    /// <summary>
    /// 博客数据库上下文
    /// </summary>
    public class BlogDbContext : DbContext
    {
        /// <summary>
        /// 文章表
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// 配置数据库连接
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database/blog.db");
        }
    }
}
