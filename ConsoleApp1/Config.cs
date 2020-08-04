using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System;
using System.Threading;
using Colorful;
using Console = Colorful.Console;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Config
    {


        public class configObject
        {
            public int threads { get; set; }
            public Boolean API { get; set; }
            public string API_LINK { get; set; }
            public int API_REFRESH_TIME { get; set; }
            public Boolean WEBHOOK { get; set; }
            public string WEBHOOK_LINK { get; set; }
            public bool INVALIDS { get; set; }
            public bool PROXYLESS { get; set; }
        }

        public static void printLogo()
        {
            Console.Clear();
            Console.WriteLine(Program.logo);
            Console.WriteLine("");
            Console.Write("[OUTLAWZ] ", Color.LightGoldenrodYellow);
        }

        public static configObject renewconfig(Boolean AskToSave)
        {
            configObject Config = new configObject();

            printLogo();
            Console.WriteLine("How Many Threads do you wish?");
            Config.threads = int.Parse(Console.ReadLine());

            printLogo();
            Console.WriteLine("Proxyless Y/N?");
            string ab = Console.ReadLine().ToLower();
            Config.PROXYLESS = ab != "n";
            if (!Config.PROXYLESS)
            {
                printLogo();
                Console.WriteLine("Load proxies through API Y/N?");
                string a = Console.ReadLine().ToLower();
                Config.API = a != "n";
                if (Config.API)
                {
                    printLogo();
                    Console.WriteLine("Enter Your API Link");
                    Config.API_LINK = Console.ReadLine();

                    printLogo();
                    Console.WriteLine("Refresh After How Many Minutes?");
                    Config.API_REFRESH_TIME = int.Parse(Console.ReadLine());
                }
            }
            /*
            printLogo();
            Console.WriteLine("Do You Wanna Send The Hits Via Discord Webhook Y/N?");
            string b = Console.ReadLine().ToLower();
            Config.WEBHOOK = b != "n";
            if (Config.WEBHOOK)
            {
                printLogo();
                Console.WriteLine("Enter Webhook Link");
                Config.WEBHOOK_LINK = Console.ReadLine();
            }
            */
            printLogo();
            Console.WriteLine("Do You Wanna Save Invalids Y/N?");
            string c = Console.ReadLine().ToLower();
            Config.INVALIDS = c != "n";

            printLogo();

            if (AskToSave)
            {
                Console.WriteLine("Do you want to save this config Y/N?");
                string d = Console.ReadLine().ToLower();
                bool e = d != "n";
                if (e)
                {
                    System.IO.File.WriteAllText("config.json", JsonConvert.SerializeObject(Config));
                    Console.WriteLine("Config saved!", Color.LawnGreen);
                    return Config;
                }
                else
                {
                    return Config;
                }

            }
            else
            {
                System.IO.File.WriteAllText("config.json", JsonConvert.SerializeObject(Config));
                Console.WriteLine("Config saved!", Color.LawnGreen);
                Thread.Sleep(2000);
                return null;
            }
        }
    }
}
