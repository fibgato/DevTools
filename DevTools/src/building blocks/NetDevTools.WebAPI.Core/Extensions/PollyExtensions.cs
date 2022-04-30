using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace NetDevTools.WebAPI.Core.Extensions
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromMilliseconds(500),
                    TimeSpan.FromMilliseconds(1000),
                    TimeSpan.FromMilliseconds(1500),
                    TimeSpan.FromMilliseconds(2000),
                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Tentando pela {retryCount} vez!");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            return retry;
        }
    }
}
