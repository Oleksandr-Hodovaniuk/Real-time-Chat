using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Infrastructure.Persistence;
using RealTimeChat.Infrastructure.Repositories;

namespace RealTimeChat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var localhost = "localhost";
        var host = configuration["CHAT_DB_HOST"];
        var port = configuration["CHAT_DB_PORT"];
        var database = configuration["CHAT_DB_NAME"];
        var username = configuration["CHAT_DB_USER"];
        var password = configuration["CHAT_DB_PASSWORD"];

        // For local development, override the host to localhost
        var connectionString = $"Server={localhost},{port};Database={database};User Id={username};Password={password};TrustServerCertificate=True;";

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IApplicationDbContext>(provider =>
           provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IApplicationDbContextInitialiser, ApplicationDbContextInitialiser>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
