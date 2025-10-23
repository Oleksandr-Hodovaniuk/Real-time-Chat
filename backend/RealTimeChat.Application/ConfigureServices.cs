using Microsoft.Extensions.DependencyInjection;
using RealTimeChat.Application.AutoMappers;
using RealTimeChat.Application.Users.Queries;

namespace RealTimeChat.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(GetUserByIdQuery).Assembly);
        });

        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(UserProfile).Assembly));

        return services;
    }
}
