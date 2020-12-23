using RobotBLL.Implementation.Factories;
using RobotBLL.Implementation.RobotModels;
using System;
using System.Collections.Generic;
using RobotBLL.Abstraction;
using RobotBLL.Implementation.States;
using RobotBLL.Implementation.Memento;
using RobotBLL.Implementation.Enums;

namespace RobotBLL.Implementation.Services
{
    public class PlayerService: IPlayerService
    {
        public Dictionary<RobotType, double> ChoiceProbability { get; set; } = new Dictionary<RobotType, double>
        {
            {RobotType.workerRobot, 0.5 },
            {RobotType.cyborgRobot, 0.3 },
            {RobotType.smartRobot, 0.2 }
        };
        private Robot CreateRobot(RobotModel model, RobotCreator creator)
        {
            return creator.CreateRobot(model);
        }

        private Robot ChooseRobot(RobotModel model)
        {
            RobotCreator creator = null;
            double sum = 0;
            Random random = new Random();
            double randomNumber = random.NextDouble();
            foreach (KeyValuePair<RobotType, double> probability in ChoiceProbability)
            {
                if (randomNumber <= (sum = sum + probability.Value))
                {
                    creator = ChooseCreator(probability.Key);
                    break;
                }
            }
            return CreateRobot(model, creator);
        }

        public PlayerState CreatePlayerState(RobotModel model, GameHistory history)
        {
            return new PlayerState(ChooseRobot(model), history);
        }
        private RobotCreator ChooseCreator(RobotType type)
        {
            switch (type)
            {
                case RobotType.workerRobot:
                    return new WorkerRobotCreator();
                case RobotType.cyborgRobot:
                    return new CyborgRobotCreator();
                case RobotType.smartRobot:
                    return new SmartRobotCreator();
                default:
                    return null;
            }
        }
    }
}
