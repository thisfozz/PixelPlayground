using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Genre
{
    public Guid GenreId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
