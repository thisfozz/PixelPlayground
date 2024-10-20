using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configurations.Games;

public class PurchasedGameConfiguration : IEntityTypeConfiguration<PurchasedGameEntity>
{
    public void Configure(EntityTypeBuilder<PurchasedGameEntity> builder)
    {
        builder.HasKey(e => e.PurchaseId).HasName("purchased_games_pkey");

        builder.ToTable("purchased_games");

        builder.Property(e => e.PurchaseId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("purchase_id");

        builder.Property(e => e.UserId)
            .HasColumnName("user_id");

        builder.Property(e => e.GameId)
            .HasColumnName("game_id");

        builder.Property(e => e.PurchaseDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("purchase_date");

        builder.HasOne(d => d.User)
            .WithMany(u => u.PurchasedGames)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("purchased_games_user_id_fkey");

        builder.HasOne(d => d.Game)
            .WithMany(g => g.PurchasedGames)
            .HasForeignKey(d => d.GameId)
            .HasConstraintName("purchased_games_game_id_fkey");
    }
}