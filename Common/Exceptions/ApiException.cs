using System.Net;

namespace Common.Exceptions;

[Serializable]
public class ApiException : Exception
{
    public virtual HttpStatusCode Code { get; } = HttpStatusCode.BadRequest;

    public ApiException(string message) : base(message) { }
}