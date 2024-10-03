namespace DataAccess.Entities.Games;

public partial class PublisherEntity
{
    public Guid PublisherId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GameEntity> Games { get; set; } = new List<GameEntity>();
}