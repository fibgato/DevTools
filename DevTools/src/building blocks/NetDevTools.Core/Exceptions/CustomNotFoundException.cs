namespace NetDevTools.Core.Exceptions
{
    public class CustomNotFoundException : Exception
    {
        public CustomNotFoundException() { }

        public CustomNotFoundException(string message)
            : base(message) { }

    }

}
