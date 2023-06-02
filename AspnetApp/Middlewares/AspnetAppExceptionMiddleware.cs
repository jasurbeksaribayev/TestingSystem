using AspnetApp.Service.Exceptions;

namespace AspnetApp.Middlewares
{
    public class AspnetAppExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<AspnetAppExceptionMiddleware> logger;

        public AspnetAppExceptionMiddleware(RequestDelegate next , ILogger<AspnetAppExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (AspnetAppException ex)
            {
                await this.HandleException(context, ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                await this.HandleException(context, 500, ex.Message);
            }
        }

        public async Task HandleException(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;

            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            }) ;
        }
    }
}
