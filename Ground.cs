using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vehicles
{
    public class Ground
    {
        readonly int w;

        public static int MinSpeed = 0;
        public static int MaxSpeed = 350;
        public static Units SpeedUnit = Units.KMpH;

        internal Ground(int wheelsCount)
        {
            w = wheelsCount;
        }

        public int WheelsCount => w;
    }
}
