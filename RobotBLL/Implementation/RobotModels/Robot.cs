using RobotBLL.Implementation.Memento;

namespace RobotBLL.Implementation.RobotModels
{
    public abstract class Robot
    {
        public RobotModel Model { get; set; }
        public int BatteryCharge { get; set; } = 100;
        public float Carrying { get; set; }
        public int DecodingProbability { get; set; }

        public Robot(RobotModel model)
        {
            Model = model;
        }

        public RobotMemento SaveState()
        {
            return new RobotMemento(BatteryCharge);
        }

        public void RestoreState(RobotMemento memento)
        {
            this.BatteryCharge = memento.BatteryCharge;
        }
    }
}
