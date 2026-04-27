using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Repositories;

namespace QuizArena.Domain.UserAnswers;

public interface IUserAnswerRepository : IGenericRepository<UserAnswer>
{
    Task<List<UserAnswer>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<UserAnswer>> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);
    Task<UserAnswer?> GetByUserAndQuestionAsync(Guid userId, Guid questionId, CancellationToken cancellationToken);
}