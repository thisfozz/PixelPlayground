using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Games;
public class GameImageConfiguration : IEntityTypeConfiguration<GameImageEntity>
{
    public void Configure(EntityTypeBuilder<GameImageEntity> builder)
    {
        builder.HasKey(e => e.ImageId).HasName("game_images_pkey");

        builder.ToTable("game_images");

        builder.Property(e => e.ImageId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("image_id");
        builder.Property(e => e.GameId).HasColumnName("game_id");
        builder.Property(e => e.ImageUrl).HasColumnName("image_url");

        builder.HasOne(d => d.Game).WithMany(p => p.GameImages)
            .HasForeignKey(d => d.GameId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("game_images_game_id_fkey");
    }
}