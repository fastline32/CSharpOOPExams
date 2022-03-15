using System.Collections.Generic;
using System.Linq;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            IAstronaut astronaut = astronauts.FirstOrDefault(x => x.CanBreath);
            while (true)
            {
                string item = planet.Items.FirstOrDefault();
                if (astronaut == null)
                {
                    break;
                }
                if (item == null)
                {
                    break;
                }
                astronaut.Bag.Items.Add(item);
                astronaut.Breath();
                planet.Items.Remove(item);
            }
        }
    }
}