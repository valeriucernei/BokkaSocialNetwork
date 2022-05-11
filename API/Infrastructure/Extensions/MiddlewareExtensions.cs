using API.Infrastructure.Middlewares;

namespace API.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder app)
    {
        return app.UseMiddleware<DbTransactionMiddleware>();
    }
}