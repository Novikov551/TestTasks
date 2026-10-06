using System.Diagnostics.CodeAnalysis;

using TestTask5_2.Domain.Aggregates.Books.Entities;

namespace TestTask5_2.Domain.Aggregates.Books.Models;

public class BookListItemDto
{
    [SetsRequiredMembers]
    public BookListItemDto(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Author = book.Author;
        Year = book.Year;
        Publisher = book.Publisher;
    }

    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required int Year { get; init; }
    public string? Publisher { get; init; }
}
