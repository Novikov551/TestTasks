namespace TestTasks1_51
{
    internal class Task4_8
    {
        public void Do()
        {
            /*
             4.8	КАК РЕАЛИЗОВАТЬ ГЛОБАЛЬНУЮ ОБРАБОТКУ ИСКЛЮЧЕНИЙ В ASP.NET CORE?

            1й способ - использовать встроенный Middleware:

            app.UseExceptionHandler("/error");

            app.Map("/error", (HttContext context) =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                return Results.Problem(
                    title: "Ошибка",
                    detail: exception?.Message,
                    statusCode: 500
                );
            });


            2й способ - кастомный обработчик ошибок. Можем сами реализовать свой обработчик через интерфейс IExceptionHandler

            class GlobalExceptionHandler : IExceptionHandler
            {
                public async Task<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct = default)
                {
                      //логика обработки

                    return true;
                }
            }

            и зарегистрировать его
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            app.UseExceptionHandler(); //должен вызываться самым первым
             */
        }
    }
}
