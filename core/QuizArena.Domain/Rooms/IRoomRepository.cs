using QuizArena.Domain.Abstractions;

namespace QuizArena.Domain.Rooms;

public interface IRoomRepository : IGenericRepository<Room>
{
    Task<IReadOnlyList<Room>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken);
    Task<Room?> GetRoomWithQuestionsAsync(Guid id, CancellationToken cancellationToken);
}