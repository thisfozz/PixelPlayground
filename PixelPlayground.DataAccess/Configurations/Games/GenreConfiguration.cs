using DataAccess.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Games;

public class GenreConfiguration : IEntityTypeConfiguration<GenreEntity>
{
    public void Configure(EntityTypeBuilder<GenreEntity> builder)
    {
        builder.HasKey(e => e.GenreId).HasName("genres_pkey");

        builder.ToTable("genres");

        builder.HasIndex(e => e.Name, "genres_name_key").IsUnique();

        builder.Property(e => e.GenreId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("genre_id");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
    }
}