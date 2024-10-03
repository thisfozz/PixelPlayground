using DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Users;
public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetailEntity>
{
    public void Configure(EntityTypeBuilder<UserDetailEntity> builder)
    {
        builder.HasKey(e => e.UserDetailsId).HasName("user_details_pkey");

        builder.ToTable("user_details");

        builder.Property(e => e.UserDetailsId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("user_details_id");
        builder.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
        builder.Property(e => e.Birthdate).HasColumnName("birthdate");
        builder.Property(e => e.FirstName)
            .HasMaxLength(30)
            .HasColumnName("first_name");
        builder.Property(e => e.LastName)
            .HasMaxLength(30)
            .HasColumnName("last_name");
        builder.Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("updated_at");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.User).WithMany(p => p.UserDetails)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("user_details_user_id_fkey");
    }
}