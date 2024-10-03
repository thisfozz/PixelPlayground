using DataAccess.Entities.Games;
using DataAccess.Entities.Users;

namespace DataAccess.Entities.Reviews;

public partial class ReviewEntity
{
    public Guid UserId { get; set; }

    public Guid GameId { get; set; }

    public string? ReviewText { get; set; }

    public bool Recommend { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual GameEntity Game { get; set; } = null!;

    public virtual ICollection<ReviewCommentEntity> ReviewComments { get; set; } = new List<ReviewCommentEntity>();

    public virtual ICollection<ReviewLikeEntity> ReviewLikes { get; set; } = new List<ReviewLikeEntity>();

    public virtual UserEntity User { get; set; } = null!;
}