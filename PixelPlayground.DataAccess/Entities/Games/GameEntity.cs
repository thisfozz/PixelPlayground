using DataAccess.Entities.Reviews;

namespace DataAccess.Entities.Games;

public partial class GameEntity
{
    public Guid GameId { get; set; }

    public string Title { get; set; } = null!;

    public string CoverUrl { get; set; } = null!;

    public decimal Rating { get; set; }

    public int ReviewCount { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? ReleaseDateStr { get; set; }

    public string? Description { get; set; }

    public Guid? DeveloperId { get; set; }

    public Guid? PublisherId { get; set; }

    public Guid? PlatformId { get; set; }

    public virtual DeveloperEntity? Developer { get; set; }

    public virtual ICollection<GameImageEntity> GameImages { get; set; } = new List<GameImageEntity>();

    public virtual PlatformEntity? Platform { get; set; }

    public virtual PublisherEntity? Publisher { get; set; }

    public virtual ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();

    public virtual ICollection<SystemRequirementEntity> SystemRequirements { get; set; } = new List<SystemRequirementEntity>();

    public virtual ICollection<FeatureEntity> Features { get; set; } = new List<FeatureEntity>();

    public virtual ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();

    public virtual ICollection<PlatformEntity> Platforms { get; set; } = new List<PlatformEntity>();

    public virtual ICollection<PurchasedGameEntity> PurchasedGames { get; set; } = new List<PurchasedGameEntity>();
}