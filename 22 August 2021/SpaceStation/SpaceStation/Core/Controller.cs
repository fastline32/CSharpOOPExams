using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;
        private int exporedCount;
        public Controller()
        {
            astronautRepository = new AstronautRepository();
            planetRepository = new PlanetRepository();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            if (type != "Biologist" && type != "Geodesist" && type != "Meteorologist")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            IAstronaut astronaut;
            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else
            {
                astronaut = new Meteorologist(astronautName);
            }
            astronautRepository.Add(astronaut);
            return String.Format(OutputMessages.AstronautAdded,type,astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            planetRepository.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronautRepository.FindByName(astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidRetiredAstronaut,
                    astronautName));
            }

            astronautRepository.Remove(astronaut);
            return String.Format(OutputMessages.AstronautRetired,astronautName);
        }

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> explorers = astronautRepository.Models.Where(a => a.Oxygen > 60).ToList();

            if (explorers.Count == 0)
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet");
            }

            IPlanet planet = this.planetRepository.FindByName(planetName);
            IMission mission = new Mission();
            mission.Explore(planet, explorers);

            exporedCount++;

            IEnumerable<IAstronaut> deadExplorers = explorers.Where(a => a.CanBreath == false);

            return $"Planet: {planetName} was explored! Exploration finished with {deadExplorers.Count()} dead astronauts!";

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{exporedCount} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var astronaut in astronautRepository.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                if (astronaut.Bag.Items.Count == 0)
                {
                    sb.AppendLine("Bag items:  none");
                }
                else
                {
                    sb.AppendLine($"Bag items: {String.Join(",", astronaut.Bag.Items)}");
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}