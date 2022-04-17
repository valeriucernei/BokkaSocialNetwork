using System.Net;

namespace Common.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
    {
    }
}