namespace NetDevTools.Core.Exceptions
{
    public class CustomHttpRedirectException : Exception
    {
        public string RedirectUrl { get; private set; }
        public CustomHttpRedirectException() { }

        public CustomHttpRedirectException(string redirectUrl, string message)
            : base(message)
        {
            RedirectUrl = redirectUrl;
        }

    }
}
