using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class proxyObject
    {
        public string Proxy { get; set; }
        public Boolean Working { get; set; }

        public proxyObject(string proxy, Boolean working)
        {
            Proxy = proxy;
            Working = working;
        }
    }
    class Proxy
    {
        public static List<proxyObject> proxies = new List<proxyObject>();
        public static bool updating = false;

        private static string[] URL = null;
        private static int proxyindex = 0;
        public static Boolean updateList(string[] url)
        {
            updating = true;
            URL = url;
            proxies.Clear();
            proxyindex = 0;
            foreach (String str in URL)
            {
                try
                {
                    var client = new HttpRequest();
                    var response = client.Get(str).ToString();
                    foreach (String line in response.Split('\n'))
                    {
                        if (line.Length > 7)
                        {
                            proxyObject a = new proxyObject(line.Trim(), true);
                            proxies.Add(a);
                        }

                    }
                }
                catch (Exception ex) { }
            }
            updating = false;
            if (proxies.Count < 5)
                return true;
            else
                return false;
        }

        public static void updateThread(object a)
        {
            while (true)
            {
                Thread.Sleep(int.Parse(a.ToString()));
                updateList(URL);
            }
        }


        public static proxyObject getProxy()
        {
            if (proxies.Count < 5)
            {
                if (!updating)
                    updateList(URL);
                return null;
            }
            try
            {
                while (true)
                {
                    int index = proxyindex;
                    if (index > proxies.Count)
                    {
                        proxyindex = 0;
                        index = 0;
                        continue;
                    }

                    proxyindex++;
                    if (proxies[index].Working)
                        return proxies[index];
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
