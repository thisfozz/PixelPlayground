using DataAccess.Entities.Games;
using DataAccess.Entities.Reviews;

namespace DataAccess.Entities.Users;

public partial class UserEntity
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

    public virtual ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();

    public virtual ICollection<ReviewCommentEntity> ReviewComments { get; set; } = new List<ReviewCommentEntity>();

    public virtual ICollection<ReviewLikeEntity> ReviewLikes { get; set; } = new List<ReviewLikeEntity>();

    public virtual ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();

    public virtual RoleEntity Role { get; set; } = null!;

    public virtual ICollection<UserDetailEntity> UserDetails { get; set; } = new List<UserDetailEntity>();

    public virtual ICollection<PurchasedGameEntity> PurchasedGames { get; set; } = new List<PurchasedGameEntity>();
}