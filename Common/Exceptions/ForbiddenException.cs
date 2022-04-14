using System.Net;

namespace Common.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message) : base(HttpStatusCode.Forbidden, message)
    {
    }
}