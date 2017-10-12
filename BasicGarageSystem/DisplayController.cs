using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BasicGarageSystem.HelperClass;

namespace BasicGarageSystem
{
    class DisplayController
    {

        public List<string> DisplayList { get; set; }

        public int Layer{ get; set;}
        public int CursorPos { get; set; }


        public DisplayController()
        {
            DisplayList = new List<string>();
            Layer = 0;
        }

        public void WriteLine(string s) {

            DisplayList.Add(s);

        }

        public void RemoveLine() {
            DisplayList.RemoveAt(DisplayList.Count-1);
        }

        public void UpdateDisplay() {

            Console.Clear();

            int i = 0;

            foreach (string s in DisplayList) {

                string stringIndentation = "";

                for (int j = 0; j < i && j < Layer && j < 10; j++)
                {
                    stringIndentation += " ";
                }
                if (i > 0){
                    stringIndentation += "∟";
                }

                if (i < Layer)
                {
                    CngBCol(ConsoleColor.Black);
                    CngFCol(ConsoleColor.DarkGray);
                }
                else
                {
                    if (CursorPos + Layer == i)
                    {
                        CngBCol(ConsoleColor.White);
                        CngFCol(ConsoleColor.Black);
                    }
                    else
                    {
                        CngBCol(ConsoleColor.Black);
                        CngFCol(ConsoleColor.White);
                    }
                }
                Console.WriteLine(stringIndentation+s);


                CngBCol(ConsoleColor.Black);
                CngFCol(ConsoleColor.White);
                i++;
            }

        }

        public int GetInput(int range) {

            CursorPos = 0;

            ConsoleKeyInfo key;

            UpdateDisplay();

            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (CursorPos > 0)
                        {
                            CursorPos--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (CursorPos < range-1)
                        {
                            CursorPos++;
                        }
                        break;
                }
                UpdateDisplay();
            } while (key.Key != ConsoleKey.Enter);

            return CursorPos;
        }


    }
}
