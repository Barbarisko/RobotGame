using RobotBLL.Implementation.CargoModels;
using RobotBLL.Implementation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.FieldModels
{
    public class Cell: ICloneable
    {
        public CellState CurrentState { get; set; } = CellState.Empty;
        public Cargo Cargo { get; set; } = null;

        public Cell()
        {

        }

        public object Clone()
        {
            Cargo cargo = null;
            if (this.CurrentState == CellState.Cargo || this.CurrentState == CellState.RobotCargo)
            {
                cargo = new Cargo
                {
                    Price = this.Cargo.Price,
                    Weight = this.Cargo.Weight,
                    IsDecoding = this.Cargo.IsDecoding
                };
            }
            return new Cell
            {
                CurrentState = this.CurrentState,
                Cargo = cargo
            };
        }
    }
}
