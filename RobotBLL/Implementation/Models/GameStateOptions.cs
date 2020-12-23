
namespace RobotBLL.Implementation.Models

{
    public class GameStateOptions
    {
        public int x { get; set; }
        public int y { get; set; }
        public int CargoAmount { get; set; }
        public int ToxicCargoAmount { get; set; }
        public double MaxPrice { get; set; }
        public double MaxWeight { get; set; }
        public bool IsDecoding { get; set; }

        public GameStateOptions()
        {

        }

        public GameStateOptions(int x, int y, int cargoAmount, int ToxicCargoAmount, 
                                double maxPrice, double maxWeight, bool isDecoding)
        {
            this.x = x;
            this.y = y;
            this.CargoAmount = cargoAmount;
            this.ToxicCargoAmount = ToxicCargoAmount;
            this.MaxPrice = maxPrice;
            this.MaxWeight = maxWeight;
            this.IsDecoding = isDecoding;
        }
    }
}
