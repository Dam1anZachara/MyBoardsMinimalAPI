using System;
using System.Collections.Generic;

namespace AdwentureWorks.Entities;

public partial class Miastum
{
    public int IdMiasta { get; set; }

    public string? Miasto { get; set; }

    public int Odległość { get; set; }

    public DateTime DataEdycji { get; set; }
}
