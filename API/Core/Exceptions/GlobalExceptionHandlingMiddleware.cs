using Application;
using Application.Exceptions;
using FluentValidation;


namespace API.Core.Exceptions
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionLogger _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IExceptionLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {

                if (exception is ValidationException ex)
                {
                    httpContext.Response.StatusCode = 422;
                    var body = ex.Errors.Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage });

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }

                if (exception is UnauthorizedAccessException)
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }

                if (exception is EntityNotFoundException)
                {
                    httpContext.Response.StatusCode = 404;
                    return;
                }

                if (exception is ConflictException c)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    var body = new { error = c.Message };

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }
                if(exception is UnsupportedFileException u)
                {
                    httpContext.Response.StatusCode = 415;
                    var body = new { error = u.Message };

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }  
                if (exception is FileNotFoundException f)
                {
                    httpContext.Response.StatusCode = 404;

                    var body = new { error = f.Message };

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }
                if(exception is PermissionDeniedException p)
                {
                    httpContext.Response.StatusCode = 403;
                    var body = new { error = p.Message };
                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }


                var actor = httpContext.RequestServices.GetService<IApplicationActor>();

                var errorId = _logger.Log(exception, actor);
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new { Message = $"An unexpected error has occured. Please contact our support with this ID - {errorId}." });

            }
        }
    }
}
