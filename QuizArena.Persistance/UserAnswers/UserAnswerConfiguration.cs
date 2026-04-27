using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizArena.Domain.UserAnswers;
using QuizArena.Persistance.Constans;

namespace QuizArena.Persistance.UserAnswers;

public sealed class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.ToTable(TableNames.UserAnswers);
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.IsCorrect).IsRequired();
        builder.Property(x => x.AnsweredAt).IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Answers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Question)
            .WithMany(x => x.UserAnswers)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.SelectedQuestionOption)
            .WithMany()
            .HasForeignKey(x => x.SelectedOptionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}