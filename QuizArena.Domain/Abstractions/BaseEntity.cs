namespace QuizArena.Domain.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BaseEntity()
    {
        Id = Guid.CreateVersion7();
    }
}