using System.Net;

namespace Common.Exceptions;

public class FormException : ApiException
{
    public FormException(string message) : base(HttpStatusCode.BadRequest, message)
    {
    }
}