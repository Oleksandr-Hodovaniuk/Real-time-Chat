using Microsoft.EntityFrameworkCore.Storage;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _context;
    public IRepository<User> Users { get; private set; }
    public IRepository<Message> Messages { get; private set; }
    public UnitOfWork(IApplicationDbContext context,
        IRepository<User> _users,
        IRepository<Message> _messages)
    {
        _context = context;
        Users = _users;
        Messages = _messages;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.BeginTransactionAsync(cancellationToken);
    }
}
