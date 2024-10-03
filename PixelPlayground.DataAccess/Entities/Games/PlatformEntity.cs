namespace DataAccess.Entities.Games;

public partial class PlatformEntity
{
    public Guid PlatformId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GameEntity> Games { get; set; } = new List<GameEntity>();

    public virtual ICollection<GameEntity> GamesNavigation { get; set; } = new List<GameEntity>();
}