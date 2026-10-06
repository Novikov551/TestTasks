using TestTask5_2.Domain.Aggregates.Books;
using TestTask5_2.Domain.Aggregates.Books.Entities;
using TestTask5_2.Domain.Aggregates.Books.Models;
using TestTask5_2.Domain.Interfaces;

namespace TestTask5_2.Logic.Books;

internal sealed class BookService : IBookService
{
    private readonly IRepository<Book> _repository;

    public BookService(IRepository<Book> repository)
    {
        _repository = repository;
    }

    public async Task<List<BookListItemDto>> GetAllAsync(
        CancellationToken ct = default)
    {
        var books = await _repository.GetAllAsync(ct);
        return books
            .OrderBy(b => b.Title)
            .ToList()
            .ConvertAll(b => new BookListItemDto(b));
    }

    public async Task<BookDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var book = await _repository.FindAsync(id, ct);

        if (book is null)
        {
            return null;
        }

        return new BookDto(book);
    }

    public async Task<List<BookListItemDto>> SearchAsync(
        string? query,
        string? field,
        CancellationToken ct = default)
    {
        var books = await _repository.GetAllAsync(ct);

        if (string.IsNullOrWhiteSpace(query))
        {
            return books
                .OrderBy(b => b.Title)
                .ToList()
                .ConvertAll(b => new BookListItemDto(b));
        }

        IEnumerable<Book> filtered = field?.ToLower() switch
        {
            "author" => books.Where(b =>
                b.Author.Contains(query, StringComparison.OrdinalIgnoreCase)),
            "tableofcontents" => books.Where(b =>
                b.TableOfContentsXml.Contains(query, StringComparison.OrdinalIgnoreCase)),
            _ => books.Where(b =>
                b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
        };

        return filtered
            .OrderBy(b => b.Title)
            .ToList()
            .ConvertAll(b => new BookListItemDto(b));
    }

    public async Task<BookDto> CreateAsync(
        CreateBookDto dto,
        CancellationToken ct = default)
    {
        var book = new Book(
            dto.Title,
            dto.Author,
            dto.Year,
            dto.Publisher,
            dto.Isbn,
            dto.Description,
            dto.TableOfContentsXml ?? "<tableOfContents></tableOfContents>");

        _repository.Create(book);
        await _repository.SaveChangesAsync(ct);

        return new BookDto(book);
    }

    public async Task<BookDto> UpdateAsync(
        Guid id,
        UpdateBookDto dto,
        CancellationToken ct = default)
    {
        var book = await _repository.FindAsync(id, ct);

        if (book is null)
        {
            throw new KeyNotFoundException($"Книга с Id {id} не найдена");
        }

        var hasChanges = book.Revise(dto);

        if (hasChanges)
        {
            _repository.Update(book);
            await _repository.SaveChangesAsync(ct);
        }

        return new BookDto(book);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var book = await _repository.FindAsync(id, ct);

        if (book is null)
        {
            throw new KeyNotFoundException($"Книга с Id {id} не найдена");
        }

        _repository.Remove(book);
        await _repository.SaveChangesAsync(ct);
    }
}