using System.Net;

namespace CustomerManagement.Tests.Utils
{
    public class Response
    {
        public string ErrorMessage { get; }
        public HttpStatusCode StatusCode { get; }

        public Response(string errorMessage, HttpStatusCode statusCode)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}
