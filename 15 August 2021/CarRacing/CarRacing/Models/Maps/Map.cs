using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return "Race cannot be completed because both racers are not available!";
            }

            if (!racerOne.IsAvailable())
            {
                return $"{racerTwo.Username} wins the race! {racerOne.Username} was not available to race!";
            }

            if (!racerTwo.IsAvailable())
            {
                return $"{racerOne.Username} wins the race! {racerTwo.Username} was not available to race!";
            }
            racerOne.Race();
            racerTwo.Race();
            double racerOnePoints = racerOne.Car.HorsePower * racerOne.DrivingExperience;
            if (racerOne.RacingBehavior == "strict")
            {
                racerOnePoints *= 1.2;
            }
            else if(racerOne.RacingBehavior == "aggressive")
            {
                racerOnePoints *= 1.1;
            }

            double racerTwoPoints = racerTwo.Car.HorsePower * racerTwo.DrivingExperience;
            if (racerTwo.RacingBehavior == "strict")
            {
                racerTwoPoints *= 1.2;
            }
            else if (racerTwo.RacingBehavior == "aggressive")
            {
                racerTwoPoints *= 1.1;
            }
            IRacer winner;
            if (racerOnePoints > racerTwoPoints)
            {
                winner = racerOne;
            }
            else
            {
                winner = racerTwo;
            }

            return $"{racerOne.Username} has just raced against {racerTwo.Username}! {winner.Username} is the winner!";
        }
    }
}