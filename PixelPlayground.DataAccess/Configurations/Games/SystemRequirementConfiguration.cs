using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Games;

public class SystemRequirementConfiguration : IEntityTypeConfiguration<SystemRequirementEntity>
{
    public void Configure(EntityTypeBuilder<SystemRequirementEntity> builder)
    {
        builder.HasKey(e => e.RequirementId).HasName("system_requirements_pkey");

        builder.ToTable("system_requirements");

        builder.Property(e => e.RequirementId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("requirement_id");
        builder.Property(e => e.GameId).HasColumnName("game_id");
        builder.Property(e => e.MinRequirements).HasColumnName("min_requirements");
        builder.Property(e => e.RecommendedRequirements).HasColumnName("recommended_requirements");

        builder.HasOne(d => d.Game).WithMany(p => p.SystemRequirements)
            .HasForeignKey(d => d.GameId)
            .HasConstraintName("system_requirements_game_id_fkey");
    }
}