using System;

namespace AutoRepairLibrary
{
    public class Truck : Car
    {
        public Truck(Brand brand, Color color, DateTime year, double width, double depth, double height) : base(brand, color, year)
        {
            TrunkSize = (width * depth * height) / 100;
        }

        public double TrunkSize { get; }
    }
}
