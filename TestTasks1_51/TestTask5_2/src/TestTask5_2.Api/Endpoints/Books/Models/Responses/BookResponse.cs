using System.Diagnostics.CodeAnalysis;
using TestTask5_2.Domain.Aggregates.Books.Models;

namespace TestTask5_2.Api.Endpoints.Books.Models.Responses;

public sealed class BookResponse
{
    [SetsRequiredMembers]
    public BookResponse(BookDto dto)
    {
        Id = dto.Id;
        Title = dto.Title;
        Author = dto.Author;
        Year = dto.Year;
        Publisher = dto.Publisher;
        Isbn = dto.Isbn;
        Description = dto.Description;
        TableOfContentsXml = dto.TableOfContentsXml;
        CreatedDate = dto.CreatedDate;
        ModifiedDate = dto.ModifiedDate;
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