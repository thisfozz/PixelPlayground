using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class ReviewComment
{
    public Guid CommentId { get; set; }

    public Guid ReviewUserId { get; set; }

    public Guid ReviewGameId { get; set; }

    public Guid UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Review Review { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
