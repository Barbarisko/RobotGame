using System;
using Microsoft.Extensions.DependencyInjection;
using RobotBLL.Abstraction;
using RobotBLL.Implementation;
using RobotBLL.Implementation.Services;
using RobotPL.Abstract;

namespace RobotPL
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var presenter = serviceProvider.GetService<Presenter>();
            presenter.StartGame();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICommandController, CommandController>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IGameController, GameController>();
            services.AddTransient<IView, View>();
            services.AddTransient<Presenter, Presenter>();
        }
    }
}
