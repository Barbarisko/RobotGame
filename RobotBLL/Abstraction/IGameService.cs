using RobotBLL.Implementation.Models;
using RobotBLL.Implementation.States;

namespace RobotBLL.Abstraction
{
    public interface IGameService
    {
        GameState CreateGameState(GameStateOptions options);
    }
}
