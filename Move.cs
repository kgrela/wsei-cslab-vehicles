using System;
namespace vehicles
{
    public class Move
    {
        internal Ground Ground;
        internal Air Air;
        internal Water Water;

        internal Move(bool drives, int wheels)
        {
            Ground = new Ground(wheels);
        }

        internal Move(bool drives, int wheels, bool sails, int buoyancy)
        {
            Ground = new Ground(wheels);
            Water = new Water(buoyancy);
        }

        internal Move(bool drives, int wheels, bool flies)
        {
            Ground = new Ground(wheels);
            Air = new Air();
        }

        internal Move(bool flies, bool sails, int buoyancy)
        {
            Water = new Water(buoyancy);
            Air = new Air();
        }

        internal Move(int buoyancy, bool sails)
        {
            Water = new Water(buoyancy);
        }

        internal Move(bool drives, int wheels, bool flies, bool sails, int buoyancy)
        {
            Ground = new Ground(wheels);
            Air = new Air();
            Water = new Water(buoyancy);
        }

        // ---------------------------------------------------------------------------------------------------------------------------- //


        internal void StopMoving(ref Vehicle.State state, Environments currentEnvironment, ref double currentSpeed, string vehicle)
        {
            if (state == Vehicle.State.Staying)
            {
                Console.WriteLine($"{vehicle} isn't moving right now");
                return;
            }
            if (state == Vehicle.State.Moving && currentEnvironment == Environments.Ground || currentEnvironment == Environments.Water)
            {
                currentSpeed = 0;
                state = Vehicle.State.Staying;
                Console.WriteLine($"{vehicle} has been stopped");
                return;
            }
            Console.WriteLine($"Air vehicles can't be stopped in air :o");
        }

        // ---------------------------------------------------------------------------------------------------------------------------- //

        internal void TryToAccelerate(Environments currentEnvironment, ref Vehicle.State state, ref double currentSpeed, double targetSpeed, string vehicle)
        {
            if (currentSpeed == targetSpeed)
            {
                Console.WriteLine($"{vehicle} speed is {currentSpeed}");
                return;
            }
            if (targetSpeed < 0)
            {
                Console.WriteLine($"{vehicle} can't accelerate to {targetSpeed}");
                return;
            }
            if (targetSpeed < currentSpeed || targetSpeed == 0)
            {
                TryToSlowDown(currentEnvironment, ref state, ref currentSpeed, targetSpeed, vehicle);
                return;
            }
            double min = 0;
            double max = 0;
            Units? temp = null;
            state = Vehicle.State.Moving;
            switch (currentEnvironment)
            {
                case Environments.Ground:
                    min = Ground.MinSpeed;
                    max = Ground.MaxSpeed;
                    temp = Units.KMpH;
                    if (targetSpeed <= max)
                    {
                        currentSpeed = targetSpeed;
                        Console.WriteLine($"{vehicle} accelerated to {targetSpeed}{Ground.SpeedUnit}");
                        return;
                    }
                    break;
                case Environments.Air:
                    min = Air.MinSpeed;
                    max = Air.MaxSpeed;
                    temp = Units.MpS;
                    if (targetSpeed <= max)
                    {
                        currentSpeed = targetSpeed;
                        Console.WriteLine($"{vehicle} accelerated to {targetSpeed}{Air.SpeedUnit}");
                        return;
                    }
                    break;
                case Environments.Water:
                    min = Water.MinSpeed;
                    max = Water.MaxSpeed;
                    temp = Units.Knots;
                    if (targetSpeed <= max)
                    {
                        currentSpeed = targetSpeed;
                        Console.WriteLine($"{vehicle} accelerated to {targetSpeed}{Water.SpeedUnit}");
                        return;
                    }
                    break;
            }
            if (currentSpeed == 0)
                state = Vehicle.State.Staying;
            Console.WriteLine($"{targetSpeed}{temp} is out of range for {currentEnvironment} environment, type speed in range {min}-{max}{temp} for {vehicle}");
        }

        // ---------------------------------------------------------------------------------------------------------------------------- //

