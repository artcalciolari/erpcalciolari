namespace ErpCalciolari.Middlewares
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
                await HandleExceptionAsync(context, ex);
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            object response;

            // Change each exception type to a specific status code
            switch (exception)
            {
                case KeyNotFoundException ex:
                    statusCode = StatusCodes.Status404NotFound;
                    response = new
                    {
                        error = new
                        {
                            code = statusCode,
                            message = ex.Message
                        }
                    };
                    break;

                case FluentValidation.ValidationException ex:
                    statusCode = StatusCodes.Status400BadRequest;
                    response = new
                    {
                        error = new
                        {
                            code = statusCode,
                            message = "validation error",
                            details = ex.Errors.Select(e => new
                            {
                                property = e.PropertyName,
                                message = e.ErrorMessage
                            })
                        }
                    };
                    break;

                case InvalidOperationException ex:
                    statusCode = StatusCodes.Status400BadRequest;
                    response = new
                    {
                        error = new
                        {
                            code = statusCode,
                            message = ex.Message
                        }
                    };
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    response = new
                    {
                        error = new
                        {
                            code = statusCode,
                            message = exception.Message
                        }
                    };
                    break;
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
