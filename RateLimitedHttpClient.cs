namespace Oz.RateLimiting;

public class RateLimitedHttpClient(SimpleRateLimiter rateLimiter)
    : HttpClient(new RateLimitedDelegatingHandler(rateLimiter, new HttpClientHandler())) { }