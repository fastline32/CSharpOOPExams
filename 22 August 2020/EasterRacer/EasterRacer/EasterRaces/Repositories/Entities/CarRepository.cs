using System.Collections.Generic;
using System.Linq;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private List<ICar> cars;
        public CarRepository()
        {
            cars = new List<ICar>();
        }

        public ICar GetByName(string name) => cars.FirstOrDefault(x => x.Model == name);

        public IReadOnlyCollection<ICar> GetAll() => cars;

        public void Add(ICar model)
        {
            cars.Add(model);
        }

        public bool Remove(ICar model) => cars.Remove(model);
    }
}