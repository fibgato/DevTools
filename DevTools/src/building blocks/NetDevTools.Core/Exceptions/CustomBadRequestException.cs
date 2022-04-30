namespace NetDevTools.Core.Exceptions
{
    public class CustomBadRequestException : Exception
    {
        public CustomBadRequestException() { }

        public CustomBadRequestException(string message)
            : base(message) { }

    }
}
