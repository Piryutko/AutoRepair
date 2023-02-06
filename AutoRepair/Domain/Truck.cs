using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepair.Domain;

[Table("Trucks")]
public partial class Truck : Car
{
    public Truck(Brand brand, Color color, DateTime year) : base(brand, color, year)
    {

    }

    public Truck(Brand brand, Color color, DateTime year, int width, int depth, int height) : base(brand, color, year)
    {
        TrunkSize = width * depth * height / 100;
    }

    public int TrunkSize { get; set; }

    public virtual Car IdNavigation { get; set; } = null!;
}
