using AutoRepair.Domain;

namespace AutoRepair.Facade
{
    internal interface IAutoRepairFacade
    {
        public Guid CreateUser(string name, int age);

        public void DeleteUser(Guid userId);

        public Guid AddCar(Brand brand, Color color, DateTime year);

        public Guid AddCar(Brand brand, Color color, DateTime year, int width, int depth, int height);

        public Guid AddCar(Brand brand, Color color, DateTime year, bool bar);

        public List<User> GetAllUsers();

        public void DeleteCar(Guid carId);

        public List<Truck> GetAllTrucks();

        public List<Car> GetAllAvailableTrucks();

        public List<Truck> GetAllTruckWithSuitableCapacity(int capacity);

        public List<Limousine> GetAllLimousines();

        public List<Car> GetAllAvailableLimousines();

        public List<Limousine> GetAllLimousinesWithBar(bool isThereBar);

        public List<Car> GetAllAvailableCar();

        public List<Booking> GetAllBookings();

        public List<Car> GetAllCars();

        public bool BookCar(Guid userId, Guid carId, DateTime from, DateTime to, out Guid bookingId);

        public bool PickUpCar(Guid bookingId, DateTime from);

        public bool ReturnCar(Guid bookingId, DateTime to);
    }
}
