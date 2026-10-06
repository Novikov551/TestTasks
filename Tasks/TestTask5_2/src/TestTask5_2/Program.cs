using Microsoft.EntityFrameworkCore;

using TestTask5_2.Api;

using TestTask5_2.Api.Endpoints.Books.Services;
using TestTask5_2.Domain.Aggregates.Books;
using TestTask5_2.Domain.Aggregates.Books.Entities;
using TestTask5_2.Domain.Interfaces;
using TestTask5_2.Infrastructure.Database.EF;
using TestTask5_2.Infrastructure.Database.Repositories;
using TestTask5_2.Logic.Books;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseEfRepository<>));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<BookWebApiAdapter>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Домашняя библиотека API",
        Version = "v1",
        Description = "API для управления домашней библиотекой"
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(db);
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();