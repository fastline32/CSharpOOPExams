using NUnit.Framework;

namespace Aquariums.Tests
{
    using System;

    [TestFixture]
    public class AquariumsTests
    {
        [Test]
        public void CreateAquariumTest()
        {
            string name = "a";
            int cap = 2;
            Aquarium aquarium = new Aquarium(name, cap);
            Assert.AreEqual(aquarium.Name,name);
            Assert.AreEqual(aquarium.Capacity,cap);
        }

        [Test]
        public void CheckForNullName()
        {

            Assert.Throws<ArgumentNullException>(() => new Aquarium(null, 1));
            Assert.Throws<ArgumentNullException>(() => new Aquarium(String.Empty, 1));
        }

        [Test]
        public void CheckForIncorrectCapacity()
        {
            Assert.Throws<ArgumentException>(() => new Aquarium("a", -1));
        }

        [Test]
        public void CheckForCorrectCount()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            Fish fish = new Fish("a");
            aquarium.Add(fish);
            Assert.AreEqual(aquarium.Count,1);
        }

        [Test]
        public void CheckForFullAquariumInAdd()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            Fish fish = new Fish("a");
            Fish fish2 = new Fish("b");
            aquarium.Add(fish);
            Assert.Throws<InvalidOperationException>(() => aquarium.Add(fish2));
        }

        [Test]
        public void CheckForCorrectFishInRemoveMethod()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            Fish fish = new Fish("a");
            aquarium.Add(fish);
            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish("b"));
        }

        [Test]
        public void CheckForRemoveMethod()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            Fish fish = new Fish("a");
            aquarium.Add(fish);
            aquarium.RemoveFish("a");
            Assert.AreEqual(aquarium.Count,0);
        }

        [Test]
        public void CheckForCorrectFishToSell()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            aquarium.Add(new Fish("a"));
            Fish fish = aquarium.SellFish("a");
            Assert.AreEqual(fish.Name,"a");
            Assert.AreEqual(fish.Available,false);
        }

        [Test]
        public void CheckForNullFishToSell()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            Fish fish = new Fish("a");
            aquarium.Add(fish);
            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish("b"));
        }

        [Test]
        public void Report()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            Fish fish = new Fish("a");
            aquarium.Add(fish);

            string expected = "Fish available at a: a";
            Assert.AreEqual(expected,aquarium.Report());
        }
    }
}
