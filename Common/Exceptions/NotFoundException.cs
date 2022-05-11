using System.Net;

namespace Common.Exceptions;

public class NotFoundException : ApiException
{
    public override HttpStatusCode Code => HttpStatusCode.NotFound;
    
    public NotFoundException(string message) : base(message) { }
}