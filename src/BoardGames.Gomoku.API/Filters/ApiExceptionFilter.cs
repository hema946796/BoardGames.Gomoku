using BoardGames.Gomoku.Common;
using BoardGames.Gomoku.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BoardGames.Gomoku.API.Filters
{
    public class ApiExceptionFilter : IActionFilter
    {
        private readonly ApiSettings _apiSettings;
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(
            ILogger<ApiExceptionFilter> logger,
            ApiSettings apiSettings
        )
        {
            _apiSettings = apiSettings;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is WebApiException apiException)
            {
                string errorMsg = null;

                var isProd = _apiSettings.Environment.ToLower() == "prod";

                if (isProd)
                {
                    errorMsg = "Sorry, an error occurred. If it continues please contact support with the error code";
                }
                else
                {
                    errorMsg = apiException.Message;
                }

                _logger.LogError(apiException,
                    $"A handled api exception occurred - [#{apiException.ErrorCode}] {apiException.Message}");

                var response = new ErrorResponse
                {
                    Error = errorMsg,
                    ErrorCode = apiException.ErrorCode
                };
                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)apiException.HttpStatusCode
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
