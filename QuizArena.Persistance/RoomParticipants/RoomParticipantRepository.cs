using Microsoft.EntityFrameworkCore;
using QuizArena.Domain.RoomParticipants;
using QuizArena.Persistance.Context;
using QuizArena.Persistance.Repositories;

namespace QuizArena.Persistance.RoomParticipants;

public sealed class RoomParticipantRepository(AppDbContext dbContext) :
    GenericRepository<RoomParticipant>(dbContext), IRoomParticipantRepository
{
    public Task<List<RoomParticipant>> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken) =>
        GetWhere(p => p.RoomId == roomId).ToListAsync(cancellationToken);

    public Task<RoomParticipant?> GetByRoomAndUserAsync(Guid roomId, Guid userId,
        CancellationToken cancellationToken) =>
        GetWhere(p => p.RoomId == roomId && p.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
}