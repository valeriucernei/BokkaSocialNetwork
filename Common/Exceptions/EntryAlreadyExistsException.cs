using System.Net;

namespace Common.Exceptions;

public class EntryAlreadyExistsException : ApiException
{
    public override HttpStatusCode Code => HttpStatusCode.BadRequest;
    
    public EntryAlreadyExistsException(string message) : base(message) { }
}