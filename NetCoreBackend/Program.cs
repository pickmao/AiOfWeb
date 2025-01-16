using Microsoft.OpenApi.Models;
using NetCoreBackend.Data;
using NetCoreBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 配置Swagger文档
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "个人网站API文档",
        Version = "v1",
        Description = "这是一个用于管理Markdown文章的API接口文档",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "作者名字",
            Email = "your.email@example.com"
        }
    });

    // 添加XML注释文档
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// 注册数据库上下文和文章服务
builder.Services.AddDbContext<BlogDbContext>();
builder.Services.AddScoped<IArticleService, ArticleService>();

// 添加CORS支持
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder => builder
            .WithOrigins("http://localhost:5173") // Vue.js 开发服务器的默认地址
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// 初始化数据库
NetCoreBackend.Database.InitDatabase.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "个人网站API V1");
        c.RoutePrefix = "api-docs"; // 可以通过 /api-docs 访问文档
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowVueApp");
app.MapControllers();

app.Run();
