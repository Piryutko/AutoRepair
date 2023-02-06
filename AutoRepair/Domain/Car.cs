namespace AutoRepair.Domain;

public partial class Car
{
    public Car(Brand brand, Color color, DateTime year)
    {
        Brand = brand;
        Color = color;
        Year = year;
        Id = Guid.NewGuid();
        IsCarBooked = false;
    }

    public Car(Guid id, Brand brand, Color color, DateTime year, bool isCarBooked)
    {
        Id = id;
        Brand = brand;
        Color = color;
        Year = year;
        IsCarBooked = isCarBooked;
    }

    public Guid Id { get; set; }

    public Brand Brand { get; set; }

    public Color Color { get; set; }

    public DateTime Year { get; set; }

    public bool IsCarBooked { get; set; }

    public void SetBookingStatus(bool isCarBooked)
    {
        IsCarBooked = isCarBooked;
    }

    public virtual Limousine? Limousine { get; set; }

    public virtual Truck? Truck { get; set; }
}
