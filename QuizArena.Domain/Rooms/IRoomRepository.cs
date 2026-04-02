using QuizArena.Domain.Abstractions;

namespace QuizArena.Domain.Rooms;

public interface IRoomRepository : IGenericRepository<Room>
{
    Task<IReadOnlyList<Room>> GetRoomsByOwnerAsync(Guid ownerId, CancellationToken cancellationToken);
    Task<Room?> GetRoomWithQuestionsAsync(Guid id, CancellationToken cancellationToken);
}