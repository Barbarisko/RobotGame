﻿using RobotBLL.Implementation.RobotModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBLL.Implementation.Factories
{
    public class CyborgRobotCreator : RobotCreator
    {
        public override Robot CreateRobot(RobotModel model)
        {
            Robot robot = new CyborgRobot(model);
            robot.DecodingProbability = 60;
            robot.Carrying = 15;
            return robot;
        }
    }
}
