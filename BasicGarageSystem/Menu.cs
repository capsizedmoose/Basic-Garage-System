using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGarageSystem
{
    class Menu
    {

        public string Titel { get; set; }
        public string[] Answers { get; set; }
        public int Length { get; set; }

        public Menu(string titel,string[] answers,DisplayController dc)
        {
            Answers = answers;
            Length = Answers.Length;
            Titel = titel;


            dc.Layer++;

            dc.WriteLine(Titel);
            foreach (string a in Answers)
            {
                dc.WriteLine(a);
            }
        }

        private void resetDisplay(DisplayController dc)
        {

            dc.Layer--;

            for (int i = 0; i < Length; i++) {
                dc.RemoveLine();
            }
        }


    }
    class MainMenu : Menu {


        public MainMenu(string titel, string[] answers, DisplayController dc) : base(titel, answers, dc)
        {

            Titel = "Main Menu:";
            Answers = new string[] {
                "Check In Vehicle",
                "Search"
            };
            Length = Answers.Length;





            int nav = dc.GetInput(Length);
            switch (nav) {
                case 0:
                    break;
            }

        }
    }
}
