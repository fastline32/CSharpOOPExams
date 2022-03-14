using System.Collections.Generic;
using System.Linq;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        private List<IDriver> drivers;
        public DriverRepository()
        {
            drivers = new List<IDriver>();
        }

        public IDriver GetByName(string name) => drivers.FirstOrDefault(x => x.Name == name);

        public IReadOnlyCollection<IDriver> GetAll() => drivers;

        public void Add(IDriver model)
        {
            drivers.Add(model);
        }

        public bool Remove(IDriver model) => drivers.Remove(model);
    }
}