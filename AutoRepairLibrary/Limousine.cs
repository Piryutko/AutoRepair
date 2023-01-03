using System;

namespace AutoRepairLibrary
{
    public class Limousine : Car
    {
        public Limousine(Brand brand, Color color, DateTime year, bool bar) : base(brand, color, year)
        {
            Bar = bar;
        }

        public bool Bar { get; }
    }
}
