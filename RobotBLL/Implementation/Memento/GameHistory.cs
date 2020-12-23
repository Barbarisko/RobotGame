using System.Collections.Generic;

namespace RobotBLL.Implementation.Memento
{
    public class GameHistory
    {
        public Stack<RobotMemento> History { get; set; }
        public GameHistory()
        {
            History = new Stack<RobotMemento>();
        }

        public void SaveRobotMemento(RobotMemento memento)
        {
            History.Push(memento);
        }
    }
}
