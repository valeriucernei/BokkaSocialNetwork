using System.Net;

namespace Common.Exceptions;

public class ApiException : Exception
{
    public HttpStatusCode Code { get; set; }

    public ApiException(HttpStatusCode code, string message) : base(message)
    {
        Code = code;
    }
}