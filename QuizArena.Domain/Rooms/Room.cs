using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Questions;
using QuizArena.Domain.RoomParticipants;
using QuizArena.Domain.Users;

namespace QuizArena.Domain.Rooms;

public sealed class Room : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = default!;
    
    public ICollection<RoomParticipant> Participants { get; set; } = [];
    public ICollection<Question> Questions { get; set; } = [];
}