using System;

namespace BoardGames.Gomoku.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }
        public ErrorResponse(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            Error = errorMessage;
        }

        public ErrorResponse(Exception ex, int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            Error = errorMessage;
        }

        public int ErrorCode { get; set; }
        public string Error { get; set; }
    }
}
