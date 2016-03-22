using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace CustomerManagement.Api.Utils
{
    public class ExceptionLogger : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            // Log the exception (context.Exception)
            return Task.FromResult(true);
        }
    }
}
