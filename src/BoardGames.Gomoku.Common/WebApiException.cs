using System;
using System.Net;

namespace BoardGames.Gomoku.Common
{
    public class WebApiException : Exception
    {
        public WebApiException(HttpStatusCode statusCode, int errorCode, string errorDescription) : base($"{errorCode}::{errorDescription}")
        {
            HttpStatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public WebApiException(HttpStatusCode statusCode)
        {
            HttpStatusCode = statusCode;
        }

        public HttpStatusCode HttpStatusCode { get; }
        public int ErrorCode { get; }
    }
}
