using System;
using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        private string make;
        private string model;
        private string vin;
        private int horsePower;
        private double fuelAvailable;
        private double fuelConsumptionPerRace;
        public Car(string make, string model, string VIN, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            Make = make;
            Model = model;
            this.VIN = VIN;
            HorsePower = horsePower;
            FuelAvailable = fuelAvailable;
            FuelConsumptionPerRace = fuelConsumptionPerRace;
        }

        public string Make
        {
            get => make;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car make cannot be null or empty.");
                }

                make = value;
            }

        }
        public string Model
        {
            get => this.model;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car model cannot be null or empty.");
                }

                this.model = value;
            }

        }

        public string VIN
        {
            get => vin;
            set
            {
                if (value.Length != 17)
                {
                    throw new ArgumentException("Car VIN must be exactly 17 characters long.");
                }
                vin = value;
            }
        }

        public int HorsePower
        {
            get => horsePower;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Horse power cannot be below 0.");
                }
                horsePower = value;
            }
        }

        public double FuelAvailable
        {
            get => fuelAvailable;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                fuelAvailable = value;
            }
        }

        public double FuelConsumptionPerRace
        {
            get => fuelConsumptionPerRace;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fuel consumption cannot be below 0.");
                }

                fuelConsumptionPerRace = value;
            }
        }
        public virtual void Drive()
        {
            fuelAvailable -= fuelConsumptionPerRace;
        }
    }
}