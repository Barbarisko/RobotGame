﻿using RobotBLL.Implementation.FieldModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.States
{
    public class GameState
    {
        public int CargoAmount { get; set; } = 0;
        public float TotalCurrentPrice { get; set; } = 0;
        public bool IsEnded { get; set; } = false;
        public Field GameField { get; set; }

        public GameState(Field field, int cargosAmount)
        {
            CargoAmount = cargosAmount;
            GameField = field;
        }
    }
}