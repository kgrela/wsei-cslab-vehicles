using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vehicles.Interfaces;

namespace vehicles
{
    public class Bicycle : Vehicle, IVehicle, IDriveable
    {
        private readonly int w;
        private Move m;
        public string Name => GetType().Name;
        public int Wheels => w;

        public Bicycle()
        {
            w = 2;
            m = new Move(true, Wheels);
        }

        public void Accelerate(double targetSpeed)
        {
            m.TryToAccelerate(currentEnv, ref _state, ref MovingSpeed, targetSpeed, Name);
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
            return $"{Name}" + base.ToString() + $" wheels: {Wheels}";
        }
    }
}
