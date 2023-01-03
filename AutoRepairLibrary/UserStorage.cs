﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoRepairLibrary
{
    public class UserStorage : IUserStorage
    {
        public UserStorage()
        {
            Users = new List<User>();
        }

        public List<User> Users { get; }

        public Guid AddUser(string name, int age)
        {
            var user = new User(name, age);

            Users.Add(user);
            return user.Id;
        }

        public void DeleteUser(Guid userId)
        {
            var user = GetAllUsers().FirstOrDefault(u => u.Id == userId);
            Users.Remove(user);
        }

        public List<User> GetAllUsers()
        {
            return Users;
        }

    }
}
