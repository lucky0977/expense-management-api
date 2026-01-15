//using System.Net;
//using System.Text.Json;
//using ExpenseManagement.Application.Exceptions;

//namespace ExpenseManagement.API.Middlewares
//{
//    public class ExceptionMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<ExceptionMiddleware> _logger;

//        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An unhandled exception occurred");
//                await HandleExceptionAsync(context, ex);
//            }
//        }

//        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
//        {
//            context.Response.ContentType = "application/json";

//            int statusCode = exception switch
//            {
//                ValidationException => (int)HttpStatusCode.BadRequest,
//                NotFoundException => (int)HttpStatusCode.NotFound,
//                ArgumentException => (int)HttpStatusCode.BadRequest,
//                _ => (int)HttpStatusCode.InternalServerError
//            };

//            context.Response.StatusCode = statusCode;

//            object response = exception switch
//            {
//                ValidationException validationEx => new
//                {
//                    statusCode,
//                    message = "Validation failed",
//                    errors = validationEx.Errors.SelectMany(e =>
//                        e.Value.Select(msg => new
//                        {
//                            field = e.Key,
//                            error = msg
//                        }))
//                },

//                _ => new
//                {
//                    statusCode,
//                    message = exception.Message
//                }
//            };

//            return context.Response.WriteAsync(
//                JsonSerializer.Serialize(response)
//            );
//        }
//    }
//}


using System.Net;

namespace ExpenseManagement.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new
                {
                    statusCode = 500,
                    message = ex.Message,
                    innerException = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}

