using System;
using System.Windows.Forms;
using Microsoft.Win32;
using DWMLib;
using System.Drawing;
using System.Collections.Generic;

namespace WIFIHelper
{
    public partial class FrmMain : DWMForm
    {
        public FrmMain()
        {
            InitializeComponent();
            if (Program.DWMState)
            {
                this.SetDWMMargin(-1, 0, 0, 0);
            }
            else
            {
                lblState.EnabledDWM = false;
                chkRun.EnabledDWM = false;
                chkShowKey.EnabledDWM = false;
                dwmLabel1.EnabledDWM = false;
                dwmLabel2.EnabledDWM = false;
                dwmLabel3.EnabledDWM = false;
                dwmLabel4.EnabledDWM = false;
                dwmLabel5.EnabledDWM = false;
                txtKey.EnabledDWM = false;
                txtSSID.EnabledDWM = false;
                cmbConnections.EnabledDWM = false;
                btnExit.EnabledDWM = false;
                btnOpenOrClose.EnabledDWM = false;
                btnShowClient.EnabledDWM = false;
            }
            lblState.Text = "";
            this.Size = new Size(323, 226);
        }

        private bool wifiState;
        private readonly NetShare netShare = new NetShare();

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtSSID.Text = UseHelper.ssid;
            txtKey.Text = UseHelper.key;
            chkShowKey.Checked = UseHelper.showKey;
            chkRun.Checked = UseHelper.run;
            SetPwd(UseHelper.showKey);

            GetWifiState();
            SetBtnText();

            cmbConnections.Items.Add(NetConnection.Null);
            List<NetConnection> strs = netShare.GetConnections();
            if (strs != null && strs.Count > 0)
            {
                foreach (NetConnection nc in strs)
                {
                    if (!nc.IsVirtual)
                    {
                        cmbConnections.Items.Add(nc);
                    }
                }
                if (netShare.SharedConnection != null)
                    cmbConnections.SelectedItem = netShare.SharedConnection;
                else
                    cmbConnections.SelectedItem = NetConnection.Null;
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtSSID.Text.Trim()))
            {
                MessageBox.Show(@"SSID不能为空！", this.Text);
                txtSSID.Focus();
                return false;
            }
            if (txtKey.Text.Trim().Length < 8)
            {
                MessageBox.Show(@"KEY不能少于 8 位！", this.Text);
                txtKey.Focus();
                return false;
            }
            if (cmbConnections.SelectedIndex == 0)
            {
                MessageBox.Show(@"请选择一个用来访问因特网的网络连接！", this.Text);
                cmbConnections.Focus();
                return false;
            }
            return true;
        }

        private void btnOpenOrClose_Click(object sender, EventArgs e)
        {
            if (!wifiState)
            {
                if (!CheckInput())
                    return;

                if (CMD.OpenWIFI(txtSSID.Text.Trim(), txtKey.Text.Trim()))
                {
                    wifiState = true;
                    SetBtnText();

                    NetConnection nc = cmbConnections.SelectedItem as NetConnection;
                    netShare.EnableSharing(nc);
                }
            }
            else
            {
                if (CMD.CloseWIFI())
                {
                    wifiState = false;
                    SetBtnText();
                }
            }
        }

        private void SetBtnText()
        {
            if (wifiState)
            {
                btnOpenOrClose.Text = @"关闭WIFI";
                lblState.Text = @"已启用";
            }
            else
            {
                btnOpenOrClose.Text = @"打开WIFI";
                lblState.Text = @"未启用";
            }
            btnShowClient.Visible = wifiState;
        }

        private void GetWifiState()
        {
            NetshInfo ni = new NetshInfo();
            wifiState = CMD.GetWifiState(ref ni);
            if (!ni.IsEmpty)
            {
                txtSSID.Text = ni.SSID;
                txtKey.Text = ni.KEY;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowKey_CheckedChanged(object sender, EventArgs e)
        {
            SetPwd(chkShowKey.Checked);
        }

        private void SetPwd(bool flag)
        {
            txtKey.PasswordChar = flag ? '\0' : '*';
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Register(chkRun.Checked);
            UseHelper.ssid = txtSSID.Text.Trim();
            UseHelper.key = txtKey.Text.Trim();
            UseHelper.run = chkRun.Checked;
            UseHelper.showKey = chkShowKey.Checked;
            if (netShare.SharedConnection != null)
            {
                UseHelper.wanName = netShare.SharedConnection.Name;
                UseHelper.wanDericeName = netShare.SharedConnection.DeviceName;
            }
            UseHelper.SaveSet();
        }

        private void Register(bool isToRegister)
        {
            string title = "wifihelper";
            string value = string.Format("\"{0}\" -run", Application.ExecutablePath);
            RegistryKey runKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (runKey != null)
            {
                if (isToRegister)
                {
                    runKey.SetValue(title, value);
                }
                else
                {
                    string[] names = runKey.GetValueNames();
                    foreach (string str in names)
                    {
                        if (str == title)
                        {
                            runKey.DeleteValue(title);
                            break;
                        }
                    }
                }
                runKey.Close();
            }
        }

        private void txtSSID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\"')
                e.Handled = true;
        }

        private void btnShowClient_Click(object sender, EventArgs e)
        {
            NetshInfo ni = new NetshInfo();
            wifiState = CMD.GetWifiState(ref ni);
            string text = string.Format("客户端数：{0}", ni.ClientCount);
            text += Environment.NewLine;
            text += "序号\tMAC";
            text += Environment.NewLine;
            text += "---------------------------------";
            for (int i = 0; i < ni.MAC.Count; i++)
            {
                text += string.Format("{2}{0}\t{1}", i + 1, ni.MAC[i], Environment.NewLine);
            }

            MessageBox.Show(text, @"当前连接客户端");
        }
    }
}
