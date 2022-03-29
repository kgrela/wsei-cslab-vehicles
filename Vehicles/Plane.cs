using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vehicles.Interfaces;

namespace vehicles
{
    public class Plane : EngineVehicle, IVehicle, IDriveable, IFlyable
    {
        private readonly int w;
        private Move m;
        public int Wheels => w;
        public string Name => GetType().Name;
        public Plane(int horsePower, FuelType fuelType) : base(horsePower, fuelType)
        {
            availableEnv.Add(Environments.Ground);
            availableEnv.Add(Environments.Air);
            w = 2;
            m = new Move(true, Wheels, true);
        }

        public void Accelerate(double targetSpeed)
        {
            m.TryToAccelerate(currentEnv, ref _state, ref MovingSpeed, targetSpeed, Name);
        }

        public void SlowDown(double targetSpeed)
        {
            m.TryToSlowDown(currentEnv, ref _state, ref MovingSpeed, targetSpeed, Name);
        }

        public void Fly()
        {
            m.TryToFly(ref currentEnvironment, _state, ref MovingSpeed, Name);
        }

        public void Land()
        {
            m.TryToDrive(ref currentEnvironment, _state, ref MovingSpeed, Name);
        }

        public void StopVehicle()
        {
            m.StopMoving(ref _state, currentEnv, ref MovingSpeed, Name);
        }

        public override string ToString()
        {
            return $"{Name}" + base.ToString() + $" wheels: {Wheels}";
        }
    }
}
