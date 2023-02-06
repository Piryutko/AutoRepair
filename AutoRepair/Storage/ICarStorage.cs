using AutoRepair.Domain;

namespace AutoRepair.Storage
{
    public interface ICarStorage
    {
        List<Car> GetAllAvailableCars();

        List<Car> GetAllBookedCars();

        Guid GetCar(Guid carId);

        List<Car> GetAllCars();

        List<Truck> GetAllTrucks();

        List<Car> GetAllAvailableTrucks();

        List<Truck> GetAllTrucksWithSuitableCapacity(int capacity);

        List<Limousine> GetAllLimousines();

        List<Car> GetAllAvailableLimousines();

        List<Limousine> GetAllLimousinesWithBar(bool isThereBar);

        Guid AddCar(Brand brand, Color color, DateTime year);

        Guid AddCar(Brand brand, Color color, DateTime year, int width, int depth, int height);

        Guid AddCar(Brand brand, Color color, DateTime year, bool bar);

        bool DeleteCar(Guid carId);

        bool ChangeStatusBooking(Guid carId, bool bookingStatus);
    }
}
