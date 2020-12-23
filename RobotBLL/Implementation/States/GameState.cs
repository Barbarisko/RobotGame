using RobotBLL.Implementation.FieldModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.States
{
    public class GameState
    {
        public int CargoAmount { get; set; } = 0;
        public double TotalCurrentPrice { get; set; } = 0;
        public bool IsEnded { get; set; } = false;
        public Field GameField { get; set; }
        public Stack<Field> PreviousStates { get; set; }

        public GameState(Field field, int cargosAmount)
        {
            CargoAmount = cargosAmount;
            GameField = field;
            PreviousStates = new Stack<Field>();
        }
        public void SaveFieldState(Field field)
        {
            PreviousStates.Push(field);
        }
        public Field RestoreField()
        {
            return PreviousStates.Pop();
        }
    }
}
