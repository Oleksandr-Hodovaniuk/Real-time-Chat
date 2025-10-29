using Microsoft.Extensions.DependencyInjection;
using RealTimeChat.Infrastructure.Services;

namespace RealTimeChat.Infrastructure.Extensions;

public static class TextAnalyticsExtension
{
    public static IServiceCollection TextAnalyticsConfig(this IServiceCollection services)
    {
        var textAnalyticsConfig = new TextAnalyticsConnection
        {
            Endpoint = Environment.GetEnvironmentVariable("AZURE_TEXT_ANALYTICS_ENDPOINT")!,
            ApiKey = Environment.GetEnvironmentVariable("AZURE_TEXT_ANALYTICS_API_KEY")!,
        };

        services.AddSingleton(textAnalyticsConfig);

        return services;
    }
}
