using System.Collections.Generic;
using System.Linq;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planet;
        public PlanetRepository()
        {
            planet = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => planet;
        public void Add(IPlanet model) => planet.Add(model);

        public bool Remove(IPlanet model) => planet.Remove(model);

        public IPlanet FindByName(string name) => planet.FirstOrDefault(x => x.Name == name);
    }
}