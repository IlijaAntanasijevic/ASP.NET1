using Application;
using Implementation.Exceptions;
using System;

namespace API.Core.Exceptions
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IExceptionLogger _logger;
        private IApplicationActor _actor;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IExceptionLogger logger, IApplicationActor actor)
        {
            _next = next;
            _logger = logger;
            _actor = actor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                if(ex is UnauthorizedAccessException)
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }

                if (ex is EntityNotFoundException)
                {
                    httpContext.Response.StatusCode = 404;
                    return;
                }

                var errorId = _logger.Log(ex, _actor);
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new { Message = $"An unexpected error has occured. Please contact our support with this ID - {errorId}." });

            }
        }
    }
}
