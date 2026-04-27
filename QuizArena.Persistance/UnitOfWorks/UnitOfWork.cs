using QuizArena.Domain.UnitOfWorks;
using QuizArena.Persistance.Context;

namespace QuizArena.Persistance.UnitOfWorks;

public sealed class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken); 
}