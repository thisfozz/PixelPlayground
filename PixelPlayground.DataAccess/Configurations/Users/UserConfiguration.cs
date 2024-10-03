using DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(e => e.UserId).HasName("users_pkey");

        builder.ToTable("users");

        builder.HasIndex(e => e.DisplayName, "users_display_name_key").IsUnique();

        builder.HasIndex(e => e.Email, "users_email_key").IsUnique();

        builder.HasIndex(e => e.Login, "users_login_key").IsUnique();

        builder.Property(e => e.UserId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("user_id");
        builder.Property(e => e.AccountCreationDate)
            .HasDefaultValueSql("CURRENT_DATE")
            .HasColumnName("account_creation_date");
        builder.Property(e => e.Balance)
            .HasPrecision(15, 2)
            .HasColumnName("balance");
        builder.Property(e => e.DisplayName)
            .HasMaxLength(20)
            .HasColumnName("display_name");
        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .HasColumnName("email");
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
        builder.Property(e => e.Login)
            .HasMaxLength(30)
            .HasColumnName("login");
        builder.Property(e => e.PasswordHash).HasColumnName("password_hash");
        builder.Property(e => e.RoleId).HasColumnName("role_id");

        builder.HasOne(d => d.Role).WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("users_role_id_fkey");
    }
}