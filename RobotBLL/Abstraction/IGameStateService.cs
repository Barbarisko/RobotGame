using RobotBLL.Implementation.CargoModels;
using RobotBLL.Implementation.FieldModels;

namespace RobotBLL.Abstraction
{
    public interface IGameStateService: 
                     IGameObjectsCoordinates, ICargoCalculation, IFieldActions
    {       
        void CheckEndGame(int robotCharge);
        Cargo GetCurrentCellCargo();
    }
    public interface ICargoCalculation
    {
        void ReduceCargoAmount();
        void IncreaseCargoAmount();
        void IncreaseTotalPrice(double price);
    }

    public interface IGameObjectsCoordinates
    {
        (int, int) GetRobotCoordinates();
        Cell GetCell((int, int) cellCoordinates);
    }
    public interface IFieldActions
    {
        (int, int) GetFieldDimension();
        void MoveUpdateField((int, int) newCoordinates);
        void PickCargoUpdateField((int, int) coordinates);
        void UndoUpdateField();
    }
}
