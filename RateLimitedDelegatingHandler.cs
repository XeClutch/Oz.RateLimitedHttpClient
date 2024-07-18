using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Oz.RateLimiting {
    public class RateLimitedDelegatingHandler : DelegatingHandler {
        private SimpleRateLimiter _rateLimiter;

        public RateLimitedDelegatingHandler(SimpleRateLimiter rateLimiter, HttpMessageHandler httpMessageHandler) :
            base(httpMessageHandler) {
            _rateLimiter = rateLimiter;
        }
        
        // Overrides
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     CancellationToken cancellationToken) {
            if (await _rateLimiter.WaitForAvailabilityAsync(cancellationToken)) {
                return await base.SendAsync(request, cancellationToken);
            }

            throw new TaskCanceledException();
        }
    }
}