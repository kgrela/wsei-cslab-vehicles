using System;
namespace vehicles.Interfaces
{
    public interface ISailable
    {
        void Sail();
        void LeaveWater();
        int Buoyancy { get; }
    }
}
