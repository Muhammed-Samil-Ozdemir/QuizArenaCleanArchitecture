using QuizArena.Domain.Abstractions;

namespace QuizArena.Domain.Options;

public interface IOptionRepository : IGenericRepository<Option>
{
    Task<List<Option>> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);
}