using System;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;
        public Driver(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Name {value} cannot be less than 5 symbols.");
                }
                name = value;
            }
        }
        public ICar Car { get; private set; }
        public int NumberOfWins { get; private set; }

        public bool CanParticipate => this.Car != null;
        public void WinRace()
        {
            this.NumberOfWins++;
        }

        public void AddCar(ICar car)
        {
            Car = car ?? throw new ArgumentNullException(nameof(car),"Car cannot be null.");
        }
    }
}