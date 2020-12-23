using RobotBLL.Implementation.Enums;
using RobotBLL.Implementation.FieldModels;
using RobotPL.Models;

namespace RobotPL.Mappers
{
    class FieldMapper
    {
        public FieldMapper()
        {
        }

        public FieldModel Map(Field field)
        {
            int[,] fieldModel = new int[field.x, field.y];
            for (int i = 0; i < field.x; i++)
            {
                for (int j = 0; j < field.y; j++)
                {
                    if (field.Cells[i, j].CurrentState == CellState.Robot) fieldModel[i, j] = 1;
                    else if (field.Cells[i, j].CurrentState == CellState.Cargo) fieldModel[i, j] = 2;
                    else if (field.Cells[i, j].CurrentState == CellState.RobotCargo) fieldModel[i, j] = 3;
                    else fieldModel[i, j] = 0;
                }
            }
            return new FieldModel(fieldModel, field.x, field.y);
        }
    }
}
