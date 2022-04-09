using System;
using NUnit.Framework;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void TestSmartPfoneConstructor()
        {
            Smartphone phone = new Smartphone("Samsung", 100);
            Assert.AreEqual("Samsung",phone.ModelName);
            phone.ModelName = "Toshiba";
            Assert.AreEqual("Toshiba",phone.ModelName);
            phone.CurrentBateryCharge -= 10;
            Assert.AreEqual(90,phone.CurrentBateryCharge);
            Assert.AreEqual(100,phone.MaximumBatteryCharge);
        }

        [Test]
        public void CheckCreateShopMethod()
        {
            Shop shop = new Shop(5);

            Assert.AreEqual(0,shop.Count);
            Assert.AreEqual(5,shop.Capacity);
        }

        [Test]
        public void CheckCreateShopWithInvalidCapacity()
        {
            Assert.Throws<ArgumentException>(() => new Shop(-1));
        }

        [Test]
        public void CheckCreateShopWithInvalidCapacityMessage()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Shop(-1));
            Assert.AreEqual("Invalid capacity.", ex.Message);
        }

        [Test]
        public void CheckCreateMethod()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Shop shop = new Shop(2);
            shop.Add(phone);
            Assert.AreEqual(1,shop.Count);
        }

        [Test]
        public void CheckAddMethodWithExistingPhone()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Toshiba", 100);
            Shop shop = new Shop(2);
            shop.Add(phone);
            Assert.Throws<InvalidOperationException>(() => shop.Add(phone2));
            var ex = Assert.Throws<InvalidOperationException>(() => shop.Add(phone2));
            Assert.AreEqual("The phone model Toshiba already exist.",ex.Message);
        }

        [Test]
        public void CheckAddMethodWithNoCapacity()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Nexia", 100);
            Shop shop = new Shop(1);
            shop.Add(phone);
            Assert.Throws<InvalidOperationException>(() => shop.Add(phone2));
            var ex = Assert.Throws<InvalidOperationException>(() => shop.Add(phone2));
            Assert.AreEqual("The shop is full.",ex.Message);
        }

        [Test]
        public void CheckRemoveMethodWithValidData()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Nexia", 100);
            Shop shop = new Shop(2);
            shop.Add(phone);
            shop.Add(phone2);
            Assert.AreEqual(2,shop.Count);
            shop.Remove("Nexia");
            Assert.AreEqual(1 , shop.Count);
        }

        [Test]
        public void CheckRemoveMethodWithInvalidData()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Nexia", 100);
            Shop shop = new Shop(2);
            shop.Add(phone);
            shop.Add(phone2);
            Assert.Throws<InvalidOperationException>(() => shop.Remove("Lg"));
            Assert.Throws<InvalidOperationException>(() => shop.Remove(null));
            var ex = Assert.Throws<InvalidOperationException>(() => shop.Remove("Lg"));
            Assert.AreEqual("The phone model Lg doesn't exist.",ex.Message);
        }

        [Test]
        public void CheckTestPhoneMethodWithValidData()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Nexia", 100);
            Shop shop = new Shop(2);
            shop.Add(phone);
            shop.Add(phone2);
            shop.TestPhone("Nexia",50);
            Assert.AreEqual(50,phone2.CurrentBateryCharge);
        }

        [Test]
        public void CheckTestPhoneMethodWithNullPhone()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Nexia", 100);
            Shop shop = new Shop(2);
            shop.Add(phone);
            shop.Add(phone2);
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Lg", 20));
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone(null, 20));
            var ex = Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Lg", 20));
            Assert.AreEqual("The phone model Lg doesn't exist.",ex.Message);
        }

        [Test]
        public void CheckTestMethodWithInvalidBatteryStatus()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Nexia", 10);
            Shop shop = new Shop(2);
            shop.Add(phone);
            shop.Add(phone2);
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Nexia", 20));
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone(null, 20));
            var ex = Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Nexia", 20));
            Assert.AreEqual("The phone model Nexia is low on batery.", ex.Message);
        }

        [Test]
        public void CheckChargePhoneWithValidData()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Nexia", 100);
            Shop shop = new Shop(2);
            shop.Add(phone);
            shop.Add(phone2);
            shop.TestPhone("Nexia", 50);
            Assert.AreEqual(50,phone2.CurrentBateryCharge);
            shop.ChargePhone("Nexia");
            Assert.AreEqual(100,phone2.CurrentBateryCharge);
        }

        [Test]
        public void CheckChargeMethodWithInvalidData()
        {
            Smartphone phone = new Smartphone("Toshiba", 100);
            Smartphone phone2 = new Smartphone("Nexia", 100);
            Shop shop = new Shop(2);
            shop.Add(phone);
            shop.Add(phone2);
            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone(null));
            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("Lg"));
            var ex = Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("Lg"));
            Assert.AreEqual("The phone model Lg doesn't exist.",ex.Message);
        }
    }
}