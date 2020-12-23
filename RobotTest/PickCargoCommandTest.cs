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
        [Fact]
        public void NoCargoInCellTest()
        {
            //Arrange
            var mock = new Mock<IGameStateService>();//repeating stuff
            var robotCoordinates = (0, 0);//again
            mock.Setup(s => s.GetRobotCoordinates()).Returns(robotCoordinates);
            mock.Setup(s => s.GetCell(robotCoordinates)).Returns(new Cell());
            var pickCommand = new PickCargoCommand(mock.Object, null);

            //Act

            //Assert
            Assert.Throws<PickCargoException>(() => pickCommand.Execute());
        }

        [Fact]
        public void PickNotDecodingCargoTest()
        {
            //Arrange
            var mock = new Mock<IGameStateService>();
            var stub = new PlayerStateServiceStub();
            var robotCoordinates = (0, 0);
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
