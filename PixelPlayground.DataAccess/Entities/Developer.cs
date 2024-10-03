using DataAccess.Models;

namespace DataAccess.Entities;

public partial class Developer
{
    public Guid DeveloperId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}