using System.Net;

namespace WebAPI_Template_Starter.SharedKernel.utils;

public static class HttpStatusCodeExtensions
{
    public static int value(this HttpStatusCode statusCode)
    {
        return (int)statusCode;
    }
}
