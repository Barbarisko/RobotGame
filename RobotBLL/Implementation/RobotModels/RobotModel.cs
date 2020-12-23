
namespace RobotBLL.Implementation.RobotModels
{
    public class RobotModel
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public RobotModel(int number, string name)
        {
            Number = number;
            Name = name;
        }
    }
}
