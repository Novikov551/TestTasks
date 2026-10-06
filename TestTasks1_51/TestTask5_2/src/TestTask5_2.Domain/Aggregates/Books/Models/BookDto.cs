using System.Diagnostics.CodeAnalysis;

using TestTask5_2.Domain.Aggregates.Books.Entities;

namespace TestTask5_2.Domain.Aggregates.Books.Models;

public class BookDto
{
    [SetsRequiredMembers]
    public BookDto(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Author = book.Author;
        Year = book.Year;
        Publisher = book.Publisher;
        Isbn = book.Isbn;
        Description = book.Description;
        TableOfContentsXml = book.TableOfContentsXml;
        CreatedDate = book.CreatedDate;
        ModifiedDate = book.ModifiedDate;
    }

    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required int Year { get; init; }
    public string? Publisher { get; init; }
    public string? Isbn { get; init; }
    public string? Description { get; init; }
    public required string TableOfContentsXml { get; init; }
    public required DateTime CreatedDate { get; init; }
    public required DateTime ModifiedDate { get; init; }
}