        internal void TryToSlowDown(Environments currentEnvironment, ref Vehicle.State state, ref double currentSpeed, double targetSpeed, string vehicle)
        {
            if (currentSpeed == targetSpeed)
            {
                Console.WriteLine($"{vehicle} speed is {currentSpeed}");
                return;
            }
            if (targetSpeed < 0)
            {
                Console.WriteLine($"{targetSpeed} is not valid speed for {vehicle}");
                return;
            }
            if (targetSpeed > currentSpeed)
            {
                TryToAccelerate(currentEnvironment, ref state, ref currentSpeed, targetSpeed, vehicle);
                return;
            }
            if (targetSpeed == 0)
            {
                StopMoving(ref state, currentEnvironment, ref currentSpeed, vehicle);
                return;
            }
            double min = 0;
            double max = 0;
            switch (currentEnvironment)
            {
                case Environments.Ground:
                    min = Ground.MinSpeed;
                    max = Ground.MaxSpeed;
                    if (targetSpeed >= min)
                    {
                        currentSpeed = targetSpeed;
                        Console.WriteLine($"{vehicle} slowed down to {targetSpeed}{Ground.SpeedUnit}");
                        return;
                    }
                    break;
                case Environments.Air:
                    min = Air.MinSpeed;
                    max = Air.MaxSpeed;
                    if (targetSpeed >= min)
                    {
                        currentSpeed = targetSpeed;
                        Console.WriteLine($"{vehicle} slowed down to {targetSpeed}{Air.SpeedUnit}");
                        return;
                    }
                    break;
                case Environments.Water:
                    min = Water.MinSpeed;
                    max = Water.MaxSpeed;
                    if (targetSpeed >= min)
                    {
                        currentSpeed = targetSpeed;
                        Console.WriteLine($"{vehicle} slowed down to {targetSpeed}{Water.SpeedUnit}");
                        return;
                    }
                    break;
            }
            Console.Write($"{targetSpeed} is out of range for {currentEnvironment} environment, type speed in range {min}-{max} for {vehicle}");
        }

        // ---------------------------------------------------------------------------------------------------------------------------- //

        internal void TryToDrive(ref Environments currentEnvironment, Vehicle.State state, ref double currentSpeed, string vehicle)
        {
            if (!ValidatingConditions(Ground, state, Environments.Ground, currentEnvironment, vehicle))
                return;
            double convertedSpeed = 0;
            if (currentEnvironment == Environments.Air)
            {
                if (currentSpeed != Air.MinSpeed)
                {
                    Console.WriteLine($"Air vehicles can land only at minimum speed {Air.MinSpeed}{Air.SpeedUnit}, your actual speed is {currentSpeed}{Air.SpeedUnit}, slow down first before landing.");
                    return;
                }
                convertedSpeed = Vehicle.UnitConverter(currentSpeed, Units.MpS, Units.KMpH);
                Console.WriteLine($"Succesfully landed on ground, your actual speed is {convertedSpeed}{Ground.SpeedUnit}");
            }
            if (currentEnvironment == Environments.Water)
            {
                convertedSpeed = Vehicle.UnitConverter(currentSpeed, Units.Knots, Units.KMpH);
                Console.WriteLine($"Succesfully left water and started to drive, your actual speed is {convertedSpeed}{Ground.SpeedUnit}");
            }
            currentSpeed = convertedSpeed;
            currentEnvironment = Environments.Ground;
        }

        // ---------------------------------------------------------------------------------------------------------------------------- //

