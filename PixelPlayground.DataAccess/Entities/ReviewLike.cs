using DataAccess.Models;

namespace DataAccess.Entities;

public partial class ReviewLike
{
    public Guid ReviewUserId { get; set; }

    public Guid ReviewGameId { get; set; }

    public Guid UserId { get; set; }

    public bool IsLike { get; set; }

    public virtual Review Review { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}