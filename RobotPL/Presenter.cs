using RobotBLL.Abstraction;
using RobotPL.Abstract;
using RobotPL.Mappers;

namespace RobotPL
{
    class Presenter
    {
        IView view;
        IGameController gameController;
        FieldMapper mapper;
        MoveParameterMapper moveMapper;
        StateMapper stateMapper;

        public Presenter(IView view, IGameController gameController)
        {
            this.view = view;
            this.gameController = gameController;
            view.OnMove += Move;
            view.OnMoveUndo += UndoMove;
            view.OnPickCargo += PickCargo;
            view.OnPickUndo += PickUndo;
            view.OnGetCargoInfo += GetCargoInfo;
            mapper = new FieldMapper();
            moveMapper = new MoveParameterMapper();
            stateMapper = new StateMapper();
        }

        public void StartGame()
        {
            view.DisplayStartMenu();
            CreateStates();
            var gameState = gameController.GetGameState();
            view.DisplayField(mapper.Map(gameState.GameField));
            DisplayPlayerInfo();
            view.DisplayGameMenu();
        }

        private void GetCargoInfo()
        {
            var cargo = gameController.GetCurrentCellCargo();
            var gameState = gameController.GetGameState();
            view.DisplayField(mapper.Map(gameState.GameField));
            DisplayPlayerInfo();

            if (cargo == null) 
                view.DisplayNoCargoInfo();
            else 
                view.DisplayCargoInfo(cargo.IsDecoding, cargo.Price, cargo.Weight);
            view.DisplayGameMenu();
        }

        private void CreateStates()
        {
            var gameState = stateMapper.Map(view.gameStateModel);
            gameController.CreateGameState(gameState);
            gameController.CreatePlayerState(view.playerStateModel.Number, view.playerStateModel.Name);
        }

        private void Move()
        {
            var moveParameter = moveMapper.Map(view.moveParameter);
            gameController.Move(moveParameter);
            CheckEndGame();
        }

        private void UndoMove()
        {
            gameController.UndoMove();
            CheckEndGame();
        }

        private void PickCargo()
        {
            gameController.PickCargo();
            CheckEndGame();
        }

        private void PickUndo()
        {
            gameController.PickUndo();
            CheckEndGame();
        }

        private void CheckEndGame()
        {
            var totalPrice = gameController.GetGameState().TotalCurrentPrice;
            if (gameController.CheckEndGame()) view.DisplayEndResult(totalPrice);
            else
            {
                var gameState = gameController.GetGameState();
                view.DisplayField(mapper.Map(gameState.GameField));
                DisplayPlayerInfo();
                view.DisplayGameMenu();
            }
        }

        private void DisplayPlayerInfo()
        {
            var gameState = gameController.GetGameState();
            var playerState = gameController.GetPlayerState();
            view.DisplayPlayerInfo(playerState.GameRobot.BatteryCharge, gameState.TotalCurrentPrice);
        }
    }
}
