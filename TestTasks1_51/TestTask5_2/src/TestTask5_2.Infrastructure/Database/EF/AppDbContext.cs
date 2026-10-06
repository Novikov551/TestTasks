using Microsoft.EntityFrameworkCore;

using TestTask5_2.Domain.Aggregates.Books.Entities;

namespace TestTask5_2.Infrastructure.Database.EF;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}