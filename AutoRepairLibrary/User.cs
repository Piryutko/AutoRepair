using EnsureThat;
using System;

namespace AutoRepairLibrary
{
    public class User
    {
        public User(string name, int age)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();
            Ensure.That(age).IsGt(18);

            Age = age;
            Name = name;
            Id = Guid.NewGuid();
        }

        public string Name { get; }

        public int Age { get; }

        public Guid Id { get; }
    }
}
