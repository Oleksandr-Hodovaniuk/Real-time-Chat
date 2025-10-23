namespace RealTimeChat.Application.Interfaces;

public interface IApplicationDbContextInitialiser
{
    Task InitialiseAsync(CancellationToken cancellationToken = default);
    Task SeedAsync(CancellationToken cancellationToken = default);
}
