using Microsoft.EntityFrameworkCore.Storage;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Interfaces;

public interface IUnitOfWork
{
    IRepository<User> Users { get; }
    IRepository<Message> Messages { get; }

    Task SaveAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
