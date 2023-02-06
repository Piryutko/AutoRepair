using AutoRepair.Domain;

namespace AutoRepair.Storage
{
    public class UserStorage : IUserStorage
    {
        public UserStorage()
        {
            _autoRepairDb = new AutoRepairContext();
        }

        public AutoRepairContext _autoRepairDb { get; }

        public Guid AddUser(string name, int age)
        {
            var user = new User(name, age);
            _autoRepairDb.Users.Add(user);
            _autoRepairDb.SaveChanges();
            return user.Id;
        }

        public void DeleteUser(Guid userId)
        {
            var user = GetAllUsers().FirstOrDefault(u => u.Id == userId);
            _autoRepairDb.Users.Remove(user);
            _autoRepairDb.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _autoRepairDb.Users.ToList();
        }
    }
}
