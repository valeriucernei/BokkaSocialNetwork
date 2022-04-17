using System.Net;

namespace Common.Exceptions;

public class EntryAlreadyExists : ApiException
{
    public EntryAlreadyExists(string message) : base(HttpStatusCode.BadRequest, message)
    {
        
    }
}