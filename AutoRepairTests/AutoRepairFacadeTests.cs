using AutoRepairLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AutoRepairTests
{
    [TestClass]
    public class AutoRepairFacadeTests
    {
        private AutoRepairFacade _autoRepairFacade;

        private Guid _userId;
        private DateTime _from;
        private DateTime _to;


        [TestInitialize]
        public void Initialize()
        {
            _autoRepairFacade = new AutoRepairFacade(new CarStorage(), new BookingStorage(), new UserStorage());
            _userId = _autoRepairFacade.CreateUser("Васька", 25);

            _from = new DateTime(2022, 10, 24, 12, 00, 00);
            _to = new DateTime(2022, 10, 24, 15, 00, 00);
        }

        [TestMethod]
        public void AddTruck_AddedTruck()
        {
            //prepare
            var brand = Brand.Volkswagen;
            var color = Color.White;
            var year = new DateTime(2000, 01, 01);
            var width = 100;
            var depth = 50;
            var height = 100;

            //act
            _autoRepairFacade.AddCar(brand, color, year, width, depth, height);

            //validation
            var carCount = 1;
            var cars = _autoRepairFacade.GetAllCars();
            var car = cars.Single();

            Assert.AreEqual(carCount, cars.Count);
            Assert.AreEqual(brand, car.Brand);
            Assert.AreEqual(color, car.Color);
            Assert.AreEqual(year, car.Year);

        }

        [TestMethod]
        public void GetAllTrucks_ReturnTrucks()
        {
            //prepare
            var brand = Brand.Volkswagen;
            var color = Color.White;
            var year = new DateTime(2000, 01, 01);
            var width = 100;
            var depth = 50;
            var height = 100;
            var carId = _autoRepairFacade.AddCar(brand, color, year, width, depth, height);

            //act
            var trucks = _autoRepairFacade.GetAllTrucks();

            //validation
            var trucksCount = 1;
            var truck = trucks.SingleOrDefault();
            var expectedBooking = false;
            var expectedTrunkSize = 5000;

            Assert.AreEqual(trucksCount, trucks.Count);
            Assert.AreEqual(expectedBooking, truck.IsCarBooked);
            Assert.AreEqual(carId, truck.Id);
            Assert.AreEqual(brand, truck.Brand);
            Assert.AreEqual(color, truck.Color);
            Assert.AreEqual(year, truck.Year);
            Assert.AreEqual(expectedTrunkSize, truck.TrunkSize);
            

        }

        [TestMethod]
        public void GetAllTruckWithSuitableCapacity_ReturnSuitableTracks()
        {
            //prepare
            _autoRepairFacade.AddCar(Brand.Volkswagen, Color.Black, new DateTime(2001,01,01),10,5,14);
            var capacity = 7;

            //act
            var trucks = _autoRepairFacade.GetAllTruckWithSuitableCapacity(capacity);

            //validation
            var expectedCount = 1;
            var truck = trucks.Single();

            Assert.AreEqual(expectedCount, trucks.Count);
            Assert.AreEqual(capacity, truck.TrunkSize);

        }

        [TestMethod]
        public void AddLimousine_AddedLimousine()
        {
            //prepare
            var brand = Brand.Mersedes;
            var color = Color.White;
            var year = new DateTime(2000, 01, 01);
            var isThereBar = true;

            //act
            _autoRepairFacade.AddCar(brand, color, year, isThereBar);

            //validation
            var carCount = 1;
            var cars = _autoRepairFacade.GetAllCars();
            var car = cars.Single();

            Assert.AreEqual(carCount, cars.Count);
            Assert.AreEqual(brand, car.Brand);
            Assert.AreEqual(color, car.Color);
            Assert.AreEqual(year, car.Year);

        }

        [TestMethod]
        public void GetAllLimousine_ReturnLimousine()
        {
            //prepare
            var brand = Brand.Volkswagen;
            var color = Color.White;
            var year = new DateTime(2000, 01, 01);
            var thereBar = true;
            var carId = _autoRepairFacade.AddCar(brand, color, year, thereBar);

            //act
            var limousines = _autoRepairFacade.GetAllLimousines();

            //validation
            var limousine = limousines.Single();
            var expectedCount = 1;
            var expectedBooking = false;
            var expectedBar = true;

            Assert.AreEqual(expectedCount, limousines.Count);
            Assert.AreEqual(carId, limousine.Id);
            Assert.AreEqual(expectedBooking, limousine.IsCarBooked);
            Assert.AreEqual(expectedBar, limousine.Bar);
            Assert.AreEqual(brand, limousine.Brand);
            Assert.AreEqual(color, limousine.Color);
            Assert.AreEqual(year, limousine.Year);
        }

        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void GetAllLimousines_ReturnCorrectly(bool isThereBar)
        {
            //prepare
            _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001,01,01), isThereBar);

            //act
            var limousines = _autoRepairFacade.GetAllLimousinesWithBar(isThereBar);

            //validation
            var expectedCount = 1;
            var expectedResult = isThereBar;

            var limousine = limousines.Single ();

            Assert.AreEqual(expectedCount, limousines.Count);
            Assert.AreEqual(expectedResult, limousine.Bar);

        }

        [TestMethod]
        public void BookCar_AddBookedCar()
        {
            // act
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            var isBooked = _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);

            // validation
            var expectedCount = 1;
            var expectedStatus = false;
            var expectedBooking = true;

            Assert.AreEqual(expectedBooking, isBooked);
            Assert.AreEqual(_autoRepairFacade.GetAllBookings().Count, expectedCount);

            var car = _autoRepairFacade.GetAllBookings().Single();

            Assert.AreEqual(_carId, car.CarId);
            Assert.AreEqual(_userId, car.UserId);
            Assert.AreEqual(_from, car.From);
            Assert.AreEqual(_to, car.To);
            Assert.AreEqual(expectedStatus, car.IsOnTheRoad);
        }

        [TestMethod]
        [DataRow(10,13)]
        [DataRow(12,15)]
        [DataRow(14,17)]
        public void BookCar_NotSuitableIntervalReturnFalse(int hourFrom, int hourTo)
        {
            //prepare
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            var secondUserId = _autoRepairFacade.CreateUser("Петька", 20);

            var newTimeFrom = new DateTime(2022, 10, 24, hourFrom, 00, 00);
            var newTimeTo = new DateTime(2022, 10, 24, hourTo, 00, 00);

            _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);

            //act
            var isBookedCar = _autoRepairFacade.BookCar(secondUserId, _carId, newTimeFrom, newTimeTo, out Guid bookedId);

            //validation
            var expectedBooking = false;
            var expectedCarsCount = 1;

            Assert.AreEqual(expectedBooking, isBookedCar);
            Assert.AreEqual(expectedCarsCount, _autoRepairFacade.GetAllBookings().Count);
        }

        [TestMethod]
        [DataRow(9, 10)]
        [DataRow(15, 16)]
        public void BookCar_SuitableIntervalReturnTrue(int hourFrom, int hourTo)
        {
            //prepare
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);

            var bookingTimeFrom = new DateTime(2022, 10, 24, hourFrom, 00, 00);
            var bookingTimeTo = new DateTime(2022, 10, 24, hourTo, 00, 00);

            //act
            var IsBookedCar = _autoRepairFacade.BookCar(_userId, _carId, bookingTimeFrom, bookingTimeTo, out Guid bookedgId);

            //validation
            var bookedCars = _autoRepairFacade.GetAllBookings();
            var expectedCarsCount = 2;
            var expectedBooking = true;

            Assert.AreEqual(expectedCarsCount, bookedCars.Count);
            Assert.AreEqual(expectedBooking, IsBookedCar);
        }

        [TestMethod]
        public void PickUpCar_ReturnTrue()
        {
            //prepare
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);

            //act
            var result = _autoRepairFacade.PickUpCar(bookingId, _from);

            //validation
            var expectedStatusMachine = true;
            var expectedResult = true;
            var car = _autoRepairFacade.GetAllBookings().Single();

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedStatusMachine, car.IsOnTheRoad);
        }

        [TestMethod]
        public void PickUpCar_UnknowUserReturnFalse()
        {
            //prepare
            var newUserId = Guid.NewGuid();
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);

            //act
            var result = _autoRepairFacade.PickUpCar(newUserId, _from);

            //validation
            var expectedStatusMachine = false;
            var expectedResult = false;
            var car = _autoRepairFacade.GetAllBookings().Single();

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedStatusMachine, car.IsOnTheRoad);
        }

        [TestMethod]
        public void PickUpCar_UnknowTimeReturnFalse()
        {
            //prepare
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            var newTime = new DateTime(2022, 10, 24, 13, 00, 00);
            _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);

            //act
            var result = _autoRepairFacade.PickUpCar(_userId, newTime);

            //validation
            var expectedStatusMachine = false;
            var expectedResult = false;
            var car = _autoRepairFacade.GetAllBookings().Single();

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedStatusMachine, car.IsOnTheRoad);
        }

        [TestMethod]
        public void UserReturnCar_ReturnTrue()
        {
            //prepare
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            var userId = _autoRepairFacade.CreateUser("Васька", 25);
            var from = new DateTime(2022, 10, 24, 12, 00, 00);
            var to = new DateTime(2022, 10, 24, 15, 00, 00);

            _autoRepairFacade.BookCar(userId, _carId, from, to, out Guid bookingId);
            _autoRepairFacade.PickUpCar(bookingId, from);

            //act
            var result = _autoRepairFacade.ReturnCar(bookingId, to);

            //validation
            var expectedMachineCount = 0;
            var expectedResult = true;

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedMachineCount, _autoRepairFacade.GetAllBookings().Count);
        }

        [TestMethod]
        public void UserReturnCar_UnknowUserReturnFalse()
        {
            //prepare
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            var newUserId = Guid.NewGuid();
            _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);
            _autoRepairFacade.PickUpCar(bookingId, _from);

            //act
            var result = _autoRepairFacade.ReturnCar(newUserId, _to);

            //validation
            var expectedMachineCount = 1;
            var expectedResult = false;
            var expectedStatusCar = true;
            var car = _autoRepairFacade.GetAllBookings().Single();

            Assert.AreEqual(expectedMachineCount, _autoRepairFacade.GetAllBookings().Count);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedStatusCar, car.IsOnTheRoad);
        }

        [TestMethod]
        public void UserReturnCar_UnknowTimeReturnFalse()
        {
            //prepare
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            var newTime = new DateTime(2022, 10, 24, 13, 00, 00);
            _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);
            _autoRepairFacade.PickUpCar(bookingId, _from);

            //act
            var result = _autoRepairFacade.ReturnCar(_userId, newTime);

            //validation
            var expectedMachineCount = 1;
            var expectedResult = false;
            var expectedStatusCar = true;
            var car = _autoRepairFacade.GetAllBookings().Single();

            Assert.AreEqual(expectedMachineCount, _autoRepairFacade.GetAllBookings().Count);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedStatusCar, car.IsOnTheRoad);
           
        }

        [TestMethod]
        public void GetAllAvailableCars_ReturnCars()
        {
            //prepare
            var carStorage = new CarStorage();
            var rentStorage = new BookingStorage();
            var userStorage = new UserStorage();
            var autoRepairFacade = new AutoRepairFacade(carStorage, rentStorage, userStorage);

            var brand = Brand.Audi;
            var color = Color.Black;
            var year = new DateTime(2001, 01, 01);
            var carId = autoRepairFacade.AddCar(brand, color, year);

            //act
            var cars = autoRepairFacade.GetAllAvailableCar();

            //validation
            var expectedCount = 1;
            var expectedStatus = false;
            var car = autoRepairFacade.GetAllAvailableCar().Single();

            Assert.AreEqual(expectedCount, cars.Count);
            Assert.AreEqual(carId, car.Id);
            Assert.AreEqual(expectedStatus, car.IsCarBooked);
            Assert.AreEqual(color, car.Color);
            Assert.AreEqual(year, car.Year);
        }

        [TestMethod]
        public void GetAllBookedCars_ReturnCars()
        {
            //prepare
            var _carId = _autoRepairFacade.AddCar(Brand.Audi, Color.Black, new DateTime(2001, 01, 01));
            _autoRepairFacade.BookCar(_userId, _carId, _from, _to, out Guid bookingId);

            //act
            var cars = _autoRepairFacade.GetAllBookings();

            //validation
            var expectedCount = 1;
            var car = _autoRepairFacade.GetAllBookings().Single();

            Assert.AreEqual(expectedCount, cars.Count);
            Assert.AreEqual(_carId, car.CarId);
            Assert.AreEqual(_userId, car.UserId);
            Assert.AreEqual(_from, car.From);
            Assert.AreEqual(_to, car.To);
        }
    }
}
