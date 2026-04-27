using Microsoft.EntityFrameworkCore;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Domain.Questions;
using QuizArena.Domain.RoomParticipants;
using QuizArena.Domain.Rooms;
using QuizArena.Domain.UserAnswers;
using QuizArena.Domain.Users;

namespace QuizArena.Persistance.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    
        var userId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
        var roomId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901");
        var questionId = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-123456789012");
        var optionId1 = Guid.Parse("d4e5f6a7-b8c9-0123-defa-234567890123");
        var optionId2 = Guid.Parse("e5f6a7b8-c9d0-1234-efab-345678901234");

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = userId,
            Username = "testuser",
            Email = "test@test.com",
            PasswordHash = "hashed",
            CreatedAt = DateTime.UtcNow
        });

        modelBuilder.Entity<Room>().HasData(new Room
        {
            Id = roomId,
            Name = "Test Room",
            Description = "Test Description",
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        });

        modelBuilder.Entity<Question>().HasData(new Question
        {
            Id = questionId,
            Text = "Test Question",
            RoomId = roomId,
            CreatedAt = DateTime.UtcNow
        });

        modelBuilder.Entity<QuestionOption>().HasData(
            new QuestionOption { Id = optionId1, Text = "Option A", IsCorrect = true, QuestionId = questionId, CreatedAt = DateTime.UtcNow },
            new QuestionOption { Id = optionId2, Text = "Option B", IsCorrect = false, QuestionId = questionId, CreatedAt = DateTime.UtcNow }
        );
    }
}