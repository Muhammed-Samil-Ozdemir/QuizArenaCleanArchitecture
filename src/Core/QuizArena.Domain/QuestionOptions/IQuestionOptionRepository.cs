using QuizArena.Domain.Abstractions;

namespace QuizArena.Domain.QuestionOptions;

public interface IQuestionOptionRepository : IGenericRepository<QuestionOption>
{
    Task<List<QuestionOption>> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);
}