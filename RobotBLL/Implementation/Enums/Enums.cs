using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.Enums
{
    public enum MoveParameter
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum CellState
    {
        Empty,
        Robot,
        Cargo,
        RobotCargo
    }
    public enum RobotType
    {
        workerRobot,
        cyborgRobot,
        smartRobot
    }
}
