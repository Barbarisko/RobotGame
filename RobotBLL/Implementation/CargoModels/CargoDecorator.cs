
namespace RobotBLL.Implementation.CargoModels
{
    abstract class CargoDecorator: Cargo
    {
        protected Cargo cargo;

        public CargoDecorator(double price, double weight, bool isDecoding, Cargo cargo)
                       : base(price, weight, isDecoding)
        {
            this.cargo = cargo;
        }
    }
}
