using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizArena.Domain.Questions;
using QuizArena.Persistance.Constans;

namespace QuizArena.Persistance.Questions;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable(TableNames.Questions);
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.HasOne(x => x.Room)
            .WithMany(x => x.Questions)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}