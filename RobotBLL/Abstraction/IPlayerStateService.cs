
namespace RobotBLL.Abstraction
{
    public interface IPlayerStateService
    {
        void reduceBatteryCharge(int percents);
        int GetBatteryCharge();
        void SaveState();
        void RestoreState();
        int GetDecodingProbability();
    }
}
