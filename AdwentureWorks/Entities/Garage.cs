using System;
using System.Collections.Generic;

namespace AdwentureWorks.Entities;

public partial class Garage
{
    public int GarageId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<ParkingPlace> ParkingPlaces { get; } = new List<ParkingPlace>();
}
