using Microsoft.EntityFrameworkCore;
using QuizArena.Domain.Questions;
using QuizArena.Persistance.Context;
using QuizArena.Persistance.Repositories;

namespace QuizArena.Persistance.Questions;

public sealed class QuestionRepository(AppDbContext dbContext) :
    GenericRepository<Question>(dbContext), IQuestionRepository
{
    public Task<List<Question>> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken) =>
        GetWhere(q => q.RoomId == roomId).ToListAsync(cancellationToken);

    public Task<Question?> GetWithOptionsAsync(Guid id, CancellationToken cancellationToken) =>
        GetWhere(q => q.Id == id)
            .Include(q => q.QuestionOptions)
            .FirstOrDefaultAsync(cancellationToken);
}