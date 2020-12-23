
namespace RobotBLL.Abstraction
{
    public abstract class Command
    {
        //почему класс а не интерфейс
        public abstract void Execute();

        //отдельные интерфейсы для команд
        public abstract void Undo();

        //int actionCharge;
        //IGameStateService gameState;
        //IPlayerStateService playerState;
    }
    public interface ICommand
    {
         void Execute();

    }

}
