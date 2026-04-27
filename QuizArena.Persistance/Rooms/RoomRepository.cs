using Microsoft.EntityFrameworkCore;
using QuizArena.Domain.Rooms;
using QuizArena.Persistance.Context;
using QuizArena.Persistance.Repositories;

namespace QuizArena.Persistance.Rooms;

public sealed class RoomRepository(AppDbContext dbContext) : GenericRepository<Room>(dbContext), IRoomRepository
{
    public Task<List<Room>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken) =>
        GetWhere(r => r.OwnerId == ownerId).ToListAsync(cancellationToken);

    public Task<Room?> GetRoomWithQuestionsAsync(Guid id, CancellationToken cancellationToken) =>
        GetWhere(r => r.Id == id)
            .Include(r => r.Questions)
            .FirstOrDefaultAsync(cancellationToken);
}