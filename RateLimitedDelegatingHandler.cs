namespace Oz.RateLimiting;

public class RateLimitedDelegatingHandler(SimpleRateLimiter rateLimiter, HttpMessageHandler httpMessageHandler)
    : DelegatingHandler(httpMessageHandler) {
    // Overrides
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken) {
        rateLimiter.WaitForAvailabilityAsync(cancellationToken)
                    .Wait(cancellationToken);
        return base.Send(request, cancellationToken);
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken) {
        if (await rateLimiter.WaitForAvailabilityAsync(cancellationToken)) {
            return await base.SendAsync(request, cancellationToken);
        }

        throw new TaskCanceledException();
    }
}