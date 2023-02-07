using System;
using System.Collections.Generic;

namespace AdwentureWorks.Entities;

public partial class Building
{
    public int BuildingId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Apartament> Apartaments { get; } = new List<Apartament>();
}
