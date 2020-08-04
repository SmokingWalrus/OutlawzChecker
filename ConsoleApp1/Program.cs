using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Figgle;

namespace ConsoleApp1
{
    class Program
    {
        public static string logo = FiggleFonts.Standard.Render("                           OUTLAWZ!");
        static void Main(string[] args)
        {
            DiscordRPC.Initialize();
            Console.Title = "OUTLAWZ CHECKER - THANKS FOR BUYING";
            Console.WriteLine(logo);
            int mode = 0;
            while (true)
            {
                mode = ChooseModule.choose();
                if (mode == 2)
                {
                    Config.renewconfig(false);
                    continue;
                }
                else if (mode == 1)
                {
                    break;
                }
            }
            DateTime thisDay = DateTime.UtcNow;
            string date = thisDay.ToString().Replace("/", "-").Replace("/", "-").Replace("/", "-").Replace("/", "-")
                .Replace(":", "-").Replace(":", "-").Replace(":", "-").Replace(":", "-");


            outFolder = "results\\" + date;
            Directory.CreateDirectory(outFolder);
            Directory.CreateDirectory(outFolder +"\\country\\free");
            Directory.CreateDirectory(outFolder + "\\country\\premium");

            CUI.Init();
            Thread CUITHREAD = new Thread(Module1.checkercui);
            CUITHREAD.Start();



            //LogCUI.loginterface();

            Thread.Sleep(100000);

            //Thread lol = new Thread(blala.check);
            //lol.Start("samgefx@gmail.com:niggernigger123|null");
        }

        public static string outFolder = null;
    }
}