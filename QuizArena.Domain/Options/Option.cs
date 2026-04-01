using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Questions;

namespace QuizArena.Domain.Options;

public sealed class Option : BaseEntity
{
    public string Text { get; set; } = default!;
    public bool IsCorrect { get; set; }
    
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = default!;
}