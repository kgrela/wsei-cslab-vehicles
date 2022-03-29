using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vehicles.Interfaces;

namespace vehicles
{
    public class Amphibian : EngineVehicle, IVehicle, ISailable, IDriveable
    {
        private readonly int w;
        private readonly int b;
        private Move m;
        public Amphibian(int horsePower, int buoyancy) : base(horsePower, FuelType.Diesel)
        {
            availableEnv.Add(Environments.Ground);
            availableEnv.Add(Environments.Water);
            w = 8;
            b = buoyancy;
            m = new Move(true, Wheels, true, Buoyancy);
        }

        public int Buoyancy => b;
        public int Wheels => w;
        public string Name => GetType().Name;

        public void Accelerate(double targetSpeed)
        {
            m.TryToAccelerate(currentEnv, ref s, ref MovingSpeed, targetSpeed, Name);
        }

        public void SlowDown(double targetSpeed)
        {
            m.TryToSlowDown(currentEnv, ref s, ref MovingSpeed, targetSpeed, Name);
        }

        public void LeaveWater()
        {
            m.TryToDrive(ref currentEnvironment, s, ref MovingSpeed, Name);
        }

        public void Sail()
        {
            m.TryToSail(ref currentEnvironment, s, ref MovingSpeed, Name);
        }

        public void StopVehicle()
        {
            m.StopMoving(ref s, currentEnv, ref MovingSpeed, Name);
        }

        public override string ToString()
        {
            return $"{Name}" + base.ToString() + $" wheels: {Wheels} buoyancy: {Buoyancy}";
        }
    }
}
