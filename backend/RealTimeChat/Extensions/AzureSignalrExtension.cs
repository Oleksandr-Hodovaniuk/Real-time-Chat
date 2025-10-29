namespace RealTimeChat.Extensions;

public static class AzureSignalrExtension
{
    public static IServiceCollection AddAzureSignalRService(this IServiceCollection services)
    {
        var signalrUrl = Environment.GetEnvironmentVariable("AZURE_SIGNALR_CONNECTION_STRING")!;

        services.AddSignalR().AddAzureSignalR( signalrUrl);

        return services;
    }
}
