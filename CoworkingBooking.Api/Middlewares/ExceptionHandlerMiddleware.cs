using CoworkingBooking.Service.Exceptions;

namespace CoworkingBooking.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CBException exception)
            {
                await ClientErrorHandleAsync(httpContext, exception);
            }
            catch (Exception exception)
            {
                await SystemErrorHandleAsync(httpContext, exception);
            }
        }

        public async Task ClientErrorHandleAsync(HttpContext httpContext, CBException exception)
        {
            httpContext.Response.ContentType = "application/json";
           
            httpContext.Response.StatusCode = exception.Code;
            await httpContext.Response.WriteAsync(exception.Message);
        }

        public async Task SystemErrorHandleAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsync(exception.Message);
        }
    }
}
