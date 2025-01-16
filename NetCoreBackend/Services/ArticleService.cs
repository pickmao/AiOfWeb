using Microsoft.EntityFrameworkCore;
using NetCoreBackend.Data;
using NetCoreBackend.Models;

namespace NetCoreBackend.Services
{
    /// <summary>
    /// 文章服务接口
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// 获取分页文章列表
        /// </summary>
        Task<PagedList<Article>> GetArticlesAsync(ArticleQueryParameters parameters);

        /// <summary>
        /// 获取所有文章
        /// </summary>
        Task<List<Article>> GetAllArticlesAsync();

        /// <summary>
        /// 根据ID获取文章
        /// </summary>
        Task<Article?> GetArticleByIdAsync(int id);

        /// <summary>
        /// 创建新文章
        /// </summary>
        Task<Article> CreateArticleAsync(Article article);

        /// <summary>
        /// 更新文章
        /// </summary>
        Task<Article?> UpdateArticleAsync(int id, Article article);

        /// <summary>
        /// 删除文章
        /// </summary>
        Task<bool> DeleteArticleAsync(int id);
    }

    /// <summary>
    /// 文章服务实现类
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly BlogDbContext _context;

        public ArticleService(BlogDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取分页文章列表
        /// </summary>
        public async Task<PagedList<Article>> GetArticlesAsync(ArticleQueryParameters parameters)
        {
            var query = _context.Articles.AsQueryable();

            // 应用搜索
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(a => a.Title.Contains(parameters.SearchTerm));
            }

            // 应用排序
            query = parameters.OrderBy?.ToLower() switch
            {
                "createdat" => parameters.Descending ? 
                    query.OrderByDescending(a => a.CreatedAt) : 
                    query.OrderBy(a => a.CreatedAt),
                "updatedat" => parameters.Descending ? 
                    query.OrderByDescending(a => a.UpdatedAt) : 
                    query.OrderBy(a => a.UpdatedAt),
                "viewcount" => parameters.Descending ? 
                    query.OrderByDescending(a => a.ViewCount) : 
                    query.OrderBy(a => a.ViewCount),
                _ => parameters.Descending ? 
                    query.OrderByDescending(a => a.CreatedAt) : 
                    query.OrderBy(a => a.CreatedAt)
            };

            // 获取总记录数
            var totalCount = await query.CountAsync();

            // 应用分页
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedList<Article>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }

        /// <summary>
        /// 获取所有文章，按创建时间降序排序
        /// </summary>
        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// 根据ID获取单个文章
        /// </summary>
        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }

        /// <summary>
        /// 创建新文章，自动设置创建时间和更新时间
        /// </summary>
        public async Task<Article> CreateArticleAsync(Article article)
        {
            article.CreatedAt = DateTime.Now;
            article.UpdatedAt = DateTime.Now;
            
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            
            return article;
        }

        /// <summary>
        /// 更新文章，自动更新修改时间
        /// </summary>
        public async Task<Article?> UpdateArticleAsync(int id, Article article)
        {
            var existingArticle = await _context.Articles.FindAsync(id);
            if (existingArticle == null)
                return null;

            existingArticle.Title = article.Title;
            existingArticle.Content = article.Content;
            existingArticle.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingArticle;
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
