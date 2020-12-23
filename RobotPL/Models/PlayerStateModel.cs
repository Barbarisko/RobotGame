
namespace RobotPL.Models
{
    class PlayerStateModel
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public PlayerStateModel(int num, string name)
        {
            Number = num;
            Name = name;
        }
    }
}
