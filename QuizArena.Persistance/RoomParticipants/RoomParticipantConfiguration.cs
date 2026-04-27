using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizArena.Domain.RoomParticipants;
using QuizArena.Persistance.Constans;

namespace QuizArena.Persistance.RoomParticipants;

public sealed class RoomParticipantConfiguration : IEntityTypeConfiguration<RoomParticipant>
{
    public void Configure(EntityTypeBuilder<RoomParticipant> builder)
    {
        builder.ToTable(TableNames.RoomParticipants);
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.JoinedAt).IsRequired();
        
        builder.HasOne(x => x.Room)
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Participations)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}