using AutoRepair.Domain;

namespace AutoRepair.Storage
{
    public interface IBookingStorage
    {
        bool BookCar(Guid userId, Guid carId, DateTime from, DateTime to, out Guid bookingId);

        bool PickUpCar(Guid bookingId, DateTime from);

        bool ReturnCar(Guid bookingId, DateTime to);

        List<Booking> GetAllBookings();

        bool GetCarId(Guid bookingId, out Guid carId);
    }
}
