using System.Net.Http;

namespace Oz.RateLimiting {
    public class RateLimitedHttpClient : HttpClient {
        private SimpleRateLimiter _rateLimiter;

        public RateLimitedHttpClient(SimpleRateLimiter rateLimiter) : base(
            new RateLimitedDelegatingHandler(rateLimiter, new HttpClientHandler())) {
            _rateLimiter = rateLimiter;
        }
    }
}