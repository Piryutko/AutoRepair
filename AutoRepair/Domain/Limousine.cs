using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepair.Domain;

[Table("Limousines")]
public partial class Limousine : Car
{
    public Limousine(Brand brand, Color color, DateTime year) : base(brand, color, year)
    {

    }

    public Limousine(Brand brand, Color color, DateTime year, bool hasBar) : base(brand, color, year)
    {
        Bar = hasBar;
    }

    public bool Bar { get; set; }

    public virtual Car IdNavigation { get; set; } = null!;
}
