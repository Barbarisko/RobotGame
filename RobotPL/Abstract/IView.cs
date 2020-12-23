using RobotPL.Models;

namespace RobotPL.Abstract
{
    interface IView: IDisplayable
    {
        GameStateModel gameStateModel { get; set; }
        PlayerStateModel playerStateModel { get; set; }
        string userOption { get; set; }
        string moveParameter { get; set; }

        event View.Handler OnMove;
        event View.Handler OnPickCargo;
        event View.Handler OnMoveUndo;
        event View.Handler OnPickUndo;
        event View.Handler OnGetCargoInfo;
    }
    interface IDisplayable
    {
        void DisplayStartMenu();
        void DisplayField(FieldModel fieldModel);
        void DisplayGameMenu();
        void DisplayEndResult(double totalPrice);
        void DisplayPlayerInfo(int batteryCharge, double totalPrice);
        void DisplayCargoInfo(bool isDecoding, double price, double weight);
        void DisplayNoCargoInfo();
    }
}