        internal void TryToSail(ref Environments currentEnvironment, Vehicle.State state, ref double currentSpeed, string vehicle)
        {
            if (!ValidatingConditions(Water, state, Environments.Water, currentEnvironment, vehicle))
                return;
            double convertedSpeed = 0;
            if (currentEnvironment == Environments.Air)
            {
                if (currentSpeed != Air.MinSpeed)
                {
                    Console.WriteLine($"Air vehicles can land only at minimum speed {Air.MinSpeed}{Air.SpeedUnit}, your actual speed is {currentSpeed}{Air.SpeedUnit}, slow down first before landing.");
                    return;
                }
                convertedSpeed = Vehicle.UnitConverter(currentSpeed, Units.MpS, Units.Knots);
                Console.WriteLine($"Succesfully landed on water, your actual speed is {convertedSpeed}{Water.SpeedUnit}");
            }
            if (currentEnvironment == Environments.Ground)
            {
                convertedSpeed = Vehicle.UnitConverter(currentSpeed, Units.KMpH, Units.Knots);
                if (convertedSpeed > Water.MaxSpeed)
                {
                    Console.WriteLine($"Your actual speed {currentSpeed}{Ground.SpeedUnit} = {convertedSpeed}{Water.SpeedUnit} is too fast to sail, slow down at least to {Water.MaxSpeed}{Water.SpeedUnit}");
                    return;
                }
                if (convertedSpeed < Water.MinSpeed)
                {
                    Console.WriteLine($"Your actual speed {currentSpeed}{Ground.SpeedUnit} = {convertedSpeed}{Water.SpeedUnit} is too slow to sail, speed up at least to {Water.MinSpeed}{Water.SpeedUnit}");
                    return;
                }
                Console.WriteLine($"Succesfully started to sail, your actual speed is {convertedSpeed}{Units.Knots}");
            }
            currentSpeed = convertedSpeed;
            currentEnvironment = Environments.Water;
        }

        // ---------------------------------------------------------------------------------------------------------------------------- //

        internal void TryToFly(ref Environments currentEnvironment, Vehicle.State state, ref double currentSpeed, string vehicle)
        {
            if (!ValidatingConditions(Air, state, Environments.Air, currentEnvironment, vehicle))
                return;
            double convertedSpeed = 0;
            if (currentEnvironment == Environments.Ground)
            {
                convertedSpeed = Vehicle.UnitConverter(currentSpeed, Units.KMpH, Units.MpS);
                if (convertedSpeed < Air.MinSpeed)
                {
                    Console.WriteLine($"Your actual speed is {currentSpeed}{Ground.SpeedUnit} = {convertedSpeed}{Air.SpeedUnit}. Minimum speed required to get off ground is {Air.MinSpeed}{Air.SpeedUnit}. Speed up!");
                    return;
                }
            }
            if (currentEnvironment == Environments.Water)
            {
                convertedSpeed = Vehicle.UnitConverter(currentSpeed, Units.Knots, Units.MpS);
                if (convertedSpeed < Air.MinSpeed)
                {
                    Console.WriteLine($"Your actual speed is {currentSpeed}{Water.SpeedUnit} = {convertedSpeed}{Air.SpeedUnit}. Minimum speed required to get off water is {Air.MinSpeed}{Air.SpeedUnit}. Speed up!");
                    return;
                }
            }
            Console.WriteLine($"Succefully started to fly. Your actual speed is {convertedSpeed}{Air.SpeedUnit}");
            currentSpeed = convertedSpeed;
            currentEnvironment = Environments.Air;
        }

        // ---------------------------------------------------------------------------------------------------------------------------- //

        private void NotMovingVehicleMsg(string vehicle) => Console.WriteLine($"{vehicle} is not moving.");
        private bool IsAlreadyInProperEnvironment(Environments target, Environments actual) => target == actual ? true : false;
        private bool IsVehicleStaying(Vehicle.State actual) => actual == Vehicle.State.Staying ? true : false;
        private bool ValidatingConditions(object module, Vehicle.State state, Environments targetEnvironment, Environments currentEnvironment, string vehicle)
        {
            if (module == null)
            {
                Console.WriteLine($"{vehicle} is not able to travel in given environment ({targetEnvironment}).");
                return false;
            }
            if (IsVehicleStaying(state))
            {
                NotMovingVehicleMsg(vehicle);
                return false;
            }
            if (IsAlreadyInProperEnvironment(targetEnvironment, currentEnvironment))
            {
                Console.WriteLine($"{vehicle} is already in target environment.");
                return false;
            }
            return true;
        }
    }
}
