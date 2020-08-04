using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ConsoleApp1
{
    class OpenFile
    {

        public static String[] openFile(object title)
        {
            String[] a = null;
            try
            {
                Thread t = new Thread((ThreadStart)(() => {
                    OpenFileDialog saveFileDialog1 = new OpenFileDialog();

                    saveFileDialog1.Title = title.ToString();
                    saveFileDialog1.Multiselect = false;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        a = File.ReadAllLines(saveFileDialog1.FileName);
                    }
                }));

                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
            }
            catch (Exception ex)
            {
                return null;
            }

            return a;
        }
    }
}
