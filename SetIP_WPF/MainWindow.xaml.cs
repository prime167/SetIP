using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SetIP_WPF
{
    public partial class MainWindow : Window
    {
        Timer _timer;
        private readonly Dispatcher _dispatcher;
        private int _counterSuccess;
        private int _counterFail;
        static readonly object Locker = new object();

        public MainWindow()
        {
            InitializeComponent();
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        private void BtnOpenNetworkConnections_OnClick(object sender, RoutedEventArgs e)
        {
            var startInfo = new ProcessStartInfo("ncpa.cpl");
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            lblIpResult.Content = "";
            lblSubMaskResult.Content = "";
            lblGatewayResult.Content = "";
            lblDns1Result.Content = "";
            lblDns2Result.Content = "";

            btnSet.IsEnabled = false;
            GetNetAdapters();

            tbCurrentStatus.Text = "";
            _timer = new Timer(TestConnection, null, 300, 1000);
        }

        private void TestConnection(object obj)
        {
            lock (Locker)
            {
                bool r = TestConnection();
                if (r)
                {
                    _counterSuccess++;
                    _dispatcher.BeginInvoke((Action)(() => { tbCurrentStatus.Text = $"连接成功 {_counterSuccess}"; }));
                }
                else
                {
                    _counterFail++;
                    _dispatcher.BeginInvoke((Action)(() => { tbCurrentStatus.Text = $"连接失败 {_counterFail}"; }));
                }
            }
        }

        private bool TestConnection()
        {
            // 测试连接
            var timeOut = TimeSpan.FromMilliseconds(60);
            try
            {
                string ips = "";
                bool r = true;
                int port = 0;
                IPAddress ip;
                _dispatcher.Invoke(() =>
                {
                    if (string.IsNullOrEmpty(txtDestIp.Text) || string.IsNullOrEmpty(txtPort.Text))
                    {
                        r = false;
                    }

                    r = IPAddress.TryParse(txtDestIp.Text, out ip);
                    r = int.TryParse(txtPort.Text, out port);

                    if (ip == null)
                    {
                        r = false;
                    }
                    else
                    {
                        ips = ip.ToString();
                    }
                });

                if (r == false)
                {
                    return false;
                }

                var client = new TcpClient();
                var result = client.BeginConnect(ips, port, null, null);

                var success = result.AsyncWaitHandle.WaitOne(timeOut);

                if (!success)
                {
                    return false;
                }

                client.EndConnect(result);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void GetNetAdapters()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    lbNic.Items.Add(nic.Description);
                }
            }
        }

        private void LbNic_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbNic.SelectedItem != null)
            {
                var nic = lbNic.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(nic))
                {
                    txtIp.Text = "";
                    txtSubMask.Text = "";
                    txtGateway.Text = "";
                    txtPrimaryDns.Text = "";
                    txtBackupDns.Text = "";

                    GetNetworkConfig(nic);
                    btnSet.IsEnabled = true;
                }
                else
                {
                    btnSet.IsEnabled = false;
                }
            }
            else
            {
                btnSet.IsEnabled = false;
            }
        }

        private void GetNetworkConfig(string nic)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.Description != nic)
                {
                    continue;
                }

                IPInterfaceProperties ips = adapter.GetIPProperties();     //IP配置信息
                foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        txtIp.Text = ip.Address.ToString();//IP地址
                        txtSubMask.Text = ip.IPv4Mask.ToString();//子网掩码
                    }
                }

                if (ips.GatewayAddresses.Count > 0)
                {
                    txtGateway.Text = ips.GatewayAddresses[0].Address.ToString();//默认网关
                }

                int dnsCount = ips.DnsAddresses.Count;
                if (dnsCount > 0)
                {
                    try
                    {
                        txtPrimaryDns.Text = ips.DnsAddresses[0].ToString(); //主DNS
                        txtBackupDns.Text = ips.DnsAddresses[1].ToString(); //备用DNS地址
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                    }
                }
            }
        }

        private void BtnSet_OnClick(object sender, RoutedEventArgs e)
        {
            btnSet.IsEnabled = false;
            lblIpResult.Content = "";
            lblSubMaskResult.Content = "";
            lblGatewayResult.Content = "";
            lblDns1Result.Content = "";
            lblDns2Result.Content = "";

            if (string.IsNullOrEmpty(txtIp.Text) || string.IsNullOrEmpty(txtSubMask.Text))
            {
                lblIpResult.Content = "不能为空";
                lblSubMaskResult.Content = "不能为空";

                return;
            }

            SetIpInfo(lbNic.SelectedItem.ToString(),
                new[] { txtIp.Text },
                new[] { txtSubMask.Text }, new[] { txtGateway.Text },
                new[] { txtPrimaryDns.Text, txtBackupDns.Text });
            btnSet.IsEnabled = true;
        }

        protected void SetIpInfo(string nic, string[] ip, string[] submask, string[] gateway, string[] dns)
        {
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"]) continue;
                if (mo["Caption"].ToString().Contains(nic))
                {
                    string str;
                    ManagementBaseObject inPar;
                    ManagementBaseObject outPar;
                    if (ip != null && submask != null)
                    {
                        inPar = mo.GetMethodParameters("EnableStatic");
                        inPar["IPAddress"] = ip;
                        inPar["SubnetMask"] = submask;
                        outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                        str = outPar["returnvalue"].ToString();
                        if (str == "0" || str == "1")
                        {
                            lblIpResult.Content = "成功";
                            lblSubMaskResult.Content = "成功";
                        }
                        else
                        {
                            lblIpResult.Content = "失败";
                            lblSubMaskResult.Content = "失败";
                        }
                        //获取操作设置IP的返回值， 可根据返回值去确认IP是否设置成功。 0或1表示成功 
                        // 返回值说明网址： https://msdn.microsoft.com/en-us/library/aa393301(v=vs.85).aspx
                    }

                    if (gateway != null)
                    {
                        inPar = mo.GetMethodParameters("SetGateways");
                        inPar["DefaultIPGateway"] = gateway;
                        outPar = mo.InvokeMethod("SetGateways", inPar, null);
                        str = outPar["returnvalue"].ToString();
                        if (str == "0" || str == "1")
                        {
                            lblGatewayResult.Content = "成功";
                        }
                        else
                        {
                            lblGatewayResult.Content = "失败";
                        }
                    }

                    if (dns != null)
                    {
                        inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        inPar["DNSServerSearchOrder"] = dns;
                        outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                        str = outPar["returnvalue"].ToString();
                        if (str == "0" || str == "1")
                        {
                            lblDns1Result.Content = "成功";
                            lblDns2Result.Content = "成功";
                        }
                        else
                        {
                            lblDns1Result.Content = "失败";
                            lblDns2Result.Content = "失败";
                        }
                    }
                }
            }
        }

        private void Hlb_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var address = Dns.GetHostEntry("www.baidu.com").AddressList;
                if (address.Any())
                {
                    txtDestIp.Text = address[0].ToString();
                    txtPort.Text = "80";
                }
            }
            catch (Exception exception)
            {
                txtDestIp.Text = "获取IP地址失败!";
            }
        }

        private void Hlg_OnClick(object sender, RoutedEventArgs e)
        {
            txtDestIp.Text = "74.125.236.199";
            txtPort.Text = "80";
        }
    }
}
