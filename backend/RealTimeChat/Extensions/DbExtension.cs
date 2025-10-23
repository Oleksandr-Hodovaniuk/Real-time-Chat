using RealTimeChat.Application.Interfaces;

namespace RealTimeChat.Extensions;

public static class DbExtensions
{

    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbInitialiser = scope.ServiceProvider.GetRequiredService<IApplicationDbContextInitialiser>();

        await dbInitialiser.InitialiseAsync();
        await dbInitialiser.SeedAsync();
    }
}
