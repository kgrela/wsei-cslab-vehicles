using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vehicles
{
    public abstract class Vehicle
    {
        public enum State { Moving, Staying }

        protected double MovingSpeed;
        protected State s = State.Staying;
        protected Environments currentEnvironment;
        public Environments currentEnv => currentEnvironment;
        public double currentSpeed => MovingSpeed;
        public Units currentUnit
        {
            get
            {
                switch (currentEnv)
                {
                    case Environments.Air:
                        return Units.MpS;
                    case Environments.Ground:
                        return Units.KMpH;
                    case Environments.Water:
                        return Units.Knots;
                    default:
                        return Units.KMpH;
                }
            }
        }
        protected List<Environments> availableEnv = new List<Environments>();
        public static double ChangeUnit(double speed, Units from, Units to)
        {
            double val = 0;
            if (from == to)
                return speed;
            switch (from)
            {
                case Units.KMpH:
                    if (to == Units.MpS)
                        val = speed * 0.277;
                    else
                        val = speed * 0.539;
                    break;
                case Units.MpS:
                    if (to == Units.KMpH)
                        val = speed * 3.6;
                    else
                        val = speed * 1.943;
                    break;
                case Units.Knots:
                    if (to == Units.KMpH)
                        val = speed * 1.852;
                    else
                        val = speed * 0.514;
                    break;
            }
            return Math.Round(val, 2, MidpointRounding.AwayFromZero);
        }
        public override string ToString()
        {
            int min = 0;
            int max = 0;
            Units? unit = null;
            switch (currentEnv)
            {
                case Environments.Ground:
                    min = Ground.MinSpeed;
                    max = Ground.MaxSpeed;
                    unit = Units.KMpH;
                    break;
                case Environments.Air:
                    min = Air.MinSpeed;
                    max = Air.MaxSpeed;
                    unit = Units.MpS;
                    break;
                case Environments.Water:
                    min = Water.MinSpeed;
                    max = Water.MaxSpeed;
                    unit = Units.Knots;
                    break;
            }
            string temp = string.Join(", ", availableEnv);
            return $"- env: {currentEnv}, state: {s}, speed: {MovingSpeed}{unit}, available envs: {temp}, speed range in current env: {min} - {max} {unit}";
        }
    }
}
