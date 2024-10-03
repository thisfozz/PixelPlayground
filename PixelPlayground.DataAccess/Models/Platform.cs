using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Platform
{
    public Guid PlatformId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<Game> GamesNavigation { get; set; } = new List<Game>();
}
