using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vehicles.Interfaces;

namespace vehicles
{
    public class Car : EngineVehicle, IVehicle, IDriveable
    {

        readonly int w;
        private Move m;

        public Car(int horsePower, FuelType fuel) : base(horsePower, fuel)
        {
            availableEnv.Add(Environments.Ground);
            w = 4;
            m = new Move(true, Wheels);
        }

        public string Name => GetType().Name;
        public int Wheels => w;

        public void Accelerate(double targetSpeed)
        {
            m.TryToAccelerate(currentEnv, ref s, ref MovingSpeed, targetSpeed, Name);
        }

        public void SlowDown(double targetSpeed)
        {
            m.TryToSlowDown(currentEnv, ref s, ref MovingSpeed, targetSpeed, Name);
        }

        public void StopVehicle()
        {
            m.StopMoving(ref s, currentEnv, ref MovingSpeed, Name);
        }

        public override string ToString()
        {
            return $"{Name}" + base.ToString() + $" wheels: {Wheels}";
        }

    }
}
