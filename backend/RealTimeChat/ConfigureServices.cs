using RealTimeChat.Extensions;

namespace RealTimeChat;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerDocumentation();

        return services;
    }
}
