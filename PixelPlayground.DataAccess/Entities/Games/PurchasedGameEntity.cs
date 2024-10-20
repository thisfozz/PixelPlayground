using DataAccess.Entities.Users;

namespace DataAccess.Entities.Games;

public partial class PurchasedGameEntity
{
    public Guid PurchaseId { get; set; }

    public Guid UserId { get; set; }

    public Guid GameId { get; set; }

    public DateTime PurchaseDate { get; set; }

    public virtual UserEntity User { get; set; } = null!;
    public virtual GameEntity Game { get; set; } = null!;
}