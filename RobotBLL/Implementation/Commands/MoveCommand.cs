using RobotBLL.Abstraction;
using RobotBLL.Exceptions;
using RobotBLL.Implementation.Enums;

namespace RobotBLL.Implementation.Commands
{
    public class MoveCommand: Command
    {
        int moveCharge = 5;
        IGameStateService gameState;
        IPlayerStateService playerState;
        MoveParameter parameter;

        //раздуватель - длинный список параетров
        public MoveCommand(IGameStateService changeGameState, 
                           IPlayerStateService changePlayerState, MoveParameter parameter)
        {
            gameState = changeGameState;
            playerState = changePlayerState;
            this.parameter = parameter;
        }

        public override void Execute()
        {
            (int, int) newCoordinates = CheckParameter(parameter);
            playerState.reduceBatteryCharge(moveCharge);
            playerState.SaveState();
            gameState.MoveUpdateField(newCoordinates); //?
            gameState.CheckEndGame(playerState.GetBatteryCharge());
        }

        public override void Undo()
        {
            playerState.RestoreState();
            gameState.UndoUpdateField();
        }

        private (int, int) CheckParameter(MoveParameter param)//add coords to params?
        {
            (int, int) coordinates = gameState.GetRobotCoordinates();
            switch (param)
            {
                case MoveParameter.Up:
                    return CanMoveUp(coordinates);
                case MoveParameter.Down:
                    return CanMoveDown(coordinates);
                case MoveParameter.Left:
                    return CanMoveLeft(coordinates);
                case MoveParameter.Right:
                    return CanMoveRight(coordinates);
                default:
                    return (0, 0);
            }
        }

        private (int, int) CanMoveUp((int, int) coordinates)
        {
            if (coordinates.Item1 == 0) throw new MoveException("Can not move up");
            return (coordinates.Item1 - 1, coordinates.Item2);
        }

        private (int, int) CanMoveDown((int, int) coordinates)
        {
            (int, int) dimension = gameState.GetFieldDimension();
            if (coordinates.Item1 == dimension.Item1 - 1) throw new MoveException("Can not move down");
            return (coordinates.Item1 + 1, coordinates.Item2);
        }

        private (int, int) CanMoveLeft((int, int) coordinates)
        {
            if (coordinates.Item2 == 0) throw new MoveException("can not move Left");
            return (coordinates.Item1, coordinates.Item2 - 1);
        }

        private (int, int) CanMoveRight((int, int) coordinates)
        {
            (int, int) dimension = gameState.GetFieldDimension();
            if (coordinates.Item2 == dimension.Item2 - 1) throw new MoveException("Can not move down");
            return (coordinates.Item1, coordinates.Item2 + 1);
        }
    }
}
