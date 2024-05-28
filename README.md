# Oz.RateLimitedHttpClient <a href="https://www.nuget.org/packages/Oz.RateLimitedHttpClient"><img alt="NuGet" src="https://badgen.net/badge/Oz.RateLimitedHttpClient/v1.0.0/blue?icon=nuget"/></a>
RateLimitHttpClient is a HttpClient wrapper implementing [SimpleRateLimiter](https://www.github.com/XeClutch/Oz.SimpleRateLimiter).

## Usage
#### Import the namespace
```csharp
using Oz.RateLimiting;
```
#### Establish a rate limit
Let's get setup for 200 requests per hour.
```csharp
var rateLimiter = new SimpleRateLimiter(200, TimeSpan.FromHours(1));
var httpClient = new RateLimitedHttpClient(rateLimiter);
```
#### Abiding by the rate limit
Let's make some API calls to [jsonplaceholder](https://jsonplaceholder.typicode.com/) (a free, fake API for testing).
```csharp
for (int i = 1; i <= 5000; i++) {
    var response = await httpClient.GetAsync($"https://jsonplaceholder.typicode.com/photos/{i}");
}
```
