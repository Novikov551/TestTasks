using TestTask5_2.Domain.Aggregates.Books.Models;

namespace TestTask5_2.Domain.Aggregates.Books.Entities;

public class Book : BaseEntity
{
    private Book()
    {
        TableOfContentsXml = string.Empty;
    }

    public static Book FromDatabase(
        Guid id,
        string title,
        string author,
        int year,
        string? publisher,
        string? isbn,
        string? description,
        string tableOfContentsXml,
        DateTime createdDate,
        DateTime modifiedDate)
    {
        return new Book
        {
            Id = id,
            Title = title,
            Author = author,
            Year = year,
            Publisher = publisher,
            Isbn = isbn,
            Description = description,
            TableOfContentsXml = tableOfContentsXml,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate
        };
    }

    public Book(
        string title,
        string author,
        int year,
        string? publisher,
        string? isbn,
        string? description,
        string tableOfContentsXml)
    {
        Id = Guid.NewGuid();
        Title = title;
        Author = author;
        Year = year;
        Publisher = publisher;
        Isbn = isbn;
        Description = description;
        TableOfContentsXml = tableOfContentsXml;
        CreatedDate = DateTime.UtcNow;
        ModifiedDate = DateTime.UtcNow;
        State = EntityState.Created;
    }

    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public string? Publisher { get; private set; }
    public string? Isbn { get; private set; }
    public string? Description { get; private set; }
    public string TableOfContentsXml { get; private set; }

    public bool Revise(UpdateBookDto dto)
    {
        if (IsFieldsEquals(dto))
        {
            return false;
        }

        Title = dto.Title;
        Author = dto.Author;
        Year = dto.Year;
        Publisher = dto.Publisher;
        Isbn = dto.Isbn;
        Description = dto.Description;
        TableOfContentsXml = dto.TableOfContentsXml ?? TableOfContentsXml;

        SetUpdated();

        return true;
    }

    private bool IsFieldsEquals(UpdateBookDto dto)
    {
        return Title == dto.Title
               && Author == dto.Author
               && Year == dto.Year
               && Publisher == dto.Publisher
               && Isbn == dto.Isbn
               && Description == dto.Description
               && (dto.TableOfContentsXml is null || TableOfContentsXml == dto.TableOfContentsXml);
    }
}