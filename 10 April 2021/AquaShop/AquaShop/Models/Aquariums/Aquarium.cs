using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium :IAquarium
    {
        private string name;
        protected Aquarium(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            this.Decorations = new List<IDecoration>();
            this.Fish = new List<IFish>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                this.name = value;
            }
        }
        public int Capacity { get; }
        public int Comfort => this.Decorations.Sum(x => x.Comfort);
        public ICollection<IDecoration> Decorations { get; }
        public ICollection<IFish> Fish { get; }
        public void AddFish(IFish fish)
        {
            if (this.Fish.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }
            Fish.Add(fish);
        }

        public bool RemoveFish(IFish fish) => Fish.Remove(fish);

        public void AddDecoration(IDecoration decoration) => this.Decorations.Add(decoration);

        public void Feed()
        {
            foreach (var fish in Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");
            sb.AppendLine($"Fishes: {(this.Fish.Any() ? string.Join(",", Fish.Select(x => x.Name)) : "none")}");
            sb.AppendLine($"Decorations: {this.Decorations.Count}");
            sb.AppendLine($"Comfort: {Comfort}");

            return sb.ToString().TrimEnd();
        }
    }
}