using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestTask5_2.Domain;

namespace TestTask5_2.Infrastructure.Database.Configurations;

internal abstract class BaseEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity>
    where TBaseEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TBaseEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasComment("ИД")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CreatedDate)
            .HasColumnName("created_date")
            .HasComment("Дата создания записи")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(e => e.ModifiedDate)
            .HasColumnName("modified_date")
            .HasComment("Дата обновления записи")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Ignore(e => e.State);
    }
}