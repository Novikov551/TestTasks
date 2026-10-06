using System.ComponentModel.DataAnnotations;
namespace TestTask5_2.Api.Endpoints.Books.Models.Requests;

public sealed class CreateBookRequest
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(500)]
    public required string Title { get; init; }

    [Required(AllowEmptyStrings = false)]
    [MaxLength(300)]
    public required string Author { get; init; }

    [Required]
    [Range(1000, 2099)]
    public required int Year { get; init; }

    [MaxLength(300)]
    public string? Publisher { get; init; }

    [MaxLength(20)]
    public string? Isbn { get; init; }

    [MaxLength(2000)]
    public string? Description { get; init; }

    public string? TableOfContentsXml { get; init; }
}