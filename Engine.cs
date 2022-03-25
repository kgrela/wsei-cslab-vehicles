using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vehicles
{
    public class Engine
    {
        private int HorsePower;
        private FuelType Fuel;
        public Engine(int power, FuelType type)
        {
            HorsePower = power;
            Fuel = type;
        }
        public override string ToString() => $"{Fuel} powered engine with power of {HorsePower}hp";
    }
}
