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
        IGameStateService gameStateService;
        IPlayerStateService playerStateService;
        public PickCargoCommand(IGameStateService changeGameState, IPlayerStateService changePlayerState)
        {
            gameStateService = changeGameState;
            playerStateService = changePlayerState;
            actionCharge = 10;
        }

        public override void Execute()
        {
            var robotCoordinates = gameStateService.GetRobotCoordinates();
            var cargo = CheckCargo(robotCoordinates);
            if (cargo.IsDecoding) 
                PickDecodingCargo(robotCoordinates, cargo);
            else PickCargo(robotCoordinates, cargo);
        }

        public override void Undo()
        {
            playerStateService.RestoreState();
            gameStateService.UndoUpdateField();
            gameStateService.IncreaseCargoAmount();
        }

        private Cargo CheckCargo((int, int) robotCoordinates)
        {
            var cell = gameStateService.GetCell(robotCoordinates);
            if (cell.CurrentState == CellState.RobotCargo) 
                return cell.Cargo;
            else throw new PickCargoException("No cargo in the cell");
        }

        //separate in 2 interfaces - for
        private bool Decode()
        {
            Random random = new Random();
            var condition = random.Next() <= playerStateService.GetDecodingProbability();
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
                playerStateService.reduceBatteryCharge(100);
                gameStateService.CheckEndGame(playerStateService.GetBatteryCharge());
            }
        }

        private void PickCargo((int, int) robotCoordinates, Cargo cargo)
        {
            playerStateService.reduceBatteryCharge(actionCharge);
            gameStateService.IncreaseTotalPrice(cargo.Price);
            gameStateService.PickCargoUpdateField(robotCoordinates);
            gameStateService.ReduceCargoAmount();
            gameStateService.CheckEndGame(playerStateService.GetBatteryCharge());
        }
    }
}
