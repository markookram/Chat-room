using ChatRoom.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChatRoom.Persistence.SqlLite.Configuration;

public class ParticipantTypeConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> configuration)
    {
        configuration.ToTable("PARTICIPANT", "dbo");

        configuration.ConfigureBaseEntity();

        configuration.Property(cr => cr.Name)
            .HasMaxLength(50)
            .IsRequired();

        configuration.HasOne(kp => kp.ChatRoom)
            .WithMany(n => n.Participants)
            .HasForeignKey(kp => kp.ChatRoomId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}