using System.Net;
using Xunit;

namespace CustomerManagement.Tests.Utils
{
    public static class ResponseExtensions
    {
        public static void ShouldBeOk(this Response response)
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public static void ShouldBeError(this Response response, string errorMessage)
        {
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
