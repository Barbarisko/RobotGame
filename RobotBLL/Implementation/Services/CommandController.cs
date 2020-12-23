using RobotBLL.Abstraction;
using RobotBLL.Implementation.Commands;
using RobotBLL.Implementation.RobotModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.Services
{
    
    public class CommandController: ICommandController
    {
        Command MoveCommand;
        Command PickCargoCommand;

        //
        public void SetMoveCommand(Command moveCommand)
        {
            MoveCommand = moveCommand;
        }

        public void SetPickCommand(Command pickCargoCommand)
        {
            PickCargoCommand = pickCargoCommand;
        }

        public void Move()
        {
            MoveCommand.Execute();
        }

        //to do single Undo command - not to undo
        public void MoveUndo()
        {
            MoveCommand.Undo();
        }

        public void PickCargo()
        {
            PickCargoCommand.Execute();
        }

        public void PickCargoUndo()
        {
            PickCargoCommand.Undo();
        }
    }
}
