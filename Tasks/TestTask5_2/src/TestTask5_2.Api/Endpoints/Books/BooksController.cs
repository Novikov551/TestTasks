using Microsoft.AspNetCore.Mvc;

using TestTask5_2.Api.Endpoints.Books.Models.Requests;
using TestTask5_2.Api.Endpoints.Books.Models.Responses;
using TestTask5_2.Api.Endpoints.Books.Services;

namespace TestTask5_2.Api.Endpoints.Books;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookWebApiAdapter _adapter;

    public BooksController(BookWebApiAdapter adapter)
    {
        _adapter = adapter;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BookListItemResponse>), 200)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? query,
        [FromQuery] string? field,
        CancellationToken ct = default)
    {
        var books = await _adapter.SearchAsync(query, field, ct);
        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken ct = default)
    {
        var book = await _adapter.GetByIdAsync(id, ct);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookResponse), 201)]
    public async Task<IActionResult> Create(
        [FromBody] CreateBookRequest request,
        CancellationToken ct = default)
    {
        var book = await _adapter.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BookResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateBookRequest request,
        CancellationToken ct = default)
    {
        var book = await _adapter.UpdateAsync(id, request, ct);
        return Ok(book);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken ct = default)
    {
        await _adapter.DeleteAsync(id, ct);
        return NoContent();
    }
}