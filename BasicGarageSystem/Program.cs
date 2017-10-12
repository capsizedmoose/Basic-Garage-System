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

        static void MainMenu() {

        }


        static void Main(string[] args)
        {
            MainMenu();

            Random randomNumber = new Random();
            int num;
            string regNum = "";

            for (int i = 0; i < 3; i++)
            {
                num = randomNumber.Next(97, 122);
                regNum += (char)num;
            }

            for (int i = 0; i < 3; i++)
            {
                num = randomNumber.Next(0, 9);
                regNum += num;
            }

            Console.WriteLine(regNum.ToUpper());

            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
