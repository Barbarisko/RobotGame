using System;
using Xunit;
using FluentAssertions;
using AutoFixture;
using RobotBLL.Implementation.FieldModels;
using RobotBLL.Implementation.Enums;

namespace RobotTests
{
    public class FieldTest
    {
        [Fact]
        public void DeepCloneTest()
        {
            //Arrange    
            var fixture = new Fixture();
            var array = fixture.Create<Cell[,]>();
            foreach (Cell c in array) c.CurrentState = CellState.Cargo;
            var field = new Field(array.GetLength(0), array.GetLength(1)) { Cells = array };

            //Act
            var result = field.DeepClone();

            //Assert
            result.Should().BeEquivalentTo(field);
        }

        [Fact]
        public void IndexerOutOfRangeTest()
        {
            //Arrange
            var field = new Field(5, 5);

            //Assert
            field.Invoking(f => f[(6, 6)]).Should().Throw<IndexOutOfRangeException>();
        }
    }

    public class CellsInFieldTests
    {
        Fixture fixture;
        Field field;
        public CellsInFieldTests()
        {
            fixture = new Fixture();
            field = fixture.Create<Field>();

        }
        public (int, int) GetCellCoordinates(Cell cell, Cell[,] cells)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if (cells[i, j].Equals(cell)) return (i, j);
                }
            }
            return (-1, -1);
        }

        [Fact]
        public void CheckCellCoordinatesIndexer()
        {
            //Arrange
            var coordinates = (0, 0);

            //Act
            var result = GetCellCoordinates(field[coordinates], field.Cells);

            //Assert
            Assert.Equal(coordinates, result);
        }

        [Fact]
        public void GetCellCoordinatesTest()
        {
            //Act
            var cell = field[(0, 0)];
            var coordinates = GetCellCoordinates(cell, field.Cells);

            //Assert
            Assert.Equal((0, 0), coordinates);
        }

        [Fact]
        public void NoCellTest()
        {
            //Arrange
            var cell = new Cell();

            //Act
            var coordinates = GetCellCoordinates(cell, field.Cells);

            //Assert
            Assert.Equal((-1, -1), coordinates);
        }
    }
}
