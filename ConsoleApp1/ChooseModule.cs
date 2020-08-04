using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using System.Threading;
using System.Drawing;

namespace ConsoleApp1
{
    class ChooseModule
    {
        public static int choose()
        {
            Console.Clear();
            Console.WriteLine(Program.logo);
            Console.WriteLine("");
            Console.WriteLine("Choose A Module:", Color.DodgerBlue);
            Console.WriteLine("[1] Account Checker");
            Console.WriteLine("[2] Config Settings");
            Console.WriteLine("[3] Join Discord !");


            int selection = 0;
            while (true)
            {
                selection = Console.ReadKey().KeyChar;
                Console.Write("\b \b");
                if (selection == 51)
                {
                    System.Diagnostics.Process.Start("https://discord.gg/TsHbDyZ");
                    continue;
                }
                else if (selection == 49)
                {
                    break;
                }
                else if (selection == 50)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            switch (selection)
            {
                case 49:
                    return 1;
                case 50:
                    return 2;
            }

            return 0;
        }
    }
}
