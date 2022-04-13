using System.Net;

namespace Common.Exceptions;

public class LoginException : BaseException
{
    public LoginException(string message) : base(HttpStatusCode.Forbidden, message)
    {
    }
}