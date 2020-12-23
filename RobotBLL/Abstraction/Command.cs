
namespace RobotBLL.Abstraction
{
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void Undo();

        protected int actionCharge;
        protected IGameStateService gameState;
        protected IPlayerStateService playerState;
    }
}
