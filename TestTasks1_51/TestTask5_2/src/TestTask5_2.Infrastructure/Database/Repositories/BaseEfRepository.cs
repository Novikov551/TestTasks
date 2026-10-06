using System.Data;

using Microsoft.EntityFrameworkCore;

using Npgsql;

using TestTask5_2.Domain;
using TestTask5_2.Domain.Aggregates.Books.Entities;
using TestTask5_2.Domain.Interfaces;
using TestTask5_2.Infrastructure.Database.EF;

namespace TestTask5_2.Infrastructure.Database.Repositories;

internal sealed class BaseEfRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly string _connectionString;
    private readonly List<T> _pendingCreate = [];
    private readonly List<T> _pendingUpdate = [];
    private readonly List<T> _pendingDelete = [];

    public BaseEfRepository(AppDbContext context)
    {
        _context = context;
        _connectionString = context.Database.GetConnectionString()!;
    }

    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken ct = default)
    {
        if (typeof(T) == typeof(Book))
        {
            return (await SelectBooksAsync("fn_books_select_all", null, ct)).Cast<T>().ToList();
        }

        return await _context.Set<T>().ToListAsync(ct);
    }

    public async Task<T?> FindAsync(Guid id, CancellationToken ct = default)
    {
        if (typeof(T) == typeof(Book))
        {
            var parameters = new[] { new NpgsqlParameter("p_id", id) };
            var books = await SelectBooksAsync("fn_books_select_by_id", parameters, ct);
            return books.FirstOrDefault() as T;
        }

        return await _context.Set<T>().FindAsync(new object[] { id }, ct);
    }

    public T Create(T entity)
    {
        entity.State = BaseEntity.EntityState.Created;
        _pendingCreate.Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        entity.SetUpdated();
        _pendingUpdate.Add(entity);
        return entity;
    }

    public void Remove(T entity)
    {
        entity.SetToDelete();
        _pendingDelete.Add(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var affected = 0;

        if (_pendingCreate.Count > 0 || _pendingUpdate.Count > 0 || _pendingDelete.Count > 0)
        {
            foreach (var entity in _pendingCreate)
            {
                if (entity is Book book)
                {
                    await InsertBookAsync(book, ct);
                    affected++;
                }
            }

            foreach (var entity in _pendingUpdate)
            {
                if (entity is Book book)
                {
                    var updated = await UpdateBookAsync(book, ct);
                    if (updated)
                    {
                        affected++;
                    }
                }
            }

            foreach (var entity in _pendingDelete)
            {
                if (entity is Book book)
                {
                    var deleted = await DeleteBookAsync(book.Id, ct);
                    if (deleted)
                    {
                        affected++;
                    }
                }
            }

            _pendingCreate.Clear();
            _pendingUpdate.Clear();
            _pendingDelete.Clear();
        }

        affected += await _context.SaveChangesAsync(ct);

        return affected;
    }

    private async Task InsertBookAsync(Book book, CancellationToken ct)
    {
        var sql = "SELECT fn_books_insert(@p_id, @p_title, @p_author, @p_year, @p_publisher, @p_isbn, @p_description, @p_table_of_contents_xml)";

        var parameters = new[]
        {
            new NpgsqlParameter("p_id", book.Id),
            new NpgsqlParameter("p_title", book.Title),
            new NpgsqlParameter("p_author", book.Author),
            new NpgsqlParameter("p_year", book.Year),
            new NpgsqlParameter("p_publisher", (object?)book.Publisher ?? DBNull.Value),
            new NpgsqlParameter("p_isbn", (object?)book.Isbn ?? DBNull.Value),
            new NpgsqlParameter("p_description", (object?)book.Description ?? DBNull.Value),
            new NpgsqlParameter("p_table_of_contents_xml", book.TableOfContentsXml)
        };

        await ExecuteFunctionAsync(sql, parameters, ct);
    }

    private async Task<bool> UpdateBookAsync(Book book, CancellationToken ct)
    {
        var sql = "SELECT fn_books_update(@p_id, @p_title, @p_author, @p_year, @p_publisher, @p_isbn, @p_description, @p_table_of_contents_xml)";

        var parameters = new[]
        {
            new NpgsqlParameter("p_id", book.Id),
            new NpgsqlParameter("p_title", book.Title),
            new NpgsqlParameter("p_author", book.Author),
            new NpgsqlParameter("p_year", book.Year),
            new NpgsqlParameter("p_publisher", (object?)book.Publisher ?? DBNull.Value),
            new NpgsqlParameter("p_isbn", (object?)book.Isbn ?? DBNull.Value),
            new NpgsqlParameter("p_description", (object?)book.Description ?? DBNull.Value),
            new NpgsqlParameter("p_table_of_contents_xml", book.TableOfContentsXml)
        };

        var result = await ExecuteFunctionScalarAsync(sql, parameters, ct);
        return result is true;
    }

    private async Task<bool> DeleteBookAsync(Guid id, CancellationToken ct)
    {
        var sql = "SELECT fn_books_delete(@p_id)";
        var parameters = new[] { new NpgsqlParameter("p_id", id) };

        var result = await ExecuteFunctionScalarAsync(sql, parameters, ct);
        return result is true;
    }

    private async Task<List<Book>> SelectBooksAsync(
        string functionName,
        NpgsqlParameter[]? parameters,
        CancellationToken ct)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync(ct);

        await using var command = connection.CreateCommand();

        if (parameters is not null && parameters.Length > 0)
        {
            var placeholders = string.Join(", ", parameters.Select(p => $"@{p.ParameterName}"));
            command.CommandText = $"SELECT * FROM {functionName}({placeholders})";

            foreach (var param in parameters)
            {
                command.Parameters.Add(param);
            }
        }
        else
        {
            command.CommandText = $"SELECT * FROM {functionName}()";
        }

        await using var reader = await command.ExecuteReaderAsync(ct);
        var books = new List<Book>();

        while (await reader.ReadAsync(ct))
        {
            var book = Book.FromDatabase(
                reader.GetGuid(reader.GetOrdinal("o_id")),
                reader.GetString(reader.GetOrdinal("o_title")),
                reader.GetString(reader.GetOrdinal("o_author")),
                reader.GetInt32(reader.GetOrdinal("o_year")),
                reader.IsDBNull(reader.GetOrdinal("o_publisher")) ? null : reader.GetString(reader.GetOrdinal("o_publisher")),
                reader.IsDBNull(reader.GetOrdinal("o_isbn")) ? null : reader.GetString(reader.GetOrdinal("o_isbn")),
                reader.IsDBNull(reader.GetOrdinal("o_description")) ? null : reader.GetString(reader.GetOrdinal("o_description")),
                reader.GetString(reader.GetOrdinal("o_table_of_contents_xml")),
                reader.GetDateTime(reader.GetOrdinal("o_created_date")),
                reader.GetDateTime(reader.GetOrdinal("o_modified_date")));

            books.Add(book);
        }

        return books;
    }

    private async Task ExecuteFunctionAsync(string sql, NpgsqlParameter[] parameters, CancellationToken ct)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync(ct);

        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        foreach (var param in parameters)
        {
            command.Parameters.Add(param);
        }

        await command.ExecuteNonQueryAsync(ct);
    }

    private async Task<object?> ExecuteFunctionScalarAsync(string sql, NpgsqlParameter[] parameters, CancellationToken ct)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync(ct);

        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        foreach (var param in parameters)
        {
            command.Parameters.Add(param);
        }

        return await command.ExecuteScalarAsync(ct);
    }
}