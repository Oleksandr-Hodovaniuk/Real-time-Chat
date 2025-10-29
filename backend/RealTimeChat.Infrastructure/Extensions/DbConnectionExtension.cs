using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChat.Infrastructure.Persistence;

namespace RealTimeChat.Infrastructure.Extensions;

public static class DbConnectionExtension
{
    public static IServiceCollection AddSqlServerConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["CHAT_DB_CONNECTION_STRING"];

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
