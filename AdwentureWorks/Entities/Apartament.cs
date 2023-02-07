using System;
using System.Collections.Generic;

namespace AdwentureWorks.Entities;

public partial class Apartament
{
    public int ApartamentId { get; set; }

    public string Name { get; set; } = null!;

    public int? BuildingId { get; set; }

    public virtual Building? Building { get; set; }
}
