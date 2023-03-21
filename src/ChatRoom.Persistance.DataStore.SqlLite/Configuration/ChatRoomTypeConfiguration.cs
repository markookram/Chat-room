using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.DataStore.SqlLite.Configuration;

public class ChatRoomTypeConfiguration : IEntityTypeConfiguration<Domain.Model.ChatRoom>
{
    public void Configure(EntityTypeBuilder<Domain.Model.ChatRoom> configuration)
    {
        configuration.ToTable("CHATROOM", "dbo");

        configuration.ConfigureBaseEntity();

        configuration.Property(cr => cr.Name)
            .HasColumnName("NAME")
            .HasMaxLength(50)
            .IsRequired();

        configuration.Navigation(n => n.Participants)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);

    }
}