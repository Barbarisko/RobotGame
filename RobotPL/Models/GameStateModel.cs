
namespace RobotPL.Models
{
    class GameStateModel
    {
        public int x { get; set; }
        public int y { get; set; }
        public int cargoAmount { get; set; } 
        public int toxicCargoAmount { get; set; }
        public double MaxPrice { get; set; }
        public double MaxWeight { get; set; }
        public bool IsDecoding { get; set; }

        public GameStateModel(int x, int y, int ca, int tca, double mp, double mw, bool id)
        {
            this.x = x;
            this.y = y;
            cargoAmount = ca;
            toxicCargoAmount = tca;
            MaxPrice = mp;
            MaxWeight = mw;
            IsDecoding = id;
        }

        public GameStateModel()
        {

        }

    }
}
