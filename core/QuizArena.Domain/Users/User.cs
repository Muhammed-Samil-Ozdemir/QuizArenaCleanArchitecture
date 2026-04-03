using QuizArena.Domain.Abstractions;
using QuizArena.Domain.RoomParticipants;
using QuizArena.Domain.Rooms;
using QuizArena.Domain.UserAnswers;

namespace QuizArena.Domain.Users;

public sealed class User : BaseEntity
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash  { get; set; } = default!;
    
    public ICollection<Room> OwnedRooms { get; set; } = [];
    public ICollection<RoomParticipant> Participations { get; set; } = [];
    public ICollection<UserAnswer> Answers { get; set; } = [];
}