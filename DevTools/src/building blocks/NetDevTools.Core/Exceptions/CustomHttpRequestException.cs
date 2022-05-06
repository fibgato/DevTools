namespace NetDevTools.Core.Exceptions
{
    public class CustomHttpRequestException : Exception
    {
        public CustomHttpRequestException() { }

        public CustomHttpRequestException(string message)
            : base(message) { }

    }
}
