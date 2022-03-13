using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorationRepository;
        private List<IAquarium> aquariumList;
        public Controller()
        {
            decorationRepository = new DecorationRepository();
            aquariumList = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType != "FreshwaterAquarium" && aquariumType != "SaltwaterAquarium")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }
            IAquarium aquarium;
            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            aquariumList.Add(aquarium);
            return String.Format(OutputMessages.SuccessfullyAdded,aquarium.GetType().Name);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = null;
            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else if(decorationType == "Plant")
            {
                decoration = new Plant();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }
            decorationRepository.Add(decoration);
            return String.Format(OutputMessages.SuccessfullyAdded,decorationType);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = decorationRepository.FindByType(decorationType);
            if (decoration == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentDecoration,
                    decorationType));
            }
            IAquarium aquarium = aquariumList.FirstOrDefault(x => x.Name == aquariumName);
            decorationRepository.Remove(decoration);
            aquarium.AddDecoration(decoration);
            return String.Format(OutputMessages.EntityAddedToAquarium,decorationType,aquariumName);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IAquarium aquarium = aquariumList.FirstOrDefault(x => x.Name == aquariumName);
            IFish fish = null;
            if (fishType != "FreshwaterFish" && fishType != "SaltwaterFish")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }
            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            
            if (aquarium.GetType().Name == "FreshwaterAquarium" && fish.GetType().Name == "FreshwaterFish")
            {
                aquarium.Fish.Add(fish);
            }
            else if (aquarium.GetType().Name == "SaltwaterAquarium" && fish.GetType().Name == "SaltwaterFish")
            {
                aquarium.Fish.Add(fish);
            }
            else
            {
                return OutputMessages.UnsuitableWater;
            }
            return String.Format(OutputMessages.EntityAddedToAquarium,fishType,aquariumName);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariumList.First(x => x.Name == aquariumName);
            aquarium.Feed();
            return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariumList.First(x => x.Name == aquariumName);
            var price = aquarium.Decorations.Sum(x => x.Price) + aquarium.Fish.Sum(x => x.Price);
            return String.Format(OutputMessages.AquariumValue,aquariumName,price);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var aquarium in aquariumList)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}