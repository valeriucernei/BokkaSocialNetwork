using System.Net;

namespace Common.Exceptions;

public class BaseException : Exception
{
    public HttpStatusCode Code { get; }
    
    public BaseException(HttpStatusCode code, string message) : base(message)
    {
        Code = code;
    }
}