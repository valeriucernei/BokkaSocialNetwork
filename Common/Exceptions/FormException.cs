using System.Net;

namespace Common.Exceptions;

public class FormException : ApiException
{
    public override HttpStatusCode Code => HttpStatusCode.BadRequest;
    
    public FormException(string message) : base(message) { }
}