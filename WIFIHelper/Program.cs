using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;

namespace WIFIHelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            if (IsAdministrator())
            {
                MutexOnly.ShowMutexOnly(Run, args);
            }
            else
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    Arguments = string.Join(" ", args),
                    Verb = "runas"
                };
                Process.Start(startInfo);
            }
        }

        private static void Run(string[] args)
        {
            if (OSVersionIsNeedUpdate())
                return;
            DWMState = DWMLib.API.DwmIsCompositionEnabled();
            UseHelper.LoadSet();
            if (args.Length > 0 && args[0] == "-run")
            {
                if (!CMD.GetWifiState())
                {
                    CMD.RunAndOpenWIFI(UseHelper.ssid, UseHelper.key);
                    new NetShare().EnableSharing(new NetConnection(UseHelper.wanName, UseHelper.wanDericeName));
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
        }

        public static bool DWMState;

        private static bool OSVersionIsNeedUpdate()
        {
            bool flag = Environment.OSVersion.Version.Major < 6;
            if (flag)
                MessageBox.Show(@"您的操作系统版本较早，为避免遭遇蓝屏，
建议先将无线网卡驱动程序升级到最新版！", @"版本信息提示");
            return flag;
        }

        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }
    }
}
