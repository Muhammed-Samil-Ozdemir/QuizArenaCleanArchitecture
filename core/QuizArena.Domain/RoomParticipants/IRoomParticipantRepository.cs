using QuizArena.Domain.Abstractions;

namespace QuizArena.Domain.RoomParticipants;

public interface IRoomParticipantRepository : IGenericRepository<RoomParticipant>
{
    Task<List<RoomParticipant>> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken);
    Task<RoomParticipant?> GetByRoomAndUserAsync(Guid roomId, Guid userId, CancellationToken cancellationToken);
}