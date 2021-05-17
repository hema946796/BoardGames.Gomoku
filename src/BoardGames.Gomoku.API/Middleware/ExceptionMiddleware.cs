using BoardGames.Gomoku.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly Common.ApiSettings _apiSettings;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next
            , ILogger<ExceptionMiddleware> logger
            , IOptions<Common.ApiSettings> apiOptions)
        {
            _logger = logger;
            _next = next;
            _apiSettings = apiOptions.Value;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"999: An Unhandled Exception Occurred: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var isProd = _apiSettings.Environment == "prod";
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                ErrorCode = context.Response.StatusCode,
                Error = isProd
                    ? "Unhandled exception occurred, if it continues please contact support"
                    : exception.Message,
            };

            _logger.LogError(exception, response.Error);

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            ;
        }
    }
}
