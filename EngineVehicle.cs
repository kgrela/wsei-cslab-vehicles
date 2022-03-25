using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vehicles
{
    public abstract class EngineVehicle : Vehicle
    {
        protected Engine engine;
        public EngineVehicle(int horsePower, FuelType fuel)
        {
            engine = new Engine(horsePower, fuel);
        }
        public string GetEngineInfo() => engine.ToString();
        public override string ToString()
        {
            return base.ToString() + GetEngineInfo();
        }
    }
}
