using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Abstraction
{
    public abstract class Command
    {
        //почему класс а не интерфейс
        public abstract void Execute();

        //отдельные интерфейсы для команд
        public abstract void Undo();
    }
    public interface ICommand
    {
         void Execute();

    }

}
