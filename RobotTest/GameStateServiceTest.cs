using RobotBLL.Implementation.Enums;
using RobotBLL.Implementation.FieldModels;
using RobotBLL.Implementation.States;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using RobotBLL.Implementation.Services;
using FluentAssertions;


namespace RobotTests
{
    public class GameStateServiceTest: IClassFixture<GameStateServiceTest.GameStateFixture>
    {
        GameStateFixture fixture;
        GameStateService gameStateService;
        public GameStateServiceTest(GameStateFixture fixture)
        {
            this.fixture = fixture;
            gameStateService = new GameStateService(fixture.state); //repeating data, use Startup

        }

        [Fact]
        public void MoveUpdateField()
        {
            //Act
            var newCoordinates = (1, 0);
            gameStateService.MoveUpdateField(newCoordinates);

            //Assert
            var coord = (0, 0);
            Assert.Equal(CellState.Empty, fixture.state.GameField[coord].CurrentState);
            Assert.Equal(CellState.Robot, fixture.state.GameField[newCoordinates].CurrentState);
        }

        [Fact]
        public void UndoUpdateField()
        {
            //Act
            gameStateService.UndoUpdateField();

            //Assert
            var coordinates = (0, 0);
            Assert.Equal(CellState.Empty, fixture.state.GameField[coordinates].CurrentState);
        }

        public class GameStateFixture
        {
            public GameState state { get; set; }

            Field field = new Field(3, 3);
            
            public GameStateFixture()
            {
                field.Cells = new Cell[3, 3]
                {
                    {new Cell() {CurrentState = CellState.Robot }, new Cell(), new Cell() },
                    {new Cell(), new Cell(), new Cell() },
                    {new Cell(), new Cell(), new Cell() }
                };
                state = new GameState(field, 0);

                var previousField = field.DeepClone();
                previousField.Cells[0, 0].CurrentState = CellState.Empty;
                state.PreviousStates.Push(previousField);
                state.PreviousStates.Push(previousField);
            }
        }
    }
}
