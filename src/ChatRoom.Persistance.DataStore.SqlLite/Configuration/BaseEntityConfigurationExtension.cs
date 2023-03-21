using ChatRoom.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatRoom.Persistence.DataStore.SqlLite.Configuration;

public static class BaseEntityConfigurationExtension
{
    public static void ConfigureBaseEntity<T>(this EntityTypeBuilder<T> configuration) where T : Entity
    {
        configuration.HasKey(ct => ct.Id);

        configuration.Property(i => i.Id)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd();


        configuration.Property(i => i.CreatedOn)
            .HasColumnName("CREATED")
            .HasColumnType("datetime2");


    }
}