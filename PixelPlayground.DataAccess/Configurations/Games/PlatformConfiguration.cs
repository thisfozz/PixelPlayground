using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Games;

public class PlatformConfiguration : IEntityTypeConfiguration<PlatformEntity>
{
    public void Configure(EntityTypeBuilder<PlatformEntity> builder)
    {
        builder.HasKey(e => e.PlatformId).HasName("platforms_pkey");

        builder.ToTable("platforms");

        builder.HasIndex(e => e.Name, "platforms_name_key").IsUnique();

        builder.Property(e => e.PlatformId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("platform_id");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
    }
}