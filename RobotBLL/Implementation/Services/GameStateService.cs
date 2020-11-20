﻿using RobotBLL.Abstraction;
using RobotBLL.Implementation.Enums;
using RobotBLL.Implementation.FieldModels;
using RobotBLL.Implementation.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.Services
{
    public class GameStateService: IGameStateService
    {
        GameState gameState;

        public GameStateService(GameState state)
        {
            gameState = state;
        }

        public void ReduceCargoAmount()
        {
            gameState.CargoAmount--;
        }

        public void IncreaseCargoAmount()
        {
            gameState.CargoAmount++;
        }

        public void IncreaseTotalPrice(double price)
        {
            gameState.TotalCurrentPrice += price;
        }

        public (int, int) GetRobotCoordinates()
        {
            for (int i = 0; i<gameState.GameField.x; i++)
            {
                for (int j=0; j<gameState.GameField.y; j++)
                {
                    if (gameState.GameField.Cells[i, j].CurrentState == CellState.Robot || 
                        gameState.GameField.Cells[i, j].CurrentState == CellState.RobotCargo) 
                        return (i, j);
                }
            }
            return (0, 0);
        }

        public Cell GetCell((int, int) cellCoordinates)
        {
            int x = cellCoordinates.Item1;
            int y = cellCoordinates.Item2;
            return gameState.GameField.Cells[x, y];
        }

        public (int, int) GetFieldDimension()
        {
            return (gameState.GameField.x, gameState.GameField.y);
        }

        public void UndoUpdateField()
        {
            gameState.GameField.Cells = gameState.GameField.PreviousState;
        }

        public void MoveUpdateField((int, int) newCoordinates)
        {
            (int, int) oldCoordinates = GetRobotCoordinates();
            var previousState = gameState.GameField.PreviousState;
            gameState.GameField.PreviousState = gameState.GameField.Cells;
            ChangeNewRobotCellState(newCoordinates);
            ChangeRobotCellState(oldCoordinates, previousState);
            //for (int i = 0; i < gameState.GameField.x; i++)
            //{
            //    for (int j = 0; j < gameState.GameField.y; j++)
            //    {
            //        if ((i, j) != oldCoordinates && (i, j) != newCoordinates)
            //            gameState.GameField.Cells[i, j].PreviousState = gameState.GameField.Cells[i, j].CurrentState;
            //    }
            //}
        }

        public void PickCargoUpdateField((int, int) coordinates)
        {
            int x = coordinates.Item1;
            int y = coordinates.Item2;
            gameState.GameField.PreviousState = gameState.GameField.Cells;
            gameState.GameField.Cells[x, y].CurrentState = CellState.Robot;
            gameState.GameField.Cells[x, y].Cargo = null;
        }

        public void CheckEndGame(int robotCharge)
        {
            if (robotCharge <= 0) gameState.IsEnded = true;
        }

        private void ChangeRobotCellState((int, int) coordinates, Cell[,] previousState)
        {
            int x = coordinates.Item1;
            int y = coordinates.Item2;
            //var currentState = gameState.GameField.Cells[coordinates.Item1, coordinates.Item2].CurrentState;
            //var previousState = gameState.GameField.Cells[coordinates.Item1, coordinates.Item2].PreviousState;
            gameState.GameField.Cells[x, y].CurrentState = previousState[x, y].CurrentState;
            //gameState.GameField.Cells[coordinates.Item1, coordinates.Item2].PreviousState = currentState;
        }

        private void ChangeNewRobotCellState((int, int) coordinates)
        {
            int x = coordinates.Item1;
            int y = coordinates.Item2;
            var currentState = gameState.GameField.Cells[x, y].CurrentState;
            //gameState.GameField.Cells[coordinates.Item1, coordinates.Item2].PreviousState = currentState;
            if (currentState == CellState.Cargo)
                gameState.GameField.Cells[x, y].CurrentState = CellState.RobotCargo;
            else gameState.GameField.Cells[x, y].CurrentState = CellState.Robot;
        }
    }
}
