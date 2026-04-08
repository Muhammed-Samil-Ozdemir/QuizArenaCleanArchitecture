using QuizArena.Domain.Abstractions;

namespace QuizArena.Domain.Questions;

public interface IQuestionRepository : IGenericRepository<Question>
{
    Task<List<Question>> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken);
    Task<Question?> GetWithOptionsAsync(Guid id, CancellationToken cancellationToken);
}