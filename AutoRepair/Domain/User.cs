using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace AutoRepair.Domain;

public partial class User
{
    public User(string name, int age)
    {
        Name = name;
        Age = age;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }
}
