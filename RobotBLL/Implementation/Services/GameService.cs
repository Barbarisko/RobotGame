using RobotBLL.Abstraction;
using RobotBLL.Implementation.CargoModels;
using RobotBLL.Implementation.Enums;
using RobotBLL.Implementation.FieldModels;
using RobotBLL.Implementation.Models;
using RobotBLL.Implementation.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotBLL.Implementation.Services
{
    public class GameService: IGameService
    {
        Random random = new Random();
        Field field;
        private Field CreateField(int x, int y)
        {
            field = new Field(x, y);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    field.Cells[i, j] = new Cell();
                }
            }
            return field;
        }
        private List<Cargo> CreateCargos(int cargoAmount, double maxPrice, double maxWeight, bool isDecoding)
        {
            List<Cargo> cargos = new List<Cargo>(cargoAmount);
            for (int i = 0; i < cargoAmount; i++)
            {

                cargos.Add(new Cargo(random.NextDouble() * maxPrice, random.NextDouble() * maxWeight, isDecoding));
            }
            return cargos;
        }

        private List<Cargo> CreateToxicCargos(int cargoAmount, double maxPrice, double maxWeight, bool isDecoding)
        {
            List<Cargo> cargos = CreateCargos(cargoAmount, maxPrice, maxWeight, isDecoding);
            cargos = cargos.Select(c => c = new ToxicCargo(c))
                           .ToList();
            return cargos;
        }

        private void PlaceCargo(Cargo cargo, Cell cell)
        {
            cell.Cargo = cargo;
            if (cell.CurrentState == CellState.Robot) cell.CurrentState = CellState.RobotCargo; 
            else cell.CurrentState = CellState.Cargo;
        }

        private void PlaceAllCargos(List<Cargo> cargos, int x, int y, Field field)
                                                            //независимость от количества спискоd
        {
            int count = 0;
            foreach (Cargo c in cargos)
            {
                while (count < cargos.Count)
                {
                    int rx = random.Next(x);
                    int ry = random.Next(y);
                    if (field.Cells[rx, ry].CurrentState != CellState.Cargo)
                        PlaceCargo(cargos[count], field.Cells[rx, ry]);
                    count++;
                }
            }
        }

        public GameState CreateGameState(GameStateOptions options)
        {
            Field field = CreateField(options.x, options.y);
            field.Cells[0, 0].CurrentState = CellState.Robot;
            
            List<Cargo> cargos = CreateCargos(options.CargoAmount, options.MaxPrice, 
                                              options.MaxWeight, options.IsDecoding);
            List<Cargo> toxicCargos = CreateToxicCargos(options.ToxicCargoAmount, options.MaxPrice, 
                                                        options.MaxWeight, options.IsDecoding);
            cargos.AddRange(toxicCargos);
            
            PlaceAllCargos(cargos, options.x, options.y, field);

            var gameState = new GameState(field, options.CargoAmount + options.ToxicCargoAmount);
            gameState.PreviousStates.Push(CreatePreviousState(field));
            return gameState;
        }

        private Field CreatePreviousState(Field field)
        {
            var previousState = field.DeepClone();
            previousState.Cells[0, 0].CurrentState = CellState.Empty;
            return previousState;
        }
    }
}
