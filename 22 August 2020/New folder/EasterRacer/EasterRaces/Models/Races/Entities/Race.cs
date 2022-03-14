using System;
using System.Collections.Generic;
using System.Linq;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private string name;
        private int laps;
        private List<IDriver> drivers;
        public Race(string name,int laps)
        {
            Name = name;
            Laps = laps;
            this.drivers = new List<IDriver>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Name {name} cannot be less than 5 symbols.");
                }
                name = value;
            }

        }

        public int Laps
        {
            get => laps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Laps cannot be less than 1.");
                }
            }
        }
        public IReadOnlyCollection<IDriver> Drivers => drivers.AsReadOnly();
        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver),"Driver cannot be null.");
            }

            if (driver.CanParticipate == false)
            {
                throw new ArgumentException($"Driver {driver.Name} could not participate in race.");
            }

            if (drivers.Any(x => x.Name == driver.Name))
            {
                throw new ArgumentNullException(nameof(driver), $"Driver {driver.Name} is already added in {this.Name} race.");
            }
            this.drivers.Add(driver);
        }
    }
}