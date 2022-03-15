using System.Collections.Generic;
using System.Linq;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private List<IAstronaut> list;
        public AstronautRepository()
        {
            list = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => list;
        public void Add(IAstronaut model) => list.Add(model);

        public bool Remove(IAstronaut model) => list.Remove(model);

        public IAstronaut FindByName(string name) => list.FirstOrDefault(x => x.Name == name);
    }
}