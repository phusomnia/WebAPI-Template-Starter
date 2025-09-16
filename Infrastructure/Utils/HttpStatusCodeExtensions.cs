using System.Net;

namespace WebAPI_Template_Starter.Infrastructure.Utils;

public static class HttpStatusCodeExtensions
{
    public static int value(this HttpStatusCode statusCode)
    {
        return (int)statusCode;
    }
}
