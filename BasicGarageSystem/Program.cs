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
            DisplayController dc = new DisplayController();
            new MenuMain(dc);
        }


        static void Main(string[] args)
        {
            MainMenu();
        }
    }
}
