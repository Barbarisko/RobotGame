﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.Memento
{
    class GameHistory
    {
        public Stack<RobotMemento> History { get; private set; }
        public GameHistory()
        {
            History = new Stack<RobotMemento>();
        }
    }
}
