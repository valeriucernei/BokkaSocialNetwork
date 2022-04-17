using System.Net;

namespace Common.Exceptions;

public class LoginException : ApiException
{
    public LoginException(string message) : base(HttpStatusCode.Forbidden, message)
    {
    }
}