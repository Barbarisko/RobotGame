using RobotBLL.Abstraction;
using RobotBLL.Implementation.CargoModels;
using RobotBLL.Implementation.Commands;
using RobotBLL.Implementation.Enums;
using RobotBLL.Implementation.Memento;
using RobotBLL.Implementation.Models;
using RobotBLL.Implementation.RobotModels;
using RobotBLL.Implementation.Services;
using RobotBLL.Implementation.States;

namespace RobotBLL.Implementation
{
    public class GameController: IGameController
    {
        GameState gameState;
        PlayerState playerState;
        IGameService gameService;
        IPlayerService playerService;
        ICommandController commandController;
        IGameStateService gameStateService;
        IPlayerStateService playerStateService;
        GameHistory gameHistory;

        public GameController(IGameService gameSevice, IPlayerService playerService, ICommandController commandController)
        {
            this.gameService = gameSevice;
            this.playerService = playerService;
            this.commandController = commandController;
            gameHistory = new GameHistory();
        }

        private void SetMoveCommand(MoveParameter parameter)
        {
            Command moveCommand = new MoveCommand(gameStateService, playerStateService, parameter);
            commandController.SetMoveCommand(moveCommand);
        }

        private void SetPickCommand()
        {
            Command pickCommand = new PickCargoCommand(gameStateService, playerStateService);
            commandController.SetPickCommand(pickCommand);
        }

        public void CreateGameState(GameStateOptions options)
        {
            gameState = gameService.CreateGameState(options);
            gameStateService = new GameStateService(gameState);
        }

        public void CreatePlayerState(int number, string name)
        {
            RobotModel robotModel = new RobotModel(number, name);
            playerState = playerService.CreatePlayerState(robotModel, gameHistory);
            playerStateService = new PlayerStateService(playerState);
        }

        public GameState GetGameState()
        {
            return gameState;
        }

        public PlayerState GetPlayerState()
        {
            return playerState;
        }     

        public void Move(MoveParameter moveParameter)
        {
            SetMoveCommand(moveParameter);
            commandController.Move();
        }

        public void UndoMove()
        {
            commandController.MoveUndo();
        }

        public void PickCargo()
        {
            SetPickCommand();
            commandController.PickCargo();
        }

        public void PickUndo()
        {
            commandController.PickCargoUndo();
        }

        public bool CheckEndGame()
        {
            return gameState.IsEnded;
        }
        public Cargo GetCurrentCellCargo()
        {
            return gameStateService.GetCurrentCellCargo();
        }
    }
}
