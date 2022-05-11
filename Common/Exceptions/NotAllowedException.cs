using System.Net;

namespace Common.Exceptions;

public class NotAllowedException : ApiException
{
    public override HttpStatusCode Code => HttpStatusCode.Forbidden;
    
    public NotAllowedException(string message) : base(message) { }
}