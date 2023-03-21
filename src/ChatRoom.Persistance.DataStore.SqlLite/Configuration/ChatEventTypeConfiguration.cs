using ChatRoom.Domain.Events;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.DataStore.SqlLite.Configuration;

public class ChatEventTypeConfiguration : IEntityTypeConfiguration<ChatEvent>
{
    public void Configure(EntityTypeBuilder<ChatEvent> configuration)
    {
        configuration.ToTable("CHATEVENT", "dbo");
        configuration.ConfigureBaseEntity();

        configuration.Property(cr => cr.Type)
            .HasColumnName("TYPE")
            .IsRequired();

        configuration.Property(cr => cr.ChatRoomId)
            .HasColumnName("CHATROOM_ID")
            .IsRequired();

        configuration.Property(cr => cr.ParticipantId)
            .HasColumnName("PARTICIPANT_ID")
            .IsRequired();

        configuration.Property(cr => cr.ParticipantName)
            .HasColumnName("PARTICIPANT_NAME")
            .HasMaxLength(50)
            .IsRequired();

        configuration.Property(cr => cr.Message)
            .HasColumnName("MESSAGE")
            .HasMaxLength(200)
            .IsRequired(false);

        configuration.Property(cr => cr.ToParticipantId)
            .HasColumnName("TO_PARTICIPANT_ID")
            .IsRequired(false);

        configuration.Property(cr => cr.ToParticipantName)
            .HasColumnName("TO_PARTICIPANT_NAME")
            .HasMaxLength(50)
            .IsRequired(false);

    }
}