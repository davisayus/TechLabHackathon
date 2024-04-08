using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.Helpers
{
    public class ErrorHandler<T>
    {
        private readonly ILogger _logger;

        public ErrorHandler(ILogger<T> logger)
        {
            _logger = logger;
        }

        public ResponseService<U> Error<U>(Exception ex, string serviceName, U content)
        {
            string innerException = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
            _logger.LogError(ex, $"Service: {typeof(T)}.{serviceName}, Message: {ex.Message} {innerException})? ");
            return new ResponseService<U>(true, $"Exception: {ex.Message} Inner Exception: {innerException}", System.Net.HttpStatusCode.InternalServerError, content);
        }

        public ResponseService<U> Warning<U>(string message, string serviceName, U content)
        {
            _logger.LogWarning($"Service: {typeof(T)}.{serviceName} Warning Messsage: {message}");
            return new ResponseService<U>(true, $"Warning: {message} ", System.Net.HttpStatusCode.NoContent, content);
        }

    }
}
