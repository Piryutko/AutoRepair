using AutoRepair.Storage;

namespace AutoRepair.Domain
{
    public class CarStorage : ICarStorage
    {
        public CarStorage()
        {
            _autoRepairDb = new AutoRepairContext();
        }

        private AutoRepairContext _autoRepairDb;

        public List<Car> GetAllAvailableCars()
        {
            var cars = _autoRepairDb.Cars.Where(c => c.IsCarBooked == false).ToList();
            return cars;
        }

        public List<Truck> GetAllTrucks()
        {
            return _autoRepairDb.Trucks.ToList();
        }

        public List<Car> GetAllAvailableTrucks()
        {
            var trucks = _autoRepairDb.Cars.Join(_autoRepairDb.Trucks, c => c.Id, t => t.Id,
                (c, t) => new Car(c.Id, c.Brand, c.Color, c.Year, c.IsCarBooked));

            var allAvailableTrucks = trucks.ToList().Where(t => t.IsCarBooked == false).ToList();

            return allAvailableTrucks;
        }

        public List<Truck> GetAllTrucksWithSuitableCapacity(int minCapacity)
        {
            var trucks = _autoRepairDb.Trucks.Where(t => t.TrunkSize >= minCapacity).ToList();
            return trucks;
        }

        public List<Limousine> GetAllLimousines()
        {
            return _autoRepairDb.Limousines.ToList();
        }

        public List<Car> GetAllAvailableLimousines()
        {
            var trucks = _autoRepairDb.Cars.Join(_autoRepairDb.Limousines, c => c.Id, t => t.Id,
                (c, t) => new Car(c.Id, c.Brand, c.Color, c.Year, c.IsCarBooked));

            var allAvailableTrucks = trucks.ToList().Where(t => t.IsCarBooked == false).ToList();

            return allAvailableTrucks;
        }

        public List<Limousine> GetAllLimousinesWithBar(bool isThereBar)
        {
            var limousines = GetAllLimousines().Where(l => l.Bar == isThereBar).ToList();
            return limousines;
        }

        public List<Car> GetAllBookedCars()
        {
            var cars = _autoRepairDb.Cars.Where(c => c.IsCarBooked).ToList();
            return cars;
        }

        public List<Car> GetAllCars()
        {
            return _autoRepairDb.Cars.ToList();
        }

        public Guid GetCar(Guid carId)
        {
            var car = _autoRepairDb.Cars.Single(c => c.Id == carId);
            return car.Id;
        }

        public Guid AddCar(Brand brand, Color color, DateTime year)
        {
            var car = new Car(brand, color, year);
            _autoRepairDb.Cars.Add(car);
            _autoRepairDb.SaveChanges();
            return car.Id;
        }

        public Guid AddCar(Brand brand, Color color, DateTime year, int width, int depth, int height)
        {
            var truck = new Truck(brand, color, year, width, depth, height);
            _autoRepairDb.Cars.Add(truck);
            _autoRepairDb.SaveChanges();
            return truck.Id;
        }

        public Guid AddCar(Brand brand, Color color, DateTime year, bool bar)
        {
            var limousine = new Limousine(brand, color, year, bar);
            _autoRepairDb.Cars.Add(limousine);
            _autoRepairDb.SaveChanges();
            return limousine.Id;
        }

        public bool DeleteCar(Guid carId)
        {
            var isCarHere = _autoRepairDb.Cars.Any(c => c.Id == carId);

            if (isCarHere)
            {
                var car = _autoRepairDb.Cars.Single(c => c.Id == carId);
                _autoRepairDb.Remove(car);
                _autoRepairDb.SaveChanges();
                return isCarHere;
            }

            return isCarHere;
        }

        public bool ChangeStatusBooking(Guid carId, bool bookingStatus)
        {
            var carBooked = _autoRepairDb.Cars.Single(c => c.Id == carId);
            carBooked.SetBookingStatus(bookingStatus);
            _autoRepairDb.Update(carBooked);
            _autoRepairDb.SaveChanges();
            return true;
        }
    }
}
