using RobotBLL.Implementation.Memento;
using RobotBLL.Implementation.RobotModels;
using RobotBLL.Implementation.States;

namespace RobotBLL.Abstraction
{
    public interface IPlayerService
    {
        PlayerState CreatePlayerState(RobotModel model, GameHistory history);
    }
}
