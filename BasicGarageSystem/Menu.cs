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
    class MenuMain : Menu
    {
        public MenuMain(DisplayController dc)
        {
            Titel = "Main Menu:";
            Answers = new string[] {
                "Exit",
                "Check In Vehicle",
                "Search Registration Number",
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
                            (dc);
                        break;
                    case 2:
                        new MenuCheckIn
                            (dc);
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
        public MenuSearchRegNr(DisplayController dc)
        {
            Titel = "Search Registration Number";
            Answers = new string[] {
                "Exit",
                "Check In Vehicle",
                "Search Registration Number",
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
                            (dc);
                        break;
                    case 2:
                        new MenuCheckIn
                            (dc);
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
        public MenuCheckIn(DisplayController dc)
        {
            Titel = "Check In Vehicle:";
            Answers = new string[] {
                "Back",
                "DEEPER"
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
                        new MenuCheckIn(dc);
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
