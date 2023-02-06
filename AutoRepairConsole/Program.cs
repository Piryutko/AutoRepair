using AutoRepair.Domain;
using AutoRepair.Storage;

var time = new DateTime(2001, 01, 01, 14, 00, 00, 000);

var carId = Guid.Parse("368447D1-99AD-412C-8C6F-6C698C8527D2");
var userId = Guid.Parse("55AF7E14-13CC-459A-BB91-FFA236F49C92");
var id = Guid.Parse("86E4F13F-3875-4B3F-8F51-247C94BEBEBC");

var from = new DateTime(2001, 01, 01, 12, 00, 00);
var to = new DateTime(2001, 01, 01, 14, 00, 00);

var autoContext = new AutoRepairContext();
var carStorage = new CarStorage();
var userStorage = new UserStorage();
var bookingStorage = new BookingStorage();

bookingStorage.GetCarId(id, out Guid caId);
Console.WriteLine(caId);
