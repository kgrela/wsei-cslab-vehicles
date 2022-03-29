using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vehicles.Interfaces;

namespace vehicles
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // ---------------------------------------------------------------------------------------------------------------------------- //

            List<Vehicle> vehicle = new List<Vehicle>();

            Car audi = new Car(180, FuelType.Petrol);
            Plane dromader = new Plane(800, FuelType.Petrol);
            Boat corsair = new Boat(1000, 10000);
            Bicycle kross = new Bicycle();
            Amphibian darwin = new Amphibian(1000, 15000);

            vehicle.Add(audi);
            vehicle.Add(dromader);
            vehicle.Add(corsair);
            vehicle.Add(kross);
            vehicle.Add(darwin);

            audi.Accelerate(300);
            dromader.Fly();
            corsair.Sail();
            kross.Accelerate(10);
            darwin.Sail();

            foreach (var i in vehicle) Console.WriteLine(i);

            audi.StopVehicle();
            dromader.Accelerate(350);

            foreach (var i in vehicle) Console.WriteLine(i);

            // ---------------------------------------------------------------------------------------------------------------------------- //

            var ground = vehicle.Where(veh => veh.currentEnv == Environments.Ground);
            foreach (var i in ground) Console.WriteLine(i);

            // ---------------------------------------------------------------------------------------------------------------------------- //

            var sortBySpeed = vehicle.OrderBy(veh => Vehicle.ChangeUnit(veh.currentSpeed, veh.currentUnit, Units.KMpH));
            foreach (var i in sortBySpeed) Console.WriteLine(i);

            // ---------------------------------------------------------------------------------------------------------------------------- //

            var sortGroundVehiclesBySpeed = vehicle.Where(veh => veh.currentEnv == Environments.Ground).OrderByDescending(veh => Vehicle.ChangeUnit(veh.currentSpeed, veh.currentUnit, Units.KMpH));
            foreach (var i in sortGroundVehiclesBySpeed) Console.WriteLine(i);

        }
    }
}
