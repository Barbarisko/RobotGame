using RobotBLL.Abstraction;

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
