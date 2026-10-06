using System.Diagnostics.CodeAnalysis;

using TestTask5_2.Domain.Aggregates.Books.Models;

namespace TestTask5_2.Api.Endpoints.Books.Models.Responses;

public sealed class BookListItemResponse
{
    [SetsRequiredMembers]
    public BookListItemResponse(BookListItemDto dto)
    {
        Id = dto.Id;
        Title = dto.Title;
        Author = dto.Author;
        Year = dto.Year;
        Publisher = dto.Publisher;
    }

    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required int Year { get; init; }
    public string? Publisher { get; init; }
}