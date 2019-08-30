using System;
using System.Diagnostics;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace SetIp
{
    public partial class Form1 : Form
    {
        Timer _timer;
        private SynchronizationContext _context;
        private int _counterSuccess;
        private int _counterFail;

        public Form1()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
            _context = _context ?? new SynchronizationContext();
        }

        private async void TestConnection(object obj)
        {

            // 测试连接
            var timeOut = TimeSpan.FromMilliseconds(200);
            var cancellationCompletionSource = new TaskCompletionSource<bool>();
            try
            {
                IPAddress ip;
                bool b = IPAddress.TryParse(txtDestIp.Text, out ip);
                if (!b)
                {
                    throw new ArgumentException();
                }

                var port = Convert.ToInt32(txtPort.Text);

                using (var cts = new CancellationTokenSource(timeOut))
                {
                    using (var client = new TcpClient())
                    {
                        var task = client.ConnectAsync(ip, port);

                        using (cts.Token.Register(() => cancellationCompletionSource.TrySetResult(true)))
                        {
                            if (task != await Task.WhenAny(task, cancellationCompletionSource.Task))
                            {
                                throw new OperationCanceledException(cts.Token);
                            }
                        }

                        client.Close();
                        _context.Post(o =>
                        {
                            _counterSuccess++;
                            tbCurrentStatus.Text = "连接成功！" + _counterSuccess;
                        }, null);
                    }
                }
            }
            catch (Exception)
            {
                _context.Post(o =>
                {
                    _counterFail++;
                    tbCurrentStatus.Text = "连接失败 " + _counterFail;
                }, null);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblIpResult.Text = "";
            lblSubMaskResult.Text = "";
            lblGatewayResult.Text = "";
            lblDns1Result.Text = "";
            lblDns2Result.Text = "";

            btnSet.Enabled = false;
            GetNetAdapters();
            _timer = new Timer(TestConnection, null, 0, 1000);
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

        private void BtnSet_Click(object sender, EventArgs e)
        {
            btnSet.Enabled = false;
            lblIpResult.Text = "";
            lblSubMaskResult.Text = "";
            lblGatewayResult.Text = "";
            lblDns1Result.Text = "";
            lblDns2Result.Text = "";

            if (string.IsNullOrEmpty(txtIp.Text) || string.IsNullOrEmpty(txtSubMask.Text))
            {
                lblIpResult.Text = "不能为空";
                lblSubMaskResult.Text = "不能为空";

                return;
            }

            SetIpInfo(lbNic.SelectedItem.ToString(),
                new[] { txtIp.Text },
                new[] { txtSubMask.Text }, new[] { txtGateway.Text },
                new[] { txtPrimaryDns.Text, txtBackupDns.Text });
            btnSet.Enabled = true;
        }

        private void LbNic_SelectedIndexChanged(object sender, EventArgs e)
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
                    btnSet.Enabled = true;
                }
                else
                {
                    btnSet.Enabled = false;
                }
            }
            else
            {
                btnSet.Enabled = false;
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
                    catch (Exception er)
                    {
                        //throw er;
                    }
                }
            }
        }

        protected void SetIpInfo(string nic, string[] ip, string[] submask, string[] gateway, string[] dns)
        {
            string str = "";
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"]) continue;
                if (mo["Caption"].ToString().Contains(nic))
                {
                    if (ip != null && submask != null)
                    {
                        inPar = mo.GetMethodParameters("EnableStatic");
                        inPar["IPAddress"] = ip;
                        inPar["SubnetMask"] = submask;
                        outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                        str = outPar["returnvalue"].ToString();
                        if (str == "0" || str == "1")
                        {
                            lblIpResult.Text = "成功";
                            lblSubMaskResult.Text = "成功";
                        }
                        else
                        {
                            lblIpResult.Text = "失败";
                            lblSubMaskResult.Text = "失败";
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
                            lblGatewayResult.Text = "成功";
                        }
                        else
                        {
                            lblGatewayResult.Text = "失败";
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
                            lblDns1Result.Text = "成功";
                            lblDns2Result.Text = "成功";
                        }
                        else
                        {
                            lblDns1Result.Text = "失败";
                            lblDns2Result.Text = "失败";
                        }
                    }
                }
            }
        }

        private void BtnOpenNetworkConnections_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo("NCPA.cpl");
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
        }
    }
}
