using RobotBLL.Abstraction;
using RobotBLL.Exceptions;
using RobotBLL.Implementation.CargoModels;
using RobotBLL.Implementation.Enums;
using RobotBLL.Implementation.RobotModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.Commands
{
    public class PickCargoCommand: Command
    {
        public PickCargoCommand(IGameStateService changeGameState, IPlayerStateService changePlayerState)
        {
            gameState = changeGameState;
            playerState = changePlayerState;
            actionCharge = 10;
        }

        public override void Execute()
        {
            var robotCoordinates = gameState.GetRobotCoordinates();
            var cargo = CheckCargo(robotCoordinates);
            if (cargo.IsDecoding) 
                PickDecodingCargo(robotCoordinates, cargo);
            else PickCargo(robotCoordinates, cargo);
        }

        public override void Undo()
        {
            playerState.RestoreState();
            gameState.UndoUpdateField();
            gameState.IncreaseCargoAmount();
        }

        private Cargo CheckCargo((int, int) robotCoordinates)
        {
            var cell = gameState.GetCell(robotCoordinates);
            if (cell.CurrentState == CellState.RobotCargo) 
                return cell.Cargo;
            else throw new PickCargoException("No cargo in the cell");
        }

        //мб вынести андукомманд
        private bool Decode()
        {
            Random random = new Random();
            var condition = random.Next() <= playerState.GetDecodingProbability();
            return condition ? true : false;
        }

        private void PickDecodingCargo((int, int) robotCoordinates, Cargo cargo)
        {
            if (Decode())
            {
                PickCargo(robotCoordinates, cargo);
            }
            else 
            {
                playerState.reduceBatteryCharge(100);
                gameState.CheckEndGame(playerState.GetBatteryCharge());
            }
        }

        private void PickCargo((int, int) robotCoordinates, Cargo cargo)
        {
            playerState.reduceBatteryCharge(actionCharge);
            gameState.IncreaseTotalPrice(cargo.Price);
            gameState.PickCargoUpdateField(robotCoordinates);
            gameState.ReduceCargoAmount();
            gameState.CheckEndGame(playerState.GetBatteryCharge());
        }
    }
}
