using System;
using System.Collections.Generic;

namespace AutoRepairLibrary
{
    interface IUserStorage
    {
        Guid AddUser(string name, int age);

        void DeleteUser(Guid userId);

        List<User> GetAllUsers();
    }
}
