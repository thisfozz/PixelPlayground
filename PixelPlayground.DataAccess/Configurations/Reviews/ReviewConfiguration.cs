using DataAccess.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Reviews;

public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.HasKey(e => new { e.UserId, e.GameId }).HasName("reviews_pkey");

        builder.ToTable("reviews");

        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.GameId).HasColumnName("game_id");
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("created_at");
        builder.Property(e => e.Recommend).HasColumnName("recommend");
        builder.Property(e => e.ReviewText).HasColumnName("review_text");
        builder.Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("updated_at");

        builder.HasOne(d => d.Game).WithMany(p => p.Reviews)
            .HasForeignKey(d => d.GameId)
            .HasConstraintName("reviews_game_id_fkey");

        builder.HasOne(d => d.User).WithMany(p => p.Reviews)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("reviews_user_id_fkey");
    }
}