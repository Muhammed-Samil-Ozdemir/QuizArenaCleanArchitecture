using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Repositories;

namespace QuizArena.Domain.QuestionOptions;

public interface IQuestionOptionRepository : IGenericRepository<QuestionOption>
{
    Task<List<QuestionOption>> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);
}