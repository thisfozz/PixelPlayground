using DataAccess.Entities.Users;

namespace DataAccess.Entities.Reviews;

public partial class ReviewLikeEntity
{
    public Guid ReviewUserId { get; set; }

    public Guid ReviewGameId { get; set; }

    public Guid UserId { get; set; }

    public bool IsLike { get; set; }

    public virtual ReviewEntity Review { get; set; } = null!;

    public virtual UserEntity User { get; set; } = null!;
}