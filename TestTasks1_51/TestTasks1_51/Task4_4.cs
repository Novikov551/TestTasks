namespace TestTasks1_51
{
    internal class Task4_4
    {
        public void Do()
        {
            /*
             4.4	ЧТО ТАКОЕ MIDDLEWARE? ДЛЯ ЧЕГО ОНИ НУЖНЫ? ПРИВЕДИТЕ ПРИМЕР.

                Middleware это компонент в конвейере обработки HTTP запроса. Каждый Middleware по очереди получает запрос, выполняет какую либо работу и передает запрос 
            дальше по цепочке. После того как следующий компонент отработал, управление возвращается в предыдущий компонет, который может как-либо обработать ответ.

                Middleware позволяет разбить обработку запроса на несколько этапов, каждый из которых будет отвечать за свою конкретную задачу, а не обрабатывать все сразу
            в одном месте. 

                Так же стоит упомянуть что регистрировать Middleware нужно в правильном порядке, потому что они выполняются последовательно, и всегда нужно 
            вызывать next(), иначе pipeline оборвется, и следующие компоненты не выполнятся.

            Например у нас в приложении по ходу обработки запроса может возникнуть исключение, и вот логику того как его обработать и какой ответ вернуть 
            мы можем вынести в Middleware.

            Пример:
                
            public class CustomExceptionHandler
            {
                private readonly RequestDelegate _next;

                public CustomExceptionHandler(RequestDelegate next)
                {
                    _next = next;
                }

                public async Task InvokeAsync(HttpContext context)
                {
                    try
                    {
                        await _next(context);
                    }
                    catch(Exception ex)
                    {
                       //Кастомная логика обработки

                        context.Response.StatusCode = 500;//Или любой другой нужный код ответа
                        await context.Response.WriteAsJsonAsync(new { error= "кастомная ошибка" } );
                    }
                }
            }

            и для регистрации вызываем app.UseMiddleware<CustomExceptionHandler>();
             */
        }
    }
}
