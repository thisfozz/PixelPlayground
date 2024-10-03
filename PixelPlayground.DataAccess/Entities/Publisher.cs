namespace DataAccess.Entities;

public partial class Publisher
{
    public Guid PublisherId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}