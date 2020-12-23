using Moq;
using RobotBLL.Implementation.Memento;
using RobotBLL.Implementation.Services;
using RobotBLL.Implementation.States;
using RobotBLL.Implementation.RobotModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace RobotTests
{
    public class PlayerStateServiceTest
    {
        [Theory]
        [InlineData(10)]
        [InlineData(110)]
        public void ReduceBatteryChargeTest(int charge)
        {
            //Arrange
            var playerState = new PlayerState(new RobotStub(null), null);
            var playerStateService = new PlayerStateService(playerState);

            //Act
            var expected = playerStateService.GetBatteryCharge() - charge;
            playerStateService.reduceBatteryCharge(charge);
            var result = playerStateService.GetBatteryCharge();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SaveStateTest()
        {
            //Arrange
            var playerState = new PlayerState(new RobotStub(null), new GameHistory());
            var playerStateService = new PlayerStateService(playerState);

            //Act
            playerStateService.SaveState();

            //Assert
            Assert.NotEmpty(playerState.History.History);

            Assert.Equal(playerState.GameRobot.BatteryCharge, playerState.History.History.Peek().BatteryCharge);
        }

        [Fact]
        public void RestoreStateTest()
        {
            //Arrange
            var gameHistory = new GameHistory();
            gameHistory.History.Push(new RobotMemento(100));
            var playerState = new PlayerState(new RobotStub(null), gameHistory);
            var playerStateService = new PlayerStateService(playerState);

            //Act
            playerStateService.RestoreState();
            var result = playerStateService.GetBatteryCharge();

            //Assert
            Assert.Equal(100, result);
        }

        internal class RobotStub : Robot
        {
            public int BatteryCharge { get; set; } = 100;

            public RobotStub(RobotModel model)
                :base(model)
            {

            }
        }
    }
}
