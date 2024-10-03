using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Games;

public class DeveloperConfiguration : IEntityTypeConfiguration<DeveloperEntity>
{
    public void Configure(EntityTypeBuilder<DeveloperEntity> builder)
    {
        builder.HasKey(e => e.DeveloperId).HasName("developers_pkey");

        builder.ToTable("developers");

        builder.HasIndex(e => e.Name, "developers_name_key").IsUnique();

        builder.Property(e => e.DeveloperId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("developer_id");
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
    }
}