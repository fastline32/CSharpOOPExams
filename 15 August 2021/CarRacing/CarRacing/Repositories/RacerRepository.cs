using System;
using System.Collections.Generic;
using System.Linq;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private List<IRacer> racers;

        public RacerRepository()
        {
            racers = new List<IRacer>();
        }

        public IReadOnlyCollection<IRacer> Models => racers;
        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException("Cannot add null in Racer Repository");
            }
            racers.Add(model);
        }

        public bool Remove(IRacer model) => racers.Remove(model);

        public IRacer FindBy(string property) => racers.FirstOrDefault(x => x.Username == property);
    }
}