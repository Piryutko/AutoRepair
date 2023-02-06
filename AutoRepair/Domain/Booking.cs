using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AutoRepair.Domain;

public class Booking
{
    public Booking(Guid carId, Guid userId, DateTime from, DateTime to)
    {
        CarId = carId;
        UserId = userId;
        From = from;
        To = to;
        Id = Guid.NewGuid();
        IsOnTheRoad = false;
    }


    public Guid Id { get; set; }

    public Guid CarId { get; set; }

    public Guid UserId { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public bool IsOnTheRoad { get; set; }

    public void GiveCar()
    {
        IsOnTheRoad = true;
    }
}
