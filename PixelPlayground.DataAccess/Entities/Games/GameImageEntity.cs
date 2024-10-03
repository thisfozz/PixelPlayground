namespace DataAccess.Entities.Games;

public partial class GameImageEntity
{
    public Guid ImageId { get; set; }

    public Guid? GameId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual GameEntity? Game { get; set; }
}