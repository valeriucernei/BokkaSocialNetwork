using System.Net;

namespace Common.Exceptions;

public class RegisterFormException : BaseException
{
    public RegisterFormException(string message) : base(HttpStatusCode.Conflict, message)
    {
    }
}