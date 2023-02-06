using AutoRepair.Domain;
using AutoRepair.Storage;

namespace AutoRepair.StorageInMemory
{
    public class CarInMemoryStorage : ICarStorage
    {
        public CarInMemoryStorage()
        {
            _cars = new List<Car>();
        }

        private List<Car> _cars;

        public List<Car> GetAllAvailableCars()
        {
            var cars = _cars.Where(c => !c.IsCarBooked).ToList();
            return cars;
        }

        public List<Truck> GetAllTrucks()
        {
            return _cars.Where(car => car is Truck).Select(c => c as Truck).ToList();
        }

        public List<Car> GetAllAvailableTrucks()
        {
            var trucks = GetAllTrucks().Where(t => !t.IsCarBooked).Select(c => c as Car).ToList();
            return trucks;
        }

        public List<Truck> GetAllTrucksWithSuitableCapacity(int minCapacity)
        {
            var trucks = GetAllTrucks().Where(t => t.TrunkSize >= minCapacity).ToList();
            return trucks;
        }

        public List<Limousine> GetAllLimousines()
        {
            return _cars.Where(limousines => limousines is Limousine).Select(l => l as Limousine).ToList();
        }

        public List<Car> GetAllAvailableLimousines()
        {
            var lumousines = GetAllLimousines().Where(l => !l.IsCarBooked).Select(c => c as Car).ToList();
            return lumousines;
        }

        public List<Limousine> GetAllLimousinesWithBar(bool isThereBar)
        {
            var limousines = GetAllLimousines().Where(l => l.Bar == isThereBar).ToList();
            return limousines;
        }

        public List<Car> GetAllBookedCars()
        {
            var cars = _cars.Where(c => c.IsCarBooked).ToList();
            return cars;
        }

        public List<Car> GetAllCars()
        {
            return _cars;
        }

        public Guid GetCar(Guid carId)
        {
            var car = _cars.SingleOrDefault(c => c.Id == carId);
            return car.Id;
        }

        public Guid AddCar(Brand brand, Color color, DateTime year)
        {
            var car = new Car(brand, color, year);
            _cars.Add(car);
            return car.Id;
        }

        public Guid AddCar(Brand brand, Color color, DateTime year, int width, int depth, int height)
        {
            var truck = new Truck(brand, color, year, width, depth, height);
            _cars.Add(truck);
            return truck.Id;
        }

        public Guid AddCar(Brand brand, Color color, DateTime year, bool bar)
        {
            var limousine = new Limousine(brand, color, year, bar);
            _cars.Add(limousine);
            return limousine.Id;
        }

        public bool DeleteCar(Guid carId)
        {
            var isCarHere = _cars.Any(c => c.Id == carId);

            if (isCarHere)
            {
                var car = _cars.Single(c => c.Id == carId);
                _cars.Remove(car);
                return isCarHere;
            }

            return isCarHere;
        }

        public bool ChangeStatusBooking(Guid carId, bool bookingStatus)
        {
            var carBooked = _cars.SingleOrDefault(c => c.Id == carId);
            carBooked.SetBookingStatus(bookingStatus);
            return true;
        }

    }
}
