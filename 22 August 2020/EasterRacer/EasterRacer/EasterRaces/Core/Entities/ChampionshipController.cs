using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController :IChampionshipController
    {
        private DriverRepository driverRepository;
        private CarRepository carRepository;
        private RaceRepository raceRepository;
        public ChampionshipController()
        {
            driverRepository = new DriverRepository();
            carRepository = new CarRepository();
            raceRepository = new RaceRepository();
        }
        public string CreateDriver(string driverName)
        {
            if (driverRepository.GetByName(driverName) != null)
            {
                throw new ArgumentException(ExceptionMessages.DriversExists,driverName);
            }
            IDriver driver = new Driver(driverName);
            driverRepository.Add(driver);
            return $"Driver {driverName} is created.";
        }
        public string CreateCar(string type, string model, int horsePower)
        {
            ICar car = null;
            if (carRepository.GetByName(model) != null)
            {
                throw new ArgumentException($"Car {model} is already created.");
            }
            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }
            carRepository.Add(car);
            return $"{car.GetType().Name} {car.Model} is created.";
        }

        public string CreateRace(string name, int laps)
        {
            if (raceRepository.GetByName(name) != null)
            {
                throw new InvalidOperationException($"Race {name} is already created.");
            }

            IRace race = new Race(name, laps);
            raceRepository.Add(race);
            return $"Race {race.Name} is created.";
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = raceRepository.GetByName(raceName);
            if (race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            IDriver driver = driverRepository.GetByName(driverName);
            if (driver == null)
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }
            race.AddDriver(driver);
            return $"Driver {driver.Name} added in {race.Name} race.";
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver driver = driverRepository.GetByName(driverName);
            if (driver == null)
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }

            ICar car = carRepository.GetByName(carModel);
            if (car == null)
            {
                throw new InvalidOperationException($"Car {carModel} could not be found.");
            }
            driver.AddCar(car);
            carRepository.Remove(car);
            return $"Driver {driver.Name} received car {car.Model}.";
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.GetByName(raceName);
            if (race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }

            if (race.Drivers.Count < 3)
            {
                throw new InvalidOperationException($"Race {race.Name} cannot start with less than 3 participants.");
            }

            List<IDriver> drivers = race.Drivers.OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps)).Take(3).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Driver {drivers[0].Name} wins {race.Name} race.");
            sb.AppendLine($"Driver {drivers[1].Name} is second in {race.Name} race.");
            sb.AppendLine($"Driver {drivers[2].Name} is third in {race.Name} race.");
            this.raceRepository.Remove(race);
            return sb.ToString().TrimEnd();
        }
    }
}