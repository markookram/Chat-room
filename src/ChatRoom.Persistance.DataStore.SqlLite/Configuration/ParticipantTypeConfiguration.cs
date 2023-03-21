using ChatRoom.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.DataStore.SqlLite.Configuration;

public class ParticipantTypeConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> configuration)
    {
        configuration.ToTable("PARTICIPANT", "dbo");

        configuration.ConfigureBaseEntity();

        configuration.Property(cr => cr.Name)
            .HasColumnName("NAME")
            .HasMaxLength(50)
            .IsRequired();

        configuration
            .Property(p => p.ChatRoomId)
            .HasColumnName("REF_ID_CHATROOM")
            .IsRequired(false);

        configuration.HasOne(kp => kp.ChatRoom)
            .WithMany(n => n.Participants)
            .HasForeignKey(kp => kp.ChatRoomId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}