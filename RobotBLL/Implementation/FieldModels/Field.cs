﻿using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.FieldModels
{
    public class Field: IEnumerable<Cell>
    {
        public int x { get; set; }
        public int y { get; set; }
        public Cell[,] Cells { get; set; }
        public Cell[,] PreviousState { get; set; } //TODO: change to stack

        public Field(int x, int y)
        {
            Cells = new Cell[x, y];
            this.x = x;
            this.y = y;
        }

        public Cell[,] DeepClone(Cell[,] cells)
        {
            Cell[,] newcells = new Cell[x, y];
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    newcells[i, j] = (Cell)cells[i, j].Clone();
                }
            }
            return newcells;
        }

        public (int, int) GetCellCoordinates(Cell cell, Cell[,] cells)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if (cells[i, j].Equals(cell)) return (i, j);
                }
            }
            return (-1, -1);
        }

        public Cell this[(int, int) point]
        {
            get => Cells[point.Item1, point.Item2];
            set => Cells[point.Item1, point.Item2] = value;
        }


        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (Cell c in Cells) yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
