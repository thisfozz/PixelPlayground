namespace DataAccess.Entities;

public partial class SystemRequirement
{
    public Guid RequirementId { get; set; }

    public Guid GameId { get; set; }

    public string? MinRequirements { get; set; }

    public string? RecommendedRequirements { get; set; }

    public virtual Game Game { get; set; } = null!;
}