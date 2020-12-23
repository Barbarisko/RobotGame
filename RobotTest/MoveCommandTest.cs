using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Moq;
using RobotBLL.Abstraction;
using RobotBLL.Exceptions;
using RobotBLL.Implementation.Commands;
using RobotBLL.Implementation.Enums;
using Xunit;

namespace RobotTests
{
    public class MoveCommandTest
    {
        [Theory]
        [ClassData(typeof(MoveTestData))]
        public void CanMoveTest((int, int) coordinates, MoveParameter parameter)
        {
            //Arrange
            var mock = new Mock<IGameStateService>();
            mock.Setup(s => s.GetRobotCoordinates()).Returns(coordinates);
            mock.Setup(s => s.GetFieldDimension()).Returns((3, 3));
            var moveCommand = new MoveCommand(mock.Object, null, parameter);

            //Act


            //Assert
            Assert.Throws<MoveException>(() => moveCommand.Execute());//verify the text  of exception?
        }

        public class MoveTestData: IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { (0, 0), MoveParameter.Up };
                yield return new object[] { (2, 0), MoveParameter.Down };
                yield return new object[] { (0, 0), MoveParameter.Left };
                yield return new object[] { (0, 2), MoveParameter.Right };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
