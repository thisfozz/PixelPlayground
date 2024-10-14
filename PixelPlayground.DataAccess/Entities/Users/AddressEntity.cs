namespace DataAccess.Entities.Users;

public partial class AddressEntity
{
    public Guid AddressId { get; set; }

    public Guid UserId { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}