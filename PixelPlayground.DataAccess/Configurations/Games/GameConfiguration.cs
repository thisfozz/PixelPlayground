using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Games;

public class GameConfiguration : IEntityTypeConfiguration<GameEntity>
{
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder.HasKey(e => e.GameId).HasName("game_pkey");

        builder.ToTable("game");

        builder.Property(e => e.GameId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("game_id");

        builder.Property(e => e.CoverUrl).HasColumnName("cover_url");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.DeveloperId).HasColumnName("developer_id");
        builder.Property(e => e.PlatformId).HasColumnName("platform_id");
        builder.Property(e => e.PublisherId).HasColumnName("publisher_id");

        builder.Property(e => e.Rating)
            .HasPrecision(5, 2)
            .HasColumnName("rating");

        builder.Property(e => e.ReleaseDate).HasColumnName("release_date");
        builder.Property(e => e.ReleaseDateStr)
            .HasMaxLength(20)
            .HasColumnName("release_date_str");

        builder.Property(e => e.ReviewCount)
            .HasDefaultValue(0)
            .HasColumnName("review_count");

        builder.Property(e => e.Title)
            .HasMaxLength(100)
            .HasColumnName("title");

        builder.HasOne(d => d.Developer)
            .WithMany(p => p.Games)
            .HasForeignKey(d => d.DeveloperId)
            .HasConstraintName("game_developer_id_fkey");

        builder.HasOne(d => d.Platform)
            .WithMany(p => p.Games)
            .HasForeignKey(d => d.PlatformId)
            .HasConstraintName("game_platform_id_fkey");

        builder.HasOne(d => d.Publisher)
            .WithMany(p => p.Games)
            .HasForeignKey(d => d.PublisherId)
            .HasConstraintName("game_publisher_id_fkey");

        builder.HasMany(d => d.Features)
            .WithMany(p => p.Games)
            .UsingEntity<Dictionary<string, object>>(
                "GameFeature",
                r => r.HasOne<FeatureEntity>().WithMany()
                    .HasForeignKey("FeatureId")
                    .HasConstraintName("game_features_feature_id_fkey"),
                l => l.HasOne<GameEntity>().WithMany()
                    .HasForeignKey("GameId")
                    .HasConstraintName("game_features_game_id_fkey"),
                j =>
                {
                    j.HasKey("GameId", "FeatureId").HasName("game_features_pkey");
                    j.ToTable("game_features");
                    j.IndexerProperty<Guid>("GameId").HasColumnName("game_id");
                    j.IndexerProperty<Guid>("FeatureId").HasColumnName("feature_id");
                });

        builder.HasMany(d => d.Genres)
            .WithMany(p => p.Games)
            .UsingEntity<Dictionary<string, object>>(
                "GameGenre",
                r => r.HasOne<GenreEntity>().WithMany()
                    .HasForeignKey("GenreId")
                    .HasConstraintName("game_genres_genre_id_fkey"),
                l => l.HasOne<GameEntity>().WithMany()
                    .HasForeignKey("GameId")
                    .HasConstraintName("game_genres_game_id_fkey"),
                j =>
                {
                    j.HasKey("GameId", "GenreId").HasName("game_genres_pkey");
                    j.ToTable("game_genres");
                    j.IndexerProperty<Guid>("GameId").HasColumnName("game_id");
                    j.IndexerProperty<Guid>("GenreId").HasColumnName("genre_id");
                });

        builder.HasMany(d => d.Platforms)
            .WithMany(p => p.GamesNavigation)
            .UsingEntity<Dictionary<string, object>>(
                "GamePlatform",
                r => r.HasOne<PlatformEntity>().WithMany()
                    .HasForeignKey("PlatformId")
                    .HasConstraintName("game_platforms_platform_id_fkey"),
                l => l.HasOne<GameEntity>().WithMany()
                    .HasForeignKey("GameId")
                    .HasConstraintName("game_platforms_game_id_fkey"),
                j =>
                {
                    j.HasKey("GameId", "PlatformId").HasName("game_platforms_pkey");
                    j.ToTable("game_platforms");
                    j.IndexerProperty<Guid>("GameId").HasColumnName("game_id");
                    j.IndexerProperty<Guid>("PlatformId").HasColumnName("platform_id");
                });
    }
}