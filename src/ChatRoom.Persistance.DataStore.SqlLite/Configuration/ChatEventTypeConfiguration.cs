using ChatRoom.Domain.Events;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.SqlLite.Configuration;

public class ChatEventTypeConfiguration : IEntityTypeConfiguration<ChatEvent>
{
    public void Configure(EntityTypeBuilder<ChatEvent> configuration)
    {
        configuration.ToTable("CHATEVENT", "dbo");
        configuration.ConfigureBaseEntity();

        configuration.Property(cr => cr.Type)
            .IsRequired();

        configuration.Property(cr => cr.ChatRoomId)
            .IsRequired();

        configuration.Property(cr => cr.ParticipantId)
            .IsRequired();

        configuration.Property(cr => cr.ParticipantName)
            .HasMaxLength(50)
            .IsRequired();

        configuration.Property(cr => cr.Message)
            .HasMaxLength(200)
            .IsRequired(false);

        configuration.Property(cr => cr.ToParticipantId)
            .IsRequired(false);

        configuration.Property(cr => cr.ToParticipantName)
            .HasMaxLength(50)
            .IsRequired(false);

    }
}