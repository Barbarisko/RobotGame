using RobotBLL.Implementation.Models;
using RobotBLL.Implementation.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using RobotBLL.Implementation.Enums;

namespace RobotTests
{
    public class GameServiceTest
    {
        [Fact]
        public void CreateGameState_Test()//
        {
            //Arrange
            var options = new GameStateOptions(5, 5, 5, 0, 10, 10, false);
            GameService service = new GameService();

            //Act
            var gameState = service.CreateGameState(options);

            //Assert
            Assert.NotNull(gameState);
            var cargosResult = gameState.GameField.Where(c => c.CurrentState == CellState.Cargo 
                                                        || c.CurrentState == CellState.RobotCargo)
                                                        .Count();//too long parameter list
            Assert.Equal(5, cargosResult);
        }
    }
}
