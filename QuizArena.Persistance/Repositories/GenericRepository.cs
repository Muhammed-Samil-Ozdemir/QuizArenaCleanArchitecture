using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QuizArena.Domain.Repositories;
using QuizArena.Persistance.Context;

namespace QuizArena.Persistance.Repositories;

public class GenericRepository<T>(AppDbContext dbContext) : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    public async Task AddAsync(T entity, CancellationToken cancellationToken) =>
        await _dbSet.AddAsync(entity, cancellationToken);

    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken) =>
        _dbSet.AddRangeAsync(entities, cancellationToken);

    public void Update(T entity) => _dbSet.Update(entity);

    public void UpdateRange(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);
        if (entity is not null)
            _dbSet.Remove(entity);
    }

    public void Remove(T entity) => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

    public IQueryable<T> GetAll() => _dbSet.AsQueryable();

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken) => _dbSet.ToListAsync(cancellationToken);

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression) => _dbSet.Where(expression);

    public ValueTask<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _dbSet.FindAsync(id, cancellationToken);

    public Task<T?> GetFirstByExpressionAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken) => _dbSet.FirstOrDefaultAsync(expression, cancellationToken);

    public Task<T?> GetFirst(CancellationToken cancellationToken) => _dbSet.FirstOrDefaultAsync(cancellationToken);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) => 
        _dbSet.AnyAsync(expression, cancellationToken);
}