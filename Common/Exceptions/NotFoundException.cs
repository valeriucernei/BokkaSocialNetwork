using System.Net;

namespace Common.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
    {
    }
}