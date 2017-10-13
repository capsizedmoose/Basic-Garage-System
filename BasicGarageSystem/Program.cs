using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BasicGarageSystem.HelperClass;

namespace BasicGarageSystem
{
    class Program
    {

        static void MainMenu()
        {
            GarageController gc = new GarageController();
            DisplayController dc = new DisplayController(gc);

            // For testing
            ////for (int i = 0; i < 10; i++)
            ////{
            ////    gc.ParkNewVehicle();
            ////}
            //gc.LoadGarageFromFile();
            //gc.SaveGarageToFile();
            //gc.LoadGarageFromFile();

            new MenuMain(dc,gc);
        }


        static void Main(string[] args)
        {
            MainMenu();
        }
    }
}
