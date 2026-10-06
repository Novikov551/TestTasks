using Microsoft.EntityFrameworkCore;

using TestTask5_2.Domain.Aggregates.Books.Entities;

namespace TestTask5_2.Infrastructure.Database.EF;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

                ExecuteSqlScript(context, "Database/StoredProcedures/fn_books_select_all.sql");
        ExecuteSqlScript(context, "Database/StoredProcedures/fn_books_select_by_id.sql");
        ExecuteSqlScript(context, "Database/StoredProcedures/fn_books_search.sql");
        ExecuteSqlScript(context, "Database/StoredProcedures/fn_books_insert.sql");
        ExecuteSqlScript(context, "Database/StoredProcedures/fn_books_update.sql");
        ExecuteSqlScript(context, "Database/StoredProcedures/fn_books_delete.sql");

        SeedData(context);
    }

    private static void ExecuteSqlScript(AppDbContext context, string relativePath)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var fullPath = Path.Combine(basePath, relativePath);

        if (!File.Exists(fullPath))
        {
            return;
        }

        var sql = File.ReadAllText(fullPath);
        context.Database.ExecuteSqlRaw(sql);
    }

    private static void SeedData(AppDbContext context)
    {
        if (context.Books.Any())
        {
            return;
        }

        var books = new[]
        {
            new Book("Чистый код", "Роберт Мартин", 2008, "Питер", "978-5-4461-0690-9",
                "Современная методология разработки ПО",
                "<tableOfContents>" +
                "<chapter number=\"1\" title=\"Значимость чистого кода\" />" +
                "<chapter number=\"2\" title=\"Осмысленные имена\" />" +
                "<chapter number=\"3\" title=\"Функции\" />" +
                "<chapter number=\"4\" title=\"Комментарии\" />" +
                "<chapter number=\"5\" title=\"Форматирование\" />" +
                "</tableOfContents>"),
            new Book("Паттерны проектирования", "Гамма Э., Хелм Р., Джонсон Р.", 1994, "Питер", "978-5-4461-0484-9",
                "Элементы повторяемого объектно-ориентированного ПО",
                "<tableOfContents>" +
                "<chapter number=\"1\" title=\"Введение\" />" +
                "<chapter number=\"2\" title=\"Экземпляр-одиночка\" />" +
                "<chapter number=\"3\" title=\"Наблюдатель\" />" +
                "<chapter number=\"4\" title=\"Фабричный метод\" />" +
                "</tableOfContents>"),
            new Book("CLR via C#", "Джеффри Рихтер", 2013, "Питер", "978-5-4461-0541-9",
                "Программирование на платформе .NET Framework 4.5",
                "<tableOfContents>" +
                "<chapter number=\"1\" title=\"Архитектура платформы CLR\" />" +
                "<chapter number=\"2\" title=\"Сборки\" />" +
                "<chapter number=\"3\" title=\"Типы\" />" +
                "</tableOfContents>")
        };

        context.Books.AddRange(books);
        context.SaveChanges();
    }
}