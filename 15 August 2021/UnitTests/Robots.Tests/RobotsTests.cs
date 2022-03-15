using NUnit.Framework;

namespace Robots.Tests
{
    using System;

    [TestFixture]
    public class RobotsTests
    {
        [Test]
        public void CheckRobotCreation()
        {
            Robot robot = new Robot("pesho", 50);
            Assert.AreEqual(robot.Name,"pesho");
            Assert.AreEqual(robot.MaximumBattery,50);
            Assert.AreEqual(robot.Battery,50);
        }

        [Test]
        public void CheckChangeNameAndMaxBattery()
        {
            Robot robot = new Robot("pesho", 50);
            robot.Name = "gosho";
            robot.Battery = 150;
            Assert.AreEqual(robot.Name, "gosho");
            Assert.AreEqual(robot.Battery, 150);
        }

        [Test]
        public void CreateRobotManager()
        {
            RobotManager robotManager = new RobotManager(15);
            Assert.AreEqual(robotManager.Capacity, 15);
        }
        [Test]
        public void CreateRobotManagerForWrongCapacity()
        {
            Assert.Throws<ArgumentException>(() => new RobotManager(-1));
        }
        [Test]
        public void CreateRobotManagerCount()
        {
            RobotManager robotManager = new RobotManager(15);
            Assert.AreEqual(robotManager.Count, 0);
        }
        [Test]
        public void CheckAddMethod()
        {
            RobotManager robotManager = new RobotManager(15);
            Robot robot = new Robot("a", 50);
            robotManager.Add(robot);
            Assert.AreEqual(robotManager.Count,1);
        }
        [Test]
        public void CheckAddMethodWithInvalidData()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("a", 50);
            robotManager.Add(robot);
            Robot robot2 = new Robot("b", 50);
            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot2));
        }
        [Test]
        public void CheckAddMethodWithInvalidNameData()
        {
            RobotManager robotManager = new RobotManager(2);
            Robot robot = new Robot("a", 50);
            robotManager.Add(robot);
            Robot robot2 = new Robot("a", 50);
            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot2));
        }
        [Test]
        public void CheckRemoveRobotManager()
        {
            RobotManager robotManager = new RobotManager(15);
            Robot robot = new Robot("a", 50);
            robotManager.Add(robot);
            robotManager.Remove("a");
            Assert.AreEqual(robotManager.Count,0);
        }
        [Test]
        public void CheckRemoveRobotManagerInvalidData()
        {
            RobotManager robotManager = new RobotManager(15);
            Assert.Throws<InvalidOperationException>(() => robotManager.Remove(null));
        }
        [Test]
        public void CheckWorkRobotManagerInvalidDataName()
        {
            RobotManager robotManager = new RobotManager(15);
            Assert.Throws<InvalidOperationException>(() => robotManager.Work(null,"a",15));
        }
        [Test]
        public void CheckWorkRobotManagerInvalidDataBattery()
        {
            RobotManager robotManager = new RobotManager(15);
            Robot robot = new Robot("a", 50);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() => robotManager.Work("a", "a", 60));
        }
        [Test]
        public void CheckWorkRobotManager()
        {
            RobotManager robotManager = new RobotManager(15);
            Robot robot = new Robot("a", 60);
            robotManager.Add(robot);
            robotManager.Work("a", "a", 50);
            Assert.AreEqual(robot.Battery,10);
        }
        [Test]
        public void CheckCharcheRobotManagerInvalidDataBattery()
        {
            RobotManager robotManager = new RobotManager(1);
            Assert.Throws<InvalidOperationException>(() => robotManager.Charge(null));
        }
        [Test]
        public void CheckChargeRobotManager()
        {
            RobotManager robotManager = new RobotManager(15);
            Robot robot = new Robot("a", 60);
            robot.Battery = 40;
            robotManager.Add(robot);
            robotManager.Charge("a");
            Assert.AreEqual(robot.Battery, 60);
        }
    }
}