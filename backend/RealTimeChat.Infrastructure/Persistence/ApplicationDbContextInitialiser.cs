using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Enums;

namespace RealTimeChat.Infrastructure.Persistence;

internal class ApplicationDbContextInitialiser : IApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    public ApplicationDbContextInitialiser(ApplicationDbContext context, ILogger<ApplicationDbContextInitialiser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync(CancellationToken cancellationToken)
    {
        if (_context.Database.IsSqlServer())
        {
            _logger.LogInformation("Applying database migrations...");

            await _context.Database.MigrateAsync(cancellationToken);

            _logger.LogInformation("Database migration completed.");
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Users.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Skipping database seeding. Users already exist.");
            return;
        }

        _logger.LogInformation("Seeding database...");

        var user1 = new User
        {
            Id = Guid.NewGuid(),
            Username = "Oleksander",
            PasswordHash = new PasswordHasher<User>().HashPassword(null!, "Password1"),
            Messages = new List<Message>
            {
                new Message
                {
                    Id = Guid.NewGuid(),
                    Text = "Hello, how are you?",
                    Created = DateTime.UtcNow,
                    SentimentType = SentimentTypeEnum.Positive
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                    Text = "Did you finish the project?",
                    Created = DateTime.UtcNow.AddMinutes(2),
                    SentimentType = SentimentTypeEnum.Neutral
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                    Text = "Ok finish your task and let me know.",
                    Created = DateTime.UtcNow.AddMinutes(4),
                    SentimentType = SentimentTypeEnum.Mixed
                }
            }
        };

        var user2 = new User
        {
            Id = Guid.NewGuid(),
            Username = "John",
            PasswordHash = new PasswordHasher<User>().HashPassword(null!, "Password2"),
            Messages = new List<Message>
            {
                new Message
                {
                    Id = Guid.NewGuid(),
                    Text = "I'm fine, thank you!",
                    Created = DateTime.UtcNow.AddMinutes(1)
                },
                new Message
                {
                    Id = Guid.NewGuid(),
                    Text = "No, i have some serious problems!",
                    Created = DateTime.UtcNow.AddMinutes(3),
                    SentimentType = SentimentTypeEnum.Negative
                }
            }
        };

        await _context.Users.AddRangeAsync(user1, user2);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Database seeding completed.");
    }
}
