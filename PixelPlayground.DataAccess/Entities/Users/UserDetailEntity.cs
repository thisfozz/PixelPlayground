namespace DataAccess.Entities.Users;

public partial class UserDetailEntity
{
    public Guid UserDetailsId { get; set; }

    public Guid UserId { get; set; }

    public string? AvatarUrl { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? FirstName { get; set; }  

    public string? LastName { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}