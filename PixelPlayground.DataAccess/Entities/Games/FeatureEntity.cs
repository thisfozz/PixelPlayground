namespace DataAccess.Entities.Games;

public partial class FeatureEntity
{
    public Guid FeatureId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GameEntity> Games { get; set; } = new List<GameEntity>();
}