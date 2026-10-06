using TestTask5_2.Domain.Aggregates.Books.Models;

namespace TestTask5_2.Domain.Aggregates.Books;

public interface IBookService
{
    Task<List<BookListItemDto>> GetAllAsync(CancellationToken ct = default);
    Task<BookDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<BookListItemDto>> SearchAsync(string? query, string? field, CancellationToken ct = default);
    Task<BookDto> CreateAsync(CreateBookDto dto, CancellationToken ct = default);
    Task<BookDto> UpdateAsync(Guid id, UpdateBookDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}