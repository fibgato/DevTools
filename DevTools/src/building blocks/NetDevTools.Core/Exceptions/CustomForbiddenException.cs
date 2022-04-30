namespace NetDevTools.Core.Exceptions
{
    public class CustomForbiddenException : Exception
    {
        public CustomForbiddenException() { }

        public CustomForbiddenException(string message)
            : base(message) { }
    }
}
