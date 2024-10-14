using DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Users;

public class AddressesConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(e => e.AddressId).HasName("addresses_pkey");

        builder.ToTable("addresses");

        builder.Property(e => e.AddressId)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("address_id");
        builder.Property(e => e.Address)
            .HasMaxLength(100)
            .HasColumnName("address");
        builder.Property(e => e.City)
            .HasMaxLength(30)
            .HasColumnName("city");
        builder.Property(e => e.Country)
            .HasMaxLength(30)
            .HasColumnName("country");
        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20)
            .HasColumnName("phone_number");
        builder.Property(e => e.PostalCode)
            .HasMaxLength(20)
            .HasColumnName("postal_code");
        builder.Property(e => e.Region)
            .HasMaxLength(30)
            .HasColumnName("region");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.User)
            .WithMany(p => p.Addresses)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("addresses_user_id_fkey");
    }
}