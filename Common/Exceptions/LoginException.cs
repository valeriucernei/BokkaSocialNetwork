using System.Net;

namespace Common.Exceptions;

public class LoginException : ApiException
{
    public override HttpStatusCode Code => HttpStatusCode.Forbidden;
    
    public LoginException(string message) : base(message) { }
}