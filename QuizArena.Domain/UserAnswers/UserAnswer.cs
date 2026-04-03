using QuizArena.Domain.Abstractions;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Domain.Questions;
using QuizArena.Domain.Users;

namespace QuizArena.Domain.UserAnswers;

public sealed class UserAnswer : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = default!;
    
    public Guid SelectedOptionId { get; set; }
    public QuestionOption SelectedQuestionOption { get; set; } = default!;
    
    public bool IsCorrect { get; set; }
    
    public DateTime AnsweredAt { get; set; }
}