using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vehicles
{
    public class Water
    {
        readonly int b;

        public static int MaxSpeed = 40;
        public static int MinSpeed = 0;
        public static Units SpeedUnit = Units.Knots;

        internal Water(int buoyancy)
        {
            b = buoyancy;
        }

        public int Buoyancy => b;
    }
}
