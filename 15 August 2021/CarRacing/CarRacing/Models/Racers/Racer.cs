using System;
using System.Text;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;
        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
        }

        public string Username
        {
            get => username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username cannot be null or empty.");
                }
                username = value;
            }
        }

        public string RacingBehavior
        {
            get => racingBehavior;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Racing behavior cannot be null or empty.");
                }
                racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get => drivingExperience;
            set
            {
                if (value <= 0 || value > 100)
                {
                    throw new ArgumentException("Racer driving experience must be between 0 and 100.");
                }

                drivingExperience = value;
            }
        }

        public ICar Car
        {
            get => car;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Car cannot be null or empty.");
                }

                car = value;
            }
        }
        public virtual void Race()
        {
            this.car.Drive();
        }

        public bool IsAvailable()
        {
            return this.car.FuelAvailable >= this.car.FuelConsumptionPerRace;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name}: {username}");
            sb.AppendLine($"--Driving behavior: {racingBehavior}");
            sb.AppendLine($"--Driving experience: {drivingExperience}");
            sb.AppendLine($"--Car: {this.Car.Make} {this.Car.Model} ({this.Car.VIN})");
            return sb.ToString().Trim();
        }
    }
}