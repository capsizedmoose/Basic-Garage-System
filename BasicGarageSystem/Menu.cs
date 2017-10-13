using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BasicGarageSystem.HelperClass;

namespace BasicGarageSystem
{
    class Menu
    {

        public string Titel { get; set; }
        public string[] Answers { get; set; }
        public int Length { get; set; }

        public void initiateDisplay(DisplayController dc){

            dc.WriteLine(Titel);
            foreach (string a in Answers)
            {
                dc.WriteLine(a);
            }

        }

        public void resetDisplay(DisplayController dc)
        {
            foreach (string a in Answers)
            {
                dc.RemoveLine();
            }
        }

        public int GetAnswer(DisplayController dc) {
            initiateDisplay(dc);
            int nav = dc.GetDirectionalInput(Length);
            resetDisplay(dc);
            return nav;
        }



    }

    class MenuVehicles : Menu
    {
        public MenuVehicles(DisplayController dc, GarageController gc, string[] vehicles)
        {
            Titel = "Main Menu:";
            Answers = new string[vehicles.Length + 1];
            new string
        }
    }

    class MenuMain : Menu
    {
        public MenuMain(DisplayController dc, GarageController gc)
        {
            Titel = "Main Menu:";
            Answers = new string[] {
                "Exit",
                "Check In Vehicle",
                "Search For Vehicle",
            };
            dc.Layer++;
            Length = Answers.Length;


            dc.WriteLine(Titel);
            string searchString = GetInput(false);







            int nav = 0;
            do
            {
                nav = GetAnswer(dc);
                switch (nav)
                {
                    case 1:
                        new MenuCheckIn(dc,gc);
                        break;
                    case 2:
                        new MenuMain(dc,gc);
                        break;

                    case 0:
                    default:
                        break;
                }
                dc.RemoveLine();
                dc.UpdateDisplay();
            } while (nav != 0);
            dc.Layer--;
            dc.UpdateDisplay();
        }
    }

    class MenuSearchRegNr : Menu
    {
        public MenuSearchRegNr(DisplayController dc, GarageController gc)
        {
            Titel = "Search By Regnr";
            Answers = new string[] {
                "Back",
                "Checkout" 
            };
            dc.Layer++;
            Length = Answers.Length;
            int nav = 0;
            do
            {
                nav = GetAnswer(dc);
                switch (nav)
                {
                    case 1:
                        new MenuCheckIn
                            (dc,gc);
                        break;
                    case 2:
                        new MenuCheckIn
                            (dc,gc);
                        break;

                    case 0:
                    default:
                        break;
                }
                dc.RemoveLine();
                dc.UpdateDisplay();
            } while (nav != 0);
            dc.Layer--;
            dc.UpdateDisplay();
        }
    }

    class MenuCheckIn : Menu
    {
        public MenuCheckIn(DisplayController dc, GarageController gc)
        {
            Titel = "Check In Vehicle:";
            Answers = new string[] {
                "Back",
                "Check in a random vehicle",
                "Check in a random Car",
                "Check in a random Motorcycle",
                "Check in a random Truck",
                "Check in a random Bus",
            };
            dc.Layer++;
            Length = Answers.Length;
            int nav = 0;
            do
            {
                nav = GetAnswer(dc);
                switch (nav)
                {
                    case 1:
                        dc.InfoText = gc.ParkNewVehicle();
                        break;
                    case 2:
                        dc.InfoText = gc.ParkVehicle(v_Vehicle.Car);
                        break;
                    case 3:
                        dc.InfoText = gc.ParkVehicle(v_Vehicle.MC);
                        break;
                    case 4:
                        dc.InfoText = gc.ParkVehicle(v_Vehicle.Truck);
                        break;
                    case 5:
                        dc.InfoText = gc.ParkVehicle(v_Vehicle.Bus);
                        break;
                    case 0:
                        break;

                    default:
                        break;
                }
                dc.RemoveLine();
                dc.UpdateDisplay();
            } while (nav != 0);
            dc.Layer--;
            dc.UpdateDisplay();
        }
    }
}
