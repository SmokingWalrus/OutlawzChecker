using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using System.Drawing;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Module1
    {
        public static void checkercui()
        {
            Console.Clear();
            Console.WriteLine(Program.logo);
            Console.WriteLine("");
            Console.ForegroundColor = Color.LightGray;
            Console.WriteLine("Threads starting . . .", Color.LightGray);
            Thread.Sleep(3000);
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Program.logo);

                Console.WriteLine("");
                Console.WriteLine("Threads running : " + Stats.runningThreads); //+thread zahl
                Console.WriteLine("");

                Console.Write("Valid: ");
                int valid = Stats.DUO + Stats.FREE + Stats.FAMILY_MEMBER + Stats.FAMILY_OWNER + Stats.HULU +
                            Stats.OTHER + Stats.STUDENT;
                Console.WriteLine(valid, Color.LawnGreen);

                Console.Write("Invalid: ");
                Console.WriteLine(Stats.INVALID, Color.DarkRed);


                Console.Write("Free : ");
                Console.WriteLine(Stats.FREE, Color.BurlyWood);
                Console.WriteLine("");

                //////////////////PREMIUM///////////////////////////
                Console.Write("Premium : ");
                Console.WriteLine(Stats.PREMIUM, Color.LimeGreen);

                Console.Write("    Spotify Duo : ");
                Console.WriteLine(Stats.DUO, Color.SeaGreen);

                Console.Write("    Spotify Student : ");
                Console.WriteLine(Stats.STUDENT, Color.SeaGreen);

                Console.Write("    Spotify Hulu : ");
                Console.WriteLine(Stats.STUDENT, Color.SeaGreen);

                Console.Write("    Spotify Other : ");
                Console.WriteLine(Stats.OTHER, Color.SeaGreen);
                Console.WriteLine("");
                //////////////////PREMIUM///////////////////////////

                Console.Write("Premium Family : ");
                int familys = Stats.FAMILY_OWNER + Stats.FAMILY_MEMBER;
                Console.WriteLine(familys, Color.DarkOrchid);

                Console.Write("    Family Member : ");
                Console.WriteLine(Stats.FAMILY_MEMBER, Color.Coral);

                Console.Write("    Family Owner : ");
                Console.WriteLine(Stats.FAMILY_OWNER, Color.DeepSkyBlue);
                Console.WriteLine("");

                Console.Write("Errors : ");
                Console.WriteLine(Stats.ERROR, Color.DarkGray);

                Console.Write("Remain : ");
                Console.WriteLine(getAcc.accindex+ "/"+ Stats.combo.Count, Color.Cornsilk);

                Console.Write("Average CPM : ");
                Console.WriteLine("1337420", Color.DarkSalmon);
                Console.WriteLine("");

                Console.ForegroundColor = Color.Goldenrod;
                Typewriter.Typewrite("Thank You For Using Outlawz Checker :)");
                Console.ForegroundColor = Color.LightGray;
                Thread.Sleep(1500);
            }




            //WHILE SCHLEIFE FÜR NUMMERN UPDATE
        }



        public static Config.configObject getConfig()
        {
            if (File.Exists("config.json"))
            {
                return JsonConvert.DeserializeObject<Config.configObject>(File.ReadAllText("config.json"));
            }
            else
            {
                return Config.renewconfig(true);
            }
        }
        
    }
}
