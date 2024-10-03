using DataAccess.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Reviews;

public class ReviewCommentConfiguration : IEntityTypeConfiguration<ReviewCommentEntity>
{
    public void Configure(EntityTypeBuilder<ReviewCommentEntity> builder)
    {
        builder.HasKey(e => e.CommentId).HasName("review_comments_pkey");

        builder.ToTable("review_comments");

        builder.Property(e => e.CommentId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("comment_id");
        builder.Property(e => e.CommentText).HasColumnName("comment_text");
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("created_at");
        builder.Property(e => e.ReviewGameId).HasColumnName("review_game_id");
        builder.Property(e => e.ReviewUserId).HasColumnName("review_user_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.User).WithMany(p => p.ReviewComments)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("review_comments_user_id_fkey");

        builder.HasOne(d => d.Review).WithMany(p => p.ReviewComments)
            .HasForeignKey(d => new { d.ReviewUserId, d.ReviewGameId })
            .HasConstraintName("review_comments_review_user_id_review_game_id_fkey");
    }
}