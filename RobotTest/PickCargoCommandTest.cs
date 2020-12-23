using Moq;
using RobotBLL.Abstraction;
using RobotBLL.Exceptions;
using RobotBLL.Implementation.CargoModels;
using RobotBLL.Implementation.Commands;
using RobotBLL.Implementation.Enums;
using RobotBLL.Implementation.FieldModels;
using RobotBLL.Implementation.Services;
using RobotBLL.Implementation.States;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotTests
{
    public class PickCargoCommandTest
    {
        Mock<IGameStateService> mock;
        (int, int) robotCoordinates;
        public PickCargoCommandTest()
        {
            mock = new Mock<IGameStateService>();//repeating stuff
            robotCoordinates = (0, 0);
        }
        [Fact]
        public void NoCargoInCellTest()
        {
            mock.Setup(s => s.GetRobotCoordinates()).Returns(robotCoordinates);
            mock.Setup(s => s.GetCell(robotCoordinates)).Returns(new Cell());
            var pickCommand = new PickCargoCommand(mock.Object, null);

            //Assert
            Assert.Throws<PickCargoException>(() => pickCommand.Execute());
        }
        [Fact]
        public void NoCargoInCellTestnew()
        {
            //Arrange
            mock.Setup(s => s.GetRobotCoordinates()).Returns(robotCoordinates);
            mock.Setup(s => s.GetCell(robotCoordinates)).Returns(new Cell());
            var pickCommand = new PickCargoCommand(mock.Object, null);

            //Assert
            Assert.Throws<PickCargoException>(() => pickCommand.Execute());
        }

        [Fact]
        public void PickNotDecodingCargoTest()
        {
            //Arrange
            var stub = new PlayerStateServiceStub();
            var cell = new Cell()
            {
                CurrentState = CellState.RobotCargo,
                Cargo = new Cargo(0, 0, false)
            };
            mock.Setup(s => s.GetRobotCoordinates()).Returns(robotCoordinates);
            mock.Setup(s => s.GetCell(robotCoordinates)).Returns(cell);
            var pickCommand = new PickCargoCommand(mock.Object, stub);

            //Act
            pickCommand.Execute();

            //Assert
            mock.Verify(s => s.PickCargoUpdateField(robotCoordinates));
        }

        public class PlayerStateServiceStub : IPlayerStateService
        {
            public int GetBatteryCharge()
            {
                return 100;
            }

            public int GetDecodingProbability()
            {
                return 0;
            }
            public void reduceBatteryCharge(int percents) { }      
            public void RestoreState(){ }
            public void SaveState() { }            
        }
    }
}
