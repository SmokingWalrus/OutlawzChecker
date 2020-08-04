using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    class getAcc
    {
        public static bool a = false;
        public static int accindex = 0;
        public static bool lellll = false;
        public static string getacc()
        {
            try
            {
                int i = accindex;
                accindex++;
                return Stats.combo[i];
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                Application.Exit();
                return null;
            }
        }
    }
}
