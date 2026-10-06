using TestTask5_2.Api.Endpoints.Books.Models.Requests;
using TestTask5_2.Domain.Aggregates.Books.Models;

namespace TestTask5_2.Api.Endpoints.Books.Services;

public static class BookRequestExtensions
{
    public static CreateBookDto ToDto(this CreateBookRequest request)
    {
        return new CreateBookDto
        {
            Title = request.Title,
            Author = request.Author,
            Year = request.Year,
            Publisher = request.Publisher,
            Isbn = request.Isbn,
            Description = request.Description,
            TableOfContentsXml = request.TableOfContentsXml
        };
    }

    public static UpdateBookDto ToDto(this UpdateBookRequest request)
    {
        return new UpdateBookDto
        {
            Title = request.Title,
            Author = request.Author,
            Year = request.Year,
            Publisher = request.Publisher,
            Isbn = request.Isbn,
            Description = request.Description,
            TableOfContentsXml = request.TableOfContentsXml
        };
    }
}