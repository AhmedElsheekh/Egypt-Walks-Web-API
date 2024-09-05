using System.Net;

namespace EgyptWalks.API.MiddleWares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                //Log the exception
                var errorId = Guid.NewGuid();
                _logger.LogError(ex, $"{errorId} : {ex.Message}");

                //Return custom exception message
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    Message = "Something Went Wrong, We Are Trying To Fix"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }

    }
}
