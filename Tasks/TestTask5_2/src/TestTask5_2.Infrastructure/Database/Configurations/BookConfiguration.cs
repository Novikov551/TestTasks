using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestTask5_2.Domain.Aggregates.Books.Entities;

namespace TestTask5_2.Infrastructure.Database.Configurations;

internal sealed class BookConfiguration : BaseEntityConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("books");
        base.Configure(builder);

        builder.Property(b => b.Title)
            .HasColumnName("title")
            .HasComment("Название книги")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(b => b.Author)
            .HasColumnName("author")
            .HasComment("Автор")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(b => b.Year)
            .HasColumnName("year")
            .HasComment("Год издания")
            .IsRequired();

        builder.Property(b => b.Publisher)
            .HasColumnName("publisher")
            .HasComment("Издательство")
            .HasMaxLength(300);

        builder.Property(b => b.Isbn)
            .HasColumnName("isbn")
            .HasComment("ISBN")
            .HasMaxLength(20);

        builder.Property(b => b.Description)
            .HasColumnName("description")
            .HasComment("Описание")
            .HasMaxLength(2000);

        builder.Property(b => b.TableOfContentsXml)
            .HasColumnName("table_of_contents_xml")
            .HasComment("Оглавление в формате XML")
            .IsRequired();
    }
}