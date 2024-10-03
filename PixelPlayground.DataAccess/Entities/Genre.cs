namespace DataAccess.Entities;

public partial class Genre
{
    public Guid GenreId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}