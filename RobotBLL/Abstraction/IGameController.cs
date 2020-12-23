using RobotBLL.Implementation.CargoModels;
using RobotBLL.Implementation.Enums;
using RobotBLL.Implementation.Models;
using RobotBLL.Implementation.States;

namespace RobotBLL.Abstraction
{
    public interface IGameController
    {
        void CreateGameState(GameStateOptions options);
        void CreatePlayerState(int number, string name);
        GameState GetGameState();
        PlayerState GetPlayerState();
        void Move(MoveParameter moveParameter);
        void UndoMove();
        void PickCargo();
        void PickUndo();
        bool CheckEndGame();
        Cargo GetCurrentCellCargo();    
    }
}
