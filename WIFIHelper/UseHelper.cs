using System;
using System.IO;
using System.Collections;

namespace WIFIHelper
{
    class UseHelper
    {
        public static string ssid, key;
        public static bool showKey, run;
        public static string wanName, wanDericeName;
        private static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public static void LoadSet()
        {
            FileStream fs = File.Open(filePath + "\\wifihelper.ini", FileMode.OpenOrCreate, FileAccess.Read);
            if (fs.Length == 0)
            {
                fs.Close();
            }
            else
            {
                StreamReader sr = new StreamReader(fs);
                Hashtable ht = new Hashtable();

                string s = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(s))
                {
                    string[] ss = s.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in ss)
                    {
                        string[] sstr = str.Split('=');
                        if (sstr.Length == 2)
                            ht.Add(sstr[0], sstr[1]);
                    }
                }
                sr.Close();
                fs.Close();

                if (ht.ContainsKey("ssid"))
                {
                    ssid = Convert.ToString(ht["ssid"]);
                }
                if (ht.ContainsKey("key"))
                {
                    key = Convert.ToString(ht["key"]);
                }
                if (ht.ContainsKey("showKey"))
                {
                    showKey = Convert.ToBoolean(ht["showKey"]);
                }
                if (ht.ContainsKey("run"))
                {
                    run = Convert.ToBoolean(ht["run"]);
                }
                if (ht.ContainsKey("wanName"))
                {
                    wanName = Convert.ToString(ht["wanName"]);
                }
                if (ht.ContainsKey("wanDericeName"))
                {
                    wanDericeName = Convert.ToString(ht["wanDericeName"]);
                }
            }
        }

        public static void SaveSet()
        {
            FileStream fs = File.Open(filePath + "\\wifihelper.ini", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("{0}={1}", "ssid", ssid);
            sw.WriteLine("{0}={1}", "key", key);
            sw.WriteLine("{0}={1}", "showKey", showKey);
            sw.WriteLine("{0}={1}", "run", run);
            sw.WriteLine("{0}={1}", "wanName", wanName);
            sw.WriteLine("{0}={1}", "wanDericeName", wanDericeName);
            sw.Close();
            fs.Close();
        }
    }
}
