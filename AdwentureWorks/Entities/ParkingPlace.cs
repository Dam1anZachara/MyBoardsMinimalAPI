using System;
using System.Collections.Generic;

namespace AdwentureWorks.Entities;

public partial class ParkingPlace
{
    public int ParkingPlaceId { get; set; }

    public int? GarageId { get; set; }

    public string? Remarks { get; set; }

    public bool IsPaid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Garage? Garage { get; set; }
}
