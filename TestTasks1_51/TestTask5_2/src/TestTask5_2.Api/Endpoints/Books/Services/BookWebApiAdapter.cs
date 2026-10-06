using TestTask5_2.Api.Endpoints.Books.Models.Requests;
using TestTask5_2.Api.Endpoints.Books.Models.Responses;
using TestTask5_2.Domain.Aggregates.Books;

namespace TestTask5_2.Api.Endpoints.Books.Services;

public class BookWebApiAdapter
{
    private readonly IBookService _bookService;

    public BookWebApiAdapter(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<List<BookListItemResponse>> GetAllAsync(
        CancellationToken ct = default)
    {
        var books = await _bookService.GetAllAsync(ct);
        return books.ConvertAll(b => new BookListItemResponse(b));
    }

    public async Task<BookResponse?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var book = await _bookService.GetByIdAsync(id, ct);

        if (book is null)
        {
            return null;
        }

        return new BookResponse(book);
    }

    public async Task<List<BookListItemResponse>> SearchAsync(
        string? query,
        string? field,
        CancellationToken ct = default)
    {
        var books = await _bookService.SearchAsync(query, field, ct);
        return books.ConvertAll(b => new BookListItemResponse(b));
    }

    public async Task<BookResponse> CreateAsync(
        CreateBookRequest request,
        CancellationToken ct = default)
    {
        var book = await _bookService.CreateAsync(request.ToDto(), ct);
        return new BookResponse(book);
    }

    public async Task<BookResponse> UpdateAsync(
        Guid id,
        UpdateBookRequest request,
        CancellationToken ct = default)
    {
        var book = await _bookService.UpdateAsync(id, request.ToDto(), ct);
        return new BookResponse(book);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct = default)
    {
        await _bookService.DeleteAsync(id, ct);
    }
}