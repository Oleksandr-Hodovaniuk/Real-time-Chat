using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Infrastructure.Extensions;
using RealTimeChat.Infrastructure.Persistence;
using RealTimeChat.Infrastructure.Repositories;

namespace RealTimeChat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServerConnection(configuration);

        services.AddScoped<IApplicationDbContext>(provider =>
           provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IApplicationDbContextInitialiser, ApplicationDbContextInitialiser>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
