using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Game
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

    public virtual Developer? Developer { get; set; }

    public virtual ICollection<GameImage> GameImages { get; set; } = new List<GameImage>();

    public virtual Platform? Platform { get; set; }

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<SystemRequirement> SystemRequirements { get; set; } = new List<SystemRequirement>();

    public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public virtual ICollection<Platform> Platforms { get; set; } = new List<Platform>();
}
