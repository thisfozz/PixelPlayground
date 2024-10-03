namespace DataAccess.Entities.Games;

public partial class SystemRequirementEntity
{
    public Guid RequirementId { get; set; }

    public Guid GameId { get; set; }

    public string? MinRequirements { get; set; }

    public string? RecommendedRequirements { get; set; }

    public virtual GameEntity Game { get; set; } = null!;
}