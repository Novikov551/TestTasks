namespace TestTask5_2.Domain.Aggregates.Books.Models;

public class CreateBookDto
{
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required int Year { get; init; }
    public string? Publisher { get; init; }
    public string? Isbn { get; init; }
    public string? Description { get; init; }
    public string? TableOfContentsXml { get; init; }
}
