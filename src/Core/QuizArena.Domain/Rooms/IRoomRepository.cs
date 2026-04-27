using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Repositories;

namespace QuizArena.Domain.Rooms;

public interface IRoomRepository : IGenericRepository<Room>
{
    Task<List<Room>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken);
    Task<Room?> GetRoomWithQuestionsAsync(Guid id, CancellationToken cancellationToken);
}