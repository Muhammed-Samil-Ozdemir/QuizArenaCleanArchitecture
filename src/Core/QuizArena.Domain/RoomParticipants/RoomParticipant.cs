using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Rooms;
using QuizArena.Domain.Users;

namespace QuizArena.Domain.RoomParticipants;

public sealed class RoomParticipant : BaseEntity
{
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = default!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    
    public DateTime JoinedAt { get; set; }

    public RoomParticipant()
    {
        JoinedAt = DateTime.UtcNow;
    }
}