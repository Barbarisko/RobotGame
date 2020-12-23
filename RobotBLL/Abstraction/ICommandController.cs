
namespace RobotBLL.Abstraction
{
    public interface ICommandController
    {
        void SetMoveCommand(Command command);
        void SetPickCommand(Command pickCargoCommand);
        void Move();
        void MoveUndo();
        void PickCargo();
        void PickCargoUndo();
    }
}
