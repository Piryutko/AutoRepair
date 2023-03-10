using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepair.Domain;
using AutoRepair.Storage;

namespace AutoRepair.StorageInMemory
{
    public class BookingInMemoryStorage : IBookingStorage
    {
        public BookingInMemoryStorage()
        {
            _bookings = new List<Booking>();
        }

        private List<Booking> _bookings;

        public bool BookCar(Guid userId, Guid carId, DateTime from, DateTime to, out Guid bookingId)
        {
            var isCarBooked = _bookings.Any(c => c.CarId == carId);
            bookingId = Guid.Empty;

            if (isCarBooked)
            {
                var succeeded = false;

                foreach (var car in _bookings)
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
            var isBookedCar = _bookings.Any(r => r.Id == bookingId && r.From == from);

            if (isBookedCar)
            {
                var car = _bookings.SingleOrDefault(c => c.Id == bookingId && c.From == from);
                car.GiveCar();
                return isBookedCar;
            }
            return isBookedCar;
        }

        public bool ReturnCar(Guid bookingId, DateTime to)
        {
            var wasСarOnTrip = _bookings.Any(c => c.Id == bookingId && c.To == to && c.IsOnTheRoad);
            if (wasСarOnTrip)
            {
                var car = _bookings.SingleOrDefault(c => c.Id == bookingId && c.To == to && c.IsOnTheRoad);
                RemoveCar(car.CarId);
                return true;
            }
            return false;
        }

        private Guid AddBooking(Guid carId, Guid userId, DateTime from, DateTime to)
        {
            var booking = new Booking(carId, userId, from, to);
            _bookings.Add(booking);
            return booking.Id;
        }

        private void RemoveCar(Guid carId)
        {
            var car = _bookings.SingleOrDefault(c => c.CarId == carId);
            _bookings.Remove(car);
        }

        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }

        public bool GetCarId(Guid bookingId, out Guid carId)
        {
            var hasCar = _bookings.Any(u => u.Id == bookingId);
            carId = Guid.Empty;
            if (hasCar)
            {
                var car = _bookings.SingleOrDefault(u => u.Id == bookingId);
                carId = car.CarId;
                return hasCar;
            }

            return hasCar;
        }
    }
}
