using System;
using NUnit.Framework;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestOne()
        {
            Assert.Pass();
        }

        [Test]
        public void CheckCar()
        {
            UnitCar car = new UnitCar("Alfa", 200, 2000);
            Assert.AreEqual(car.Model,"Alfa");
            Assert.AreEqual(car.HorsePower, 200);
            Assert.AreEqual(car.CubicCentimeters, 2000);
        }

        [Test]
        public void CheckDriver()
        {
            UnitCar car = new UnitCar("alfa", 200, 200);
            UnitDriver driver = new UnitDriver("pesho", car);
            Assert.AreEqual(driver.Name,"pesho");
        }

        [Test]
        public void CheckDriverForNullName()
        {
            UnitCar car = new UnitCar("alfa", 200, 200);
            Assert.Throws<ArgumentNullException>(() => new UnitDriver(null, car));
        }

        [Test]
        public void CheckConstructorAndCount()
        {
            RaceEntry race = new RaceEntry();
            Assert.AreEqual(race.Counter,0);
        }

        [Test]
        public void CheckAddDriver()
        {
            RaceEntry race = new RaceEntry();
            UnitCar car = new UnitCar("alfa", 200, 200);
            UnitDriver driver = new UnitDriver("pesho", car);
            race.AddDriver(driver);
            Assert.AreEqual(race.Counter,1);

        }
        
        [Test]
        public void CheckAddDriverForSameName()
        {
            RaceEntry race = new RaceEntry();
            UnitCar car = new UnitCar("alfa", 200, 200);
            UnitDriver driver = new UnitDriver("pesho", car);
            race.AddDriver(driver);
            Assert.Throws<InvalidOperationException>(() => race.AddDriver(new UnitDriver("pesho", car)));

        }

        [Test]
        public void CheckParticipantcLessThanMin()
        {
            RaceEntry race = new RaceEntry();
            UnitCar car = new UnitCar("alfa", 200, 200);
            UnitDriver driver = new UnitDriver("pesho", car);
            Assert.Throws<InvalidOperationException>(() => race.CalculateAverageHorsePower());
        }

        [Test]
        public void CheckAverage()
        {
            RaceEntry race = new RaceEntry();
            UnitCar car = new UnitCar("alfa", 200, 200);
            UnitDriver driver = new UnitDriver("pesho", car);
            race.AddDriver(driver);
            UnitCar car1 = new UnitCar("alfa", 300, 200);
            UnitDriver driver1 = new UnitDriver("pesho1", car1);
            race.AddDriver(driver1);
            Assert.AreEqual(race.CalculateAverageHorsePower(),250);
        }

        [Test]
        public void CheckOutPut()
        {
            RaceEntry race = new RaceEntry();
            UnitCar car = new UnitCar("alfa", 200, 200);
            UnitDriver driver = new UnitDriver("pesho", car);
            string expected = "Driver pesho added in race.";
            Assert.AreEqual(race.AddDriver(driver),expected);
        }

        [Test]
        public void CheckCarGetter()
        {
            UnitCar car = new UnitCar("alfa", 200, 200);
            UnitDriver driver = new UnitDriver("pesho", car);
            Assert.AreEqual(driver.Car.Model,car.Model);
        }

        [Test]
        public void CheckForNullDriver()
        {
            RaceEntry race = new RaceEntry();
            Assert.Throws<InvalidOperationException>(() => race.AddDriver(null));
        }
    }
}