using AutoRepair.Domain;

namespace AutoRepair.Storage
{
    public class BookingStorage : IBookingStorage
    {
        public BookingStorage()
        {
            _autoRepairDb = new AutoRepairContext();
        }

        private AutoRepairContext _autoRepairDb;

        public bool BookCar(Guid userId, Guid carId, DateTime from, DateTime to, out Guid bookingId)
        {
            var isCarBooked = _autoRepairDb.Bookings.Any(c => c.CarId == carId);
            bookingId = Guid.Empty;

            if (isCarBooked)
            {
                var succeeded = false;

                foreach (var car in GetAllBookings())
                {
                    if (from < car.From && to <= car.From || from >= car.To && to > car.To)
                    {
                        succeeded = true;
                    }
                    else
                    {
                        succeeded = false;
                        break;
                    }
                }

                if (succeeded)
                {
                    bookingId = AddBooking(carId, userId, from, to);
                }

                return succeeded;
            }

            bookingId = AddBooking(carId, userId, from, to);
            return true;
        }

        public bool PickUpCar(Guid bookingId, DateTime from)
        {
            var isBookedCar = _autoRepairDb.Bookings.Any(r => r.Id == bookingId && r.From == from);

            if (isBookedCar)
            {
                var car = _autoRepairDb.Bookings.SingleOrDefault(c => c.Id == bookingId && c.From == from);
                car.GiveCar();
                _autoRepairDb.Bookings.Update(car);
                _autoRepairDb.SaveChanges();
                return isBookedCar;
            }
            return isBookedCar;
        }

        public bool ReturnCar(Guid bookingId, DateTime to)
        {
            var wasСarOnTrip = _autoRepairDb.Bookings.Any(c => c.Id == bookingId && c.To == to && c.IsOnTheRoad);
            if (wasСarOnTrip)
            {
                var car = _autoRepairDb.Bookings.SingleOrDefault(c => c.Id == bookingId && c.To == to && c.IsOnTheRoad);
                RemoveCar(car.CarId);
                _autoRepairDb.SaveChanges();
                return true;
            }
            return false;
        }

        private Guid AddBooking(Guid carId, Guid userId, DateTime from, DateTime to)
        {
            var booking = new Booking(carId, userId, from, to);
            _autoRepairDb.Bookings.Add(booking);
            _autoRepairDb.SaveChanges();
            return booking.Id;
        }

        private void RemoveCar(Guid carId)
        {
            var car = _autoRepairDb.Bookings.SingleOrDefault(c => c.CarId == carId);
            _autoRepairDb.Bookings.Remove(car);
            _autoRepairDb.SaveChanges();
        }

        public List<Booking> GetAllBookings()
        {
            return _autoRepairDb.Bookings.ToList();
        }

        public bool GetCarId(Guid bookingId, out Guid carId)
        {
            var hasCar = _autoRepairDb.Bookings.Any(u => u.Id == bookingId);
            carId = Guid.Empty;
            if (hasCar)
            {
                var car = _autoRepairDb.Bookings.SingleOrDefault(u => u.Id == bookingId);
                carId = car.CarId;
                return hasCar;
            }

            return hasCar;
        }
    }
}
