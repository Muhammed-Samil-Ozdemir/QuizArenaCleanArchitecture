using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Persistance.Constans;

namespace QuizArena.Persistance.QuestionOptions;

public sealed class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.ToTable(TableNames.QuestionOptions);
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.IsCorrect)
            .IsRequired();
        
        builder.HasOne(x => x.Question)
            .WithMany(x => x.QuestionOptions)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}