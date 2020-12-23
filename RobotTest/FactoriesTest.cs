using RobotBLL.Implementation.Factories;
using RobotBLL.Implementation.RobotModels;
using Xunit;
using FluentAssertions;


namespace RobotTests
{
    public class FactoriesTest
    {
        [Fact]
        public void CreateCyborgRobotTest()
        {
            //Arrange
            var expected = new CyborgRobot(null) { DecodingProbability = 60, Carrying = 15};
            var robotCreator = new CyborgRobotCreator();

            //Act
            var result = robotCreator.CreateRobot(null);

            //Assert
            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public void CreateWorkerRobotTest()
        {
            //Arrange
            var expected = new WorkerRobot(null) { DecodingProbability = 10, Carrying = 20 };
            var robotCreator = new WorkerRobotCreator();

            //Act
            var result = robotCreator.CreateRobot(null);

            //Assert
            expected.Should().BeEquivalentTo(result);
        }

        [Fact]
        public void CreateSmartRobotTest()
        {
            //Arrange
            var expected = new SmartRobot(null) { DecodingProbability = 100, Carrying = 10 };
            var robotCreator = new SmartRobotCreator();

            //Act
            var result = robotCreator.CreateRobot(null);

            //Assert
            expected.Should().BeEquivalentTo(result);
        }
    }
}
