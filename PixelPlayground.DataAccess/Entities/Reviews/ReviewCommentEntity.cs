using DataAccess.Entities.Users;

namespace DataAccess.Entities.Reviews;

public partial class ReviewCommentEntity
{
    public Guid CommentId { get; set; }

    public Guid ReviewUserId { get; set; }

    public Guid ReviewGameId { get; set; }

    public Guid UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ReviewEntity Review { get; set; } = null!;

    public virtual UserEntity User { get; set; } = null!;
}