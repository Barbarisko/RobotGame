using RobotBLL.Implementation.RobotModels;

namespace RobotBLL.Implementation.Memento
{
    public class RobotMemento
    {
        Robot robot;
        public int BatteryCharge { get; private set; }
        public RobotMemento(int battery)
        {
            BatteryCharge = battery;
        }
        public void RestoreState()
        {
            robot.BatteryCharge = BatteryCharge;
        }
    }
}
