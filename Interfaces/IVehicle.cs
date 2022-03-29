using System;
namespace vehicles.Interfaces
{
    public interface IVehicle
    {
        void StopVehicle();
        void Accelerate(double targetSpeed);
        void SlowDown(double targetSpeed);
        string Name { get; }
        Environments actualEnv { get; }
    }
}
