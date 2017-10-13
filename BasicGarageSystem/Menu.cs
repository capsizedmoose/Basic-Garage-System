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
        public MenuVehicles(DisplayController dc, GarageController gc, v_Vehicle type)
        {
            int nav = 0;
            do
            {
                string[] vehicles = gc.FindVehiclesByType(type).ToArray();
                Titel = "Choose Vehicle:";
                Answers = new string[vehicles.Length+1];
                Answers[0] = "Back";
                vehicles.CopyTo(Answers, 1);
                dc.Layer++;
                Length = Answers.Length;
                nav = GetAnswer(dc);
                if (nav != 0)
                {
                    new MenuThisVehicle(dc,gc,Answers[nav]);
                }
                dc.RemoveLine();
                dc.UpdateDisplay();
                dc.Layer--;
                dc.UpdateDisplay();
            } while (nav != 0);
        }
    }
    class MenuThisVehicle : Menu
    {
        public MenuThisVehicle(DisplayController dc, GarageController gc, string vehicle)
        {
            Titel = vehicle;
            Answers = new string[] {
                "Back",
                "Get Info",
                "Check out",
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
                        dc.InfoText = gc.ViewParkedVehicle(Titel.Substring(0,6));
                        break;
                    case 2:
                        dc.InfoText = gc.VehicleCheckout(Titel.Substring(0, 6));
                        dc.RemoveLine();
                        dc.Layer--;
                        dc.UpdateDisplay();
                        return;
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
                        new MenuSearch(dc,gc);
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



    class MenuSearch : Menu
    {
        public MenuSearch(DisplayController dc, GarageController gc)
        {
            Titel = "Search For Vehicle:";
            Answers = new string[] {
                "Back",
                "Search By Type",
                "Search For A Specific Vehicle",
                "Search Trough All Vehicles"
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
                        new MenuSearchType(dc, gc);
                        break;
                    case 2:
                        new MenuSearchRegNr(dc, gc);
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

    class MenuSearchType : Menu
    {
        public MenuSearchType(DisplayController dc, GarageController gc)
        {
            int nav = 0;
            do
            {
                Titel = "Search By Type:";
                Answers = new string[Enum.GetNames(typeof(v_Vehicle)).Length+1];
                Answers[0] = "Back";
                Enum.GetNames(typeof(v_Vehicle)).CopyTo(Answers,1);
                dc.Layer++;
                Length = Answers.Length;
                nav = GetAnswer(dc);
                if (nav != 0){
                    
                    new MenuVehicles(dc, gc,(v_Vehicle)nav-1);
                }
                dc.RemoveLine();
                dc.UpdateDisplay();
                dc.Layer--;
                dc.UpdateDisplay();
            } while (nav != 0);
        }
    }
    class MenuSearchRegNr : Menu
    {
        public MenuSearchRegNr(DisplayController dc, GarageController gc)
        {
            Titel = "Search By Regnr";
            dc.Layer++;
            dc.WriteLine(Titel);
            dc.UpdateDisplay();
            string searchString = GetInput(false);
            if (gc.FindVehicle(searchString.ToUpper()) != null) {
                new MenuThisVehicle(dc,gc,gc.FindVehicle(searchString.ToUpper()));
            }
            dc.RemoveLine();
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
