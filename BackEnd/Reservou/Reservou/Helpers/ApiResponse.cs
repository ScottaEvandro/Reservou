using System.Net;

namespace Reservou.Helpers;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}
