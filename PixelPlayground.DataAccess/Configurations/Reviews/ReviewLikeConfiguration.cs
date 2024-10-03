using DataAccess.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Reviews
{
    public class ReviewLikeConfiguration : IEntityTypeConfiguration<ReviewLikeEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewLikeEntity> builder)
        {
            builder.HasKey(e => new { e.ReviewUserId, e.ReviewGameId, e.UserId }).HasName("review_likes_pkey");

            builder.ToTable("review_likes");

            builder.Property(e => e.ReviewUserId).HasColumnName("review_user_id");
            builder.Property(e => e.ReviewGameId).HasColumnName("review_game_id");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.IsLike).HasColumnName("is_like");

            builder.HasOne(d => d.User).WithMany(p => p.ReviewLikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("review_likes_user_id_fkey");

            builder.HasOne(d => d.Review).WithMany(p => p.ReviewLikes)
                .HasForeignKey(d => new { d.ReviewUserId, d.ReviewGameId })
                .HasConstraintName("review_likes_review_user_id_review_game_id_fkey");
        }
    }
}