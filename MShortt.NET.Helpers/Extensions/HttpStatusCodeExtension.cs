using System.Net;

namespace MShortt.NET.Helpers.Extensions
{
    public static class HttpStatusCodeExtension
    {
        /// <summary>Indicates whether the associated request is retryable.</summary>
        public static bool IsRetryable(this HttpStatusCode statusCode)
        {
            //Casts to int are required for enum values not available in .NET Standard.
            return statusCode == HttpStatusCode.GatewayTimeout
                || statusCode == HttpStatusCode.BadGateway
                || statusCode == HttpStatusCode.RequestTimeout
                || statusCode == HttpStatusCode.ServiceUnavailable
                || (int)statusCode == 429;
        }
    }
}
