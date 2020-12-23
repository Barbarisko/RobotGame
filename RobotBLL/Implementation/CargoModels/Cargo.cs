
namespace RobotBLL.Implementation.CargoModels
{
    public class Cargo
    {
        public double Price { get; set; }
        public double Weight { get; set; }
        public bool IsDecoding { get; set; }       

        public Cargo(double price, double weight, bool isDecoding)
        {
            Price = price;
            Weight = weight;
            IsDecoding = isDecoding;
        }
        public Cargo(){  }
    }
}
