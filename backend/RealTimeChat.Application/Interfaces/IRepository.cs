using System.Linq.Expressions;

namespace RealTimeChat.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(Expression<Func<T, bool>> filter,
        string? includeProperties = null,
        CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null,
        CancellationToken cancellationToken = default);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
}
