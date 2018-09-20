using System;
using System.Collections.Generic;
using NETCONLib;
using System.Windows.Forms;

namespace WIFIHelper
{
    class NetShare
    {
        private INetSharingEveryConnectionCollection connections;
        private NetSharingManagerClass netSharingMgr;

        public NetConnection SharedConnection { get; set; }

        /*
                private void DisableSharing()
                {
                    connections = netSharingMgr.EnumEveryConnection;
                    foreach (INetConnection connection in connections)
                    {
                        INetSharingConfiguration connSharcf = netSharingMgr.INetSharingConfigurationForINetConnection[connection];
                        INetConnectionProps connProps = netSharingMgr.NetConnectionProps[connection];
                        try
                        {
                            if (connSharcf.SharingEnabled)
                                connSharcf.DisableSharing();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                }
        */

        public bool EnableSharing(NetConnection nc)
        {
            if (nc == null)
                return false;
            try
            {
                netSharingMgr = new NetSharingManagerClass();
                connections = netSharingMgr.EnumEveryConnection;
                foreach (INetConnection connection in connections)
                {
                    INetSharingConfiguration connSharcf = netSharingMgr.INetSharingConfigurationForINetConnection[connection];
                    INetConnectionProps connProps = netSharingMgr.NetConnectionProps[connection];
                    try
                    {
                        //判断要配置的网卡 Microsoft Virtual WiFi Miniport Adapter   Realtek PCIe GBE Family Controller

                        if (connProps.DeviceName.Equals(nc.DeviceName))
                        {
                            //配置WAN连接 
                            if (!connSharcf.SharingEnabled || connSharcf.SharingConnectionType != tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC)
                                connSharcf.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
                        }
                        else if (connProps.DeviceName.Contains("Virtual"))
                        {
                            //配置LAN连接
                            if (!connSharcf.SharingEnabled || connSharcf.SharingConnectionType != tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC)
                                connSharcf.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("网络共享出现不可预知的错误，请手动共享网络。\r\n\r\n错误信息：" + ex.Message, @"虚拟WIFI助手");
                        return false;
                    }
                }
                SharedConnection = nc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("网络共享出现不可预知的错误，请手动共享网络。\r\n\r\n错误信息：" + ex.Message, @"虚拟WIFI助手");
                return false;
            }
            return true;
        }

        public List<NetConnection> GetConnections()
        {
            try
            {
                netSharingMgr = new NetSharingManagerClass();
                connections = netSharingMgr.EnumEveryConnection;
                var strs = new List<NetConnection>();
                foreach (INetConnection connection in connections)
                {
                    INetSharingConfiguration connSharcf = netSharingMgr.INetSharingConfigurationForINetConnection[connection];
                    INetConnectionProps connProps = netSharingMgr.NetConnectionProps[connection];
                    var nc = new NetConnection(connProps.Name, connProps.DeviceName);
                    strs.Add(nc);

                    if (connSharcf.SharingEnabled && connSharcf.SharingConnectionType == tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC)
                    {
                        SharedConnection = nc;
                    }
                }
                return strs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("网络共享出现不可预知的错误，请手动共享网络。\r\n\r\n错误信息：" + ex.Message, @"虚拟WIFI助手"); 
                return null;
            }
        }
    }

    public class NetConnection
    {
        public NetConnection(string name, string dericeName)
        {
            this.name = name;
            this.dericeName = dericeName;
        }

        private readonly string name;

        public string Name
        {
            get { return name; }
        }
        private readonly string dericeName;

        public string DeviceName
        {
            get { return dericeName; }
        }

        public bool IsVirtual
        {
            get { return DeviceName.Contains("Virtual"); }
        }

        public override string ToString()
        {
            return this.name;
        }

        public static NetConnection Null
        {
            get
            {
                return new NetConnection(@"请选择一个用来共享的网络连接", "");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is NetConnection)
            {
                var nc = obj as NetConnection;
                return nc.Name.Equals(this.Name) && nc.DeviceName.Equals(this.DeviceName);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
