
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

namespace WIFIHelper
{
    public class CMD
    {
        static readonly Dictionary<string, Interface> lstInterface = new Dictionary<string, Interface>();

        static CMD()
        {
            lstInterface = GetInterfaces();
        }

        public static string ExecuteDOS(string cmd)
        {
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("@echo off");
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            string result = process.StandardOutput.ReadToEnd();
            return result.Substring(result.IndexOf(cmd, StringComparison.Ordinal));
        }

        public static string ExecuteDOS(string[] cmds)
        {
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("@echo off");
            foreach (string cmd in cmds)
            {
                process.StandardInput.WriteLine(cmd);
            }
            process.StandardInput.WriteLine("exit");
            string result = process.StandardOutput.ReadToEnd();
            return result.Substring(result.IndexOf(cmds[0], StringComparison.Ordinal));
        }

        public static void RunAndOpenWIFI(string ssid, string key)
        {
            CloseInterface();
            OpenWIFI(ssid, key);
        }

        public static void CloseInterface()
        {
            foreach (Interface item in lstInterface.Values)
            {
                item.SetInterface(true);
            }
            CloseWIFI();
        }

        public static bool OpenWIFI(string ssid, string key)
        {
            foreach (Interface item in lstInterface.Values)
            {
                item.SetInterface(true);
            }
            string cmd = string.Format("netsh wlan set hostednetwork mode=allow ssid={0} key={1}", ssid, key);
            string[] cmds = { cmd, "netsh wlan start hostednetwork" };
            string result = ExecuteDOS(cmds);
            return result.Contains("已启动承载网络");
        }

        public static bool CloseWIFI()
        {
            string[] cmds = { "netsh wlan stop hostednetwork", "netsh wlan set hostednetwork mode=disallow" };

            string result = ExecuteDOS(cmds);
            return result.Contains("已停止承载网络");
        }

        public static bool GetWifiState()
        {
            const string cmd = "netsh wlan show hostednetwork";
            string result = ExecuteDOS(cmd);
            return result.Contains("已启用") && result.Contains("已启动");
        }

        private static bool CheckHashtable(Hashtable ht, object key, object value)
        {
            if (ht.ContainsKey(key) && ht[key].Equals(value))
                return true;
            return false;
        }

        private static string GetHashString(Hashtable ht, object key)
        {
            if (ht.ContainsKey(key))
                return ht[key].ToString();
            return string.Empty;
        }

        private static int GetHashInt32(Hashtable ht, object key)
        {
            int v = 0;
            if (ht.ContainsKey(key))
                int.TryParse(ht[key].ToString(), out v);
            return v;
        }

        public static bool GetWifiState(ref NetshInfo ni)
        {
            const string cmd = "netsh wlan show hostednetwork";
            string result = ExecuteDOS(cmd);
            string[] res = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var ht = new Hashtable();
            const string pattern = @"^([a-fA-F\d]{2})(([/\s:-][a-fA-F\d]{2}){5})";
            var lstMac = new List<string>();
            for (int i = 0; i < res.Length; i++)
            {
                string line = res[i];
                Match match = Regex.Match(line.Trim(), pattern);
                if (match.Success)
                {
                    lstMac.Add(match.ToString());
                }
                else
                {
                    string[] ls = line.Split(new[] { ':', '：' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (ls.Length == 2)
                    {
                        if (!ht.ContainsKey(ls[0].Trim()))
                            ht.Add(ls[0].Trim(), ls[1].Trim(new[] { '“', '”', '\r', '\n', ' ' }));
                    }
                }
            }
            GetNetshInfo(ht, ni);
            ni.MAC = lstMac;
            return CheckHashtable(ht, "模式", "已启用") && CheckHashtable(ht, "状态", "已启动");
        }

        private static void GetNetshInfo(Hashtable ht, NetshInfo ni)
        {
            ni.SSID = GetHashString(ht, "SSID 名称");
            if (!ni.IsEmpty)
            {
                ni.BSSID = GetHashString(ht, "BSSID");
                ni.Channel = GetHashString(ht, "频道");
                ni.ClientCount = GetHashInt32(ht, "客户端数");
                ni.IdentityAuth = GetHashString(ht, "身份验证");
                ni.MaxClientCount = GetHashInt32(ht, "最多客户端数");
                ni.Password = GetHashString(ht, "密码");
                ni.WiFiType = GetHashString(ht, "无线电类型");
                GetWifiKey(ref ht, ref ni);
            }
            ni.HashTable = ht;
        }

        private static void GetWifiKey(ref Hashtable ht, ref NetshInfo ni)
        {
            const string cmdkey = "netsh wlan show hostednetwork security";
            string rekey = ExecuteDOS(cmdkey);
            string[] res = rekey.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in res)
            {
                string[] ls = line.Split(new[] { ':', '：' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (ls.Length == 2)
                {
                    if (!ht.ContainsKey(ls[0].Trim()))
                        ht.Add(ls[0].Trim(), ls[1].Trim(new[] { '“', '”', '\r', '\n', ' ' }));
                }
            }
            ni.KeyUsage = GetHashString(ht, "用户安全密钥用法");
            ni.SystemKey = GetHashString(ht, "系统安全密钥");
            ni.KEY = GetHashString(ht, "用户安全密钥");
        }

        public static Dictionary<string, Interface> GetInterfaces()
        {
            string cmd = string.Format("netsh interface show interface");
            string result = ExecuteDOS(cmd);
            string[] rs = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var lst = new Dictionary<string, Interface>();
            bool flag = false;
            for (int i = 0; i < rs.Length; i++)
            {
                string ifstr = rs[i];
                if (!flag)
                {
                    if (string.IsNullOrEmpty(ifstr.Replace("-", "")))
                        flag = true;
                }
                else
                {
                    string[] ifs = ifstr.Split(new[] { ' ', '\r', '\n' }, 4, StringSplitOptions.RemoveEmptyEntries);
                    if (ifs.Length == 4)
                    {
                        var item = new Interface
                        {
                            Name = ifs[3],
                            Type = ifs[2],
                            State = ifs[1] == "已连接",
                            AdminState = ifs[0] == "已启用"
                        };
                        lst.Add(item.Name, item);
                    }
                }
            }
            return lst;
        }

        public class Interface
        {
            public string Name;
            public string Type;
            public bool State;
            public bool AdminState;

            public bool SetInterface(bool enabled)
            {
                if (AdminState != enabled)
                {
                    string cmd = string.Format("netsh interface set interface name=\"{0}\" admin={1}",
                        this.Name, enabled ? "enabled" : "disabled");
                    string result = ExecuteDOS(cmd);
                    if (string.IsNullOrEmpty(result))
                    {
                        AdminState = enabled;
                    }
                }
                return AdminState;
            }
        }
    }

    public class NetshInfo
    {
        public bool IsEmpty { get { return string.IsNullOrEmpty(SSID); } }
        public string SSID { get; set; }
        public string KEY { get; set; }
        public int ClientCount { get; set; }
        public int MaxClientCount { get; set; }
        public List<string> MAC { get; set; }
        public string IdentityAuth { get; set; }
        public string Password { get; set; }
        public string BSSID { get; set; }
        public string WiFiType { get; set; }
        public string Channel { get; set; }
        public string SystemKey { get; set; }
        public string KeyUsage { get; set; }
        public Hashtable HashTable { get; set; }
    }
}
