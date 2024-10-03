using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Review
{
    public Guid UserId { get; set; }

    public Guid GameId { get; set; }

    public string? ReviewText { get; set; }

    public bool Recommend { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual ICollection<ReviewComment> ReviewComments { get; set; } = new List<ReviewComment>();

    public virtual ICollection<ReviewLike> ReviewLikes { get; set; } = new List<ReviewLike>();

    public virtual User User { get; set; } = null!;
}
