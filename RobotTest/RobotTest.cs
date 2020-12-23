using RobotBLL.Implementation.Memento;
using RobotBLL.Implementation.RobotModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotTests
{
    public class RobotTest
    {
        [Fact]
        public void SaveStateTest()
        {
            //Arrange
            var robot = new WorkerRobot(null) { BatteryCharge = 30 };

            //Act
            var result = robot.SaveState();

            //Assert
            Assert.Equal(30, result.BatteryCharge);
        }

        [Fact]
        public void RestoreState()//inapropriate naming
        {
            //Arrange
            var robot = new CyborgRobot(null);
            var robotMemento = new RobotMemento(30);

            //Act
            robot.RestoreState(robotMemento);

            //Assert
            Assert.Equal(30, robot.BatteryCharge);
        }
    }
}
