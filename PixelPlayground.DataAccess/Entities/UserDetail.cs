namespace DataAccess.Entities;

public partial class UserDetail
{
    public Guid UserDetailsId { get; set; }

    public Guid UserId { get; set; }

    public string? AvatarUrl { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}