using System.Linq.Expressions;

namespace QuizArena.Domain.Repositories;

public interface IGenericRepository<T>
{
    Task AddAsync(T entity,  CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entities,  CancellationToken cancellationToken);

    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);

    Task RemoveByIdAsync(Guid id,  CancellationToken cancellationToken);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    IQueryable<T> GetAll();
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);

    ValueTask<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<T?> GetFirstByExpressionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    Task<T?> GetFirst(CancellationToken cancellationToken);

    Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
}