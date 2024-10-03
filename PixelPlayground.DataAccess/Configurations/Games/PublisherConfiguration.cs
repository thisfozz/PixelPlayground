using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Games;

public class PublisherConfiguration : IEntityTypeConfiguration<PublisherEntity>
{
    public void Configure(EntityTypeBuilder<PublisherEntity> builder)
    {
        builder.HasKey(e => e.PublisherId).HasName("publishers_pkey");

        builder.ToTable("publishers");

        builder.HasIndex(e => e.Name, "publishers_name_key").IsUnique();

        builder.Property(e => e.PublisherId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("publisher_id");
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
    }
}