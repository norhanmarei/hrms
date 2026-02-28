using HRMS.Application.Enums;

namespace HRMS.Application.Common
{
    public sealed class Error
    {
        public enError Code { get; }
        public string Message { get; }
        private Error(enError code, string message)
        {
            Code = code;
            Message = message;
        }
        public static Error NotFound(string message) =>
            new Error(enError.NotFound, message);
        public static Error Conflict(string message) =>
            new Error(enError.Conflict, message);
        public static Error BadRequest(string message) =>
            new Error(enError.BadRequest, message);
    }
}