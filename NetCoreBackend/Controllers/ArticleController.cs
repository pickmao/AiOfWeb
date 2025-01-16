using Microsoft.AspNetCore.Mvc;
using NetCoreBackend.Models;
using NetCoreBackend.Services;

namespace NetCoreBackend.Controllers
{
    /// <summary>
    /// 文章控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        /// <summary>
        /// 获取分页文章列表
        /// </summary>
        /// <param name="parameters">查询参数</param>
        /// <returns>分页文章列表</returns>
        [HttpGet("list")]
        public async Task<ActionResult<PagedList<Article>>> GetArticles([FromQuery] ArticleQueryParameters parameters)
        {
            var articles = await _articleService.GetArticlesAsync(parameters);

            // 添加分页信息到响应头
            Response.Headers.Add("X-Pagination-Total", articles.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-Pages", articles.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasPrev", articles.HasPrevious.ToString());
            Response.Headers.Add("X-Pagination-HasNext", articles.HasNext.ToString());

            return Ok(articles);
        }

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns>文章列表</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return Ok(articles);
        }

        /// <summary>
        /// 根据ID获取文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns>文章详情</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        /// <summary>
        /// 创建新文章
        /// </summary>
        /// <param name="article">文章内容</param>
        /// <returns>创建的文章</returns>
        [HttpPost]
        public async Task<ActionResult<Article>> CreateArticle([FromBody] Article article)
        {
            var createdArticle = await _articleService.CreateArticleAsync(article);
            return CreatedAtAction(
                nameof(GetArticle),
                new { id = createdArticle.Id },
                createdArticle);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <param name="article">更新的文章内容</param>
        /// <returns>更新后的文章</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Article>> UpdateArticle(int id, [FromBody] Article article)
        {
            var updatedArticle = await _articleService.UpdateArticleAsync(id, article);
            if (updatedArticle == null)
                return NotFound();

            return Ok(updatedArticle);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns>删除结果</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            var result = await _articleService.DeleteArticleAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
