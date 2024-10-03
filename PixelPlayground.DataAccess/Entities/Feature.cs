using DataAccess.Models;

namespace DataAccess.Entities;

public partial class Feature
{
    public Guid FeatureId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}