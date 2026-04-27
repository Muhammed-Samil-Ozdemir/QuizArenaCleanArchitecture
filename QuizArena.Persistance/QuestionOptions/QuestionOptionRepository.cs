using Microsoft.EntityFrameworkCore;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Persistance.Context;
using QuizArena.Persistance.Repositories;

namespace QuizArena.Persistance.QuestionOptions;

public sealed class QuestionOptionRepository(AppDbContext dbContext)
    : GenericRepository<QuestionOption>(dbContext), IQuestionOptionRepository
{
    public Task<List<QuestionOption>> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken) =>
        GetWhere(o => o.QuestionId == questionId).ToListAsync(cancellationToken);
}