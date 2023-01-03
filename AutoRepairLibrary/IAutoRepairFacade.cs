using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairLibrary
{
    interface IAutoRepairFacade
    {
        Guid CreateUser(string name, int age);

        void DeleteUser(Guid userId);

        Guid AddCar(Brand brand, Color color, DateTime year);

        List<User> GetAllUsers();

        void DeleteCar(Guid carId);

        List<Car> GetAllAvailableCar();

        List<Car> GetAllCars();

        List<Truck> GetAllTrucks();

        List<Limousine> GetAllLimousines();

        List<Booking> GetAllBookings();

        bool BookCar(Guid userId, Guid carId, DateTime from, DateTime to, out Guid bookingId);

        bool PickUpCar(Guid bookingId, DateTime from);

        bool ReturnCar(Guid bookingId, DateTime to);

    }
}
