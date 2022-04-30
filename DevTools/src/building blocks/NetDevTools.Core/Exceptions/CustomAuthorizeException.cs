using System.Net;

namespace NetDevTools.Core.Exceptions
{
    public class CustomAuthorizeException : Exception
    {
        public HttpStatusCode StatusCode;

        public CustomAuthorizeException() { }

        public CustomAuthorizeException(string message, Exception innerException)
            : base(message, innerException) { }

        public CustomAuthorizeException(string message) : base(message) { }

        public CustomAuthorizeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
