using EnsureThat;
using System;

namespace AutoRepairLibrary
{
    public class Booking
    {
        public Booking(Guid carId, Guid userId, DateTime from, DateTime to)
        {
            CarId = carId;
            UserId = userId;
            To = to;
            From = from;
            IsOnTheRoad = false;
            BookingId = Guid.NewGuid();
        }

        public Guid CarId { get; }

        public Guid UserId { get; }

        public DateTime From { get; }

        public DateTime To { get; }

        public Guid BookingId { get; }

        public bool IsOnTheRoad { get; private set; }

        public void GiveCar()
        {
            IsOnTheRoad = true;
        }
    }
}
