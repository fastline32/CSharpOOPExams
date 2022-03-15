using System;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Models.Bags.Contracts;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxygen;
        private IBag bag;
        protected Astronaut(string name, double oxygen)
        {
            Name = name;
            Oxygen = oxygen;
            CanBreath = true;
            bag = new Backpack();
        }

        public string Name
        {
            get => name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidAstronautName);
                }
                name = value;
            }

        }

        public double Oxygen
        {
            get => oxygen;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOxygen);
                }
                oxygen = value;
            }
        }
        public bool CanBreath { get; private set; }
        public IBag Bag => bag;
        public virtual void Breath()
        {
            oxygen -= 10;
            if (oxygen <= 0)
            {
                CanBreath = false;
            }
        }
    }
}