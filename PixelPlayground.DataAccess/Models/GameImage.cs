using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class GameImage
{
    public Guid ImageId { get; set; }

    public Guid? GameId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual Game? Game { get; set; }
}
