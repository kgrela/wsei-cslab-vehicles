using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vehicles.Interfaces;

namespace vehicles
{
    public class Boat : EngineVehicle, IVehicle, ISailable
    {
        private readonly int b;
        private Move m;
        public string Name => GetType().Name;
        public int Buoyancy => b;

        public Boat(int horsePower, int buoyancy) : base(horsePower, FuelType.Diesel)
        {
            currentEnv = Environments.Water;
            availableEnv.Add(Environments.Water);
            b = buoyancy;
            m = new Move(buoyancy, true);
        }

        public void Accelerate(double targetSpeed)
        {
            m.TryToAccelerate(currentEnv, ref _state, ref MovingSpeed, targetSpeed, Name);
        }

        public void LeaveWater()
        {
            Console.WriteLine($"{Name} can't leave water");
        }

        public void Sail()
        {
            Console.WriteLine($"{Name} is already sailing");
        }

        public void SlowDown(double targetSpeed)
        {
            m.TryToSlowDown(currentEnv, ref _state, ref MovingSpeed, targetSpeed, Name);
        }

        public void StopVehicle()
        {
            m.StopMoving(ref _state, currentEnv, ref MovingSpeed, Name);
        }

        public override string ToString()
        {
            return $"{Name}" + base.ToString() + $"\nBuoyancy: {Buoyancy}\n";
        }
    }
}
