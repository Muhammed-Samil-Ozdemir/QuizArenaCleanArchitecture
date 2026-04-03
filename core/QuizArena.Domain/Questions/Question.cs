using QuizArena.Domain.Abstractions;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Domain.Rooms;
using QuizArena.Domain.UserAnswers;

namespace QuizArena.Domain.Questions;

public sealed class Question : BaseEntity
{
    public string Text { get; set; } =  string.Empty;
    
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = default!;
    
    public ICollection<QuestionOption> QuestionOptions { get; set; } = [];
    public ICollection<UserAnswer> UserAnswers { get; set; } = [];
}