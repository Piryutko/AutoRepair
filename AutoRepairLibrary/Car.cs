using System;

namespace AutoRepairLibrary
{
    public class Car
    {
        public Car(Brand brand, Color color, DateTime year)
        {
            Brand = brand;
            Color = color;
            Year = year;
            Id = Guid.NewGuid();
            IsCarBooked = false;
        }

        public Brand Brand { get; }

        public Color Color { get; }

        public DateTime Year { get; }

        public Guid Id { get; }

        public bool IsCarBooked { get; private set; }

        public void SetBookingStatus(bool isCarBooked)
        {
            IsCarBooked = isCarBooked;
        }
    }

}
