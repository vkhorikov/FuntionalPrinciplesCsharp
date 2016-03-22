using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CustomerManagement.Api.Models;
using Newtonsoft.Json;

namespace CustomerManagement.Api.Utils
{
    public class GenericTextExceptionHandler : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            context.Result = new ErrorResult(context.ExceptionContext.Request, context.Exception);
            return Task.FromResult(true);
        }


        private class ErrorResult : IHttpActionResult
        {
            private readonly HttpRequestMessage _request;
            private readonly Exception _exception;

            public ErrorResult(HttpRequestMessage request, Exception exception)
            {
                _exception = exception;
                _request = request;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                var container = Envelope.Error(_exception.Message);
                response.Content = new StringContent(JsonConvert.SerializeObject(container, Formatting.Indented));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.RequestMessage = _request;
                return Task.FromResult(response);
            }
        }
    }
}
