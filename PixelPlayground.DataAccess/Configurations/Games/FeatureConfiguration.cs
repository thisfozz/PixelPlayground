using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Games;

public class FeatureConfiguration : IEntityTypeConfiguration<FeatureEntity>
{
    public void Configure(EntityTypeBuilder<FeatureEntity> builder)
    {
        builder.HasKey(e => e.FeatureId).HasName("features_pkey");

        builder.ToTable("features");

        builder.HasIndex(e => e.Name, "features_name_key").IsUnique();

        builder.Property(e => e.FeatureId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("feature_id");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
    }
}