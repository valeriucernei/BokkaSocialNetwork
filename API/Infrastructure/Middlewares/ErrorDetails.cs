using Newtonsoft.Json;

namespace API.Infrastructure.Middlewares;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = String.Empty;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}