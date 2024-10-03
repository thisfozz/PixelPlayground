using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Address
{
    public Guid AddressId { get; set; }

    public Guid UserId { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? Address1 { get; set; }

    public string? PostalCode { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual User User { get; set; } = null!;
}
