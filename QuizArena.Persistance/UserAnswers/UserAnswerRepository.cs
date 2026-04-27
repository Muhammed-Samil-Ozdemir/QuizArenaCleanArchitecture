using Microsoft.EntityFrameworkCore;
using QuizArena.Domain.UserAnswers;
using QuizArena.Persistance.Context;
using QuizArena.Persistance.Repositories;

namespace QuizArena.Persistance.UserAnswers;

public sealed class UserAnswerRepository(AppDbContext dbContext)
    : GenericRepository<UserAnswer>(dbContext), IUserAnswerRepository
{
    public Task<List<UserAnswer>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken) =>
        GetWhere(a => a.UserId == userId).ToListAsync(cancellationToken);

    public Task<List<UserAnswer>> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken) =>
        GetWhere(a => a.QuestionId == questionId).ToListAsync(cancellationToken);

    public Task<UserAnswer?> GetByUserAndQuestionAsync(Guid userId, Guid questionId,
        CancellationToken cancellationToken) => 
        GetWhere(a => a.UserId == userId && a.QuestionId == questionId)
            .FirstOrDefaultAsync(cancellationToken);
}