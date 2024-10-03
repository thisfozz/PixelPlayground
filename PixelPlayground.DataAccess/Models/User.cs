using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? DisplayName { get; set; }

    public Guid RoleId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateOnly AccountCreationDate { get; set; }

    public decimal Balance { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<ReviewComment> ReviewComments { get; set; } = new List<ReviewComment>();

    public virtual ICollection<ReviewLike> ReviewLikes { get; set; } = new List<ReviewLike>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
