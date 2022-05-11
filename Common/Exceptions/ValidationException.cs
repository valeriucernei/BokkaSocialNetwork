using System.Net;

namespace Common.Exceptions;

public class ValidationException : ApiException
{
    public override HttpStatusCode Code => HttpStatusCode.BadRequest;
    
    public ValidationException(string message) : base(message) { }
}