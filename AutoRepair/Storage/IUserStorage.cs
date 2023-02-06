using AutoRepair.Domain;

namespace AutoRepair.Storage
{
    public interface IUserStorage
    {
        Guid AddUser(string name, int age);

        void DeleteUser(Guid userId);

        List<User> GetAllUsers();
    }
}
