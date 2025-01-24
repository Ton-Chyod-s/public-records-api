using PublicRecords.Domain.Errors.Common;

namespace PublicRecords.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleUnexpectedException(httpContext, ex);
            }
        }

        private static async Task HandleUnexpectedException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(new UnexpectedError("Deu ruim ae!"));
        }
    }
}
