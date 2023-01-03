using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairLibrary
{
    interface IBookingStorage
    {
        bool BookCar(Guid userId, Guid carId, DateTime from, DateTime to, out Guid bookingId);

        bool PickUpCar(Guid bookingId, DateTime from);

        bool ReturnCar(Guid bookingId, DateTime to);

        List<Booking> GetAllBookings();

        bool GetCarId(Guid bookingId, out Guid carId);

    }
}
