using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairLibrary
{
    interface ICarStorage
    {
        List<Car> GetAllAvailableCars();

        List<Car> GetAllBookedCars();

        Guid GetCar(Guid carId);

        List<Car> GetAllCars();

        List<Truck> GetAllTrucks();

        List<Truck> GetAllAvailableTrucks();

        List<Truck> GetAllTrucksWithSuitableCapacity(double capacity);

        List<Limousine> GetAllLimousines();

        List<Limousine> GetAllAvailableLimousines();

        List<Limousine> GetAllLimousinesWithBar(bool isThereBar);

        Guid AddCar(Brand brand, Color color, DateTime year);
        Guid AddCar(Brand brand, Color color, DateTime year, double width, double depth, double height);

        Guid AddCar(Brand brand, Color color, DateTime year, bool bar);

        bool DeleteCar(Guid carId);

        bool ChangeStatusBooking(Guid carId, bool bookingStatus);
    }
}
