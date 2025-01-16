using NetCoreBackend.Data;
using System.IO;

namespace NetCoreBackend.Database
{
    /// <summary>
    /// 数据库初始化静态类
    /// </summary>
    public static class InitDatabase
    {
        /// <summary>
        /// 确保数据库创建
        /// </summary>
        public static void EnsureCreated()
        {
            try
            {
                // 确保Database目录存在
                var databaseDir = Path.GetDirectoryName("Database/blog.db");
                if (!Directory.Exists(databaseDir))
                {
                    Directory.CreateDirectory(databaseDir!);
                }

                // 使用 EF Core 创建数据库
                using var context = new BlogDbContext();
                context.Database.EnsureCreated();
                
                Console.WriteLine("数据库初始化成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库初始化错误: {ex.Message}");
                throw;
            }
        }
    }
}
