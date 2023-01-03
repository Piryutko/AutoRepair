using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoRepairLibrary
{
    public class AutoRepairFacade : IAutoRepairFacade
    {
        public AutoRepairFacade(CarStorage carStorage,BookingStorage bookStorage, UserStorage userStorage)
        {
            _carStorage = carStorage;
            _bookingStorage = bookStorage;
            _userStorage = userStorage;
        }

        private CarStorage _carStorage;

        private BookingStorage _bookingStorage;

        private UserStorage _userStorage;

        public Guid CreateUser(string name, int age)
        {
            return _userStorage.AddUser(name, age);
        }

        public void DeleteUser(Guid userId)
        {
            _userStorage.DeleteUser(userId);
        }

        public Guid AddCar(Brand brand, Color color, DateTime year)
        {
            return _carStorage.AddCar(brand, color, year);
        }

        public Guid AddCar(Brand brand, Color color, DateTime year, double width, double depth, double height)
        {
            return _carStorage.AddCar(brand, color, year,width, depth, height);
        }

        public Guid AddCar(Brand brand, Color color, DateTime year, bool bar)
        {
            return _carStorage.AddCar(brand, color, year, bar);
        }

        public List<User> GetAllUsers()
        {
            return _userStorage.GetAllUsers();
        }

        public void DeleteCar(Guid carId)
        {
            _carStorage.DeleteCar(carId);
        }

        public List<Truck> GetAllTrucks()
        {
            return _carStorage.GetAllTrucks();
        }

        public List<Truck> GetAllAvailableTrucks()
        {
            return _carStorage.GetAllAvailableTrucks();
        }

        public List<Truck> GetAllTruckWithSuitableCapacity(double capacity)
        {
            return _carStorage.GetAllTrucksWithSuitableCapacity(capacity);
        }
        
        public List<Limousine> GetAllLimousines()
        {
            return _carStorage.GetAllLimousines();
        }

        public List<Limousine> GetAllAvailableLimousines()
        {
            return _carStorage.GetAllAvailableLimousines();
        }

        public List<Limousine> GetAllLimousinesWithBar(bool isThereBar)
        {
            return _carStorage.GetAllLimousinesWithBar(isThereBar);
        }

        public List<Car> GetAllAvailableCar()
        {
            return _carStorage.GetAllAvailableCars();
        }

        public List<Booking> GetAllBookings()
        {
            return _bookingStorage.GetAllBookings();
        }

        public List<Car> GetAllCars()
        {
           return _carStorage.GetAllCars();
        }

        public bool BookCar(Guid userId, Guid carId, DateTime from, DateTime to, out Guid bookingId)
        {
            bookingId = Guid.Empty;

            var carBooked = _bookingStorage.BookCar(userId, carId, from, to, out Guid id);

            if (carBooked)
            {
                _carStorage.ChangeStatusBooking(carId, true);
                bookingId = id;
            }

            return carBooked;
        }

        public bool PickUpCar(Guid bookingId, DateTime from)
        {
            var result = _bookingStorage.PickUpCar(bookingId, from);
            return result;
        }

        public bool ReturnCar(Guid bookingId, DateTime to)
        {
            _bookingStorage.GetCarId(bookingId, out Guid carId);
            var result = _bookingStorage.ReturnCar(bookingId, to);

            if (result)
            {
                _carStorage.ChangeStatusBooking(carId, false);
            }

            return result;
        }

    }
}
