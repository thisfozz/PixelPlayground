using DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Users;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(e => e.RoleId).HasName("roles_pkey");

        builder.ToTable("roles");

        builder.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

        builder.Property(e => e.RoleId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("role_id");
        builder.Property(e => e.RoleName)
            .HasMaxLength(30)
            .HasColumnName("role_name");
    }
}