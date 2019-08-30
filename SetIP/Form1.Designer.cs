namespace SetIp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDestIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.tbCurrentStatus = new System.Windows.Forms.Label();
            this.lbNic = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtSubMask = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGateway = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrimaryDns = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBackupDns = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblIpResult = new System.Windows.Forms.Label();
            this.lblSubMaskResult = new System.Windows.Forms.Label();
            this.lblGatewayResult = new System.Windows.Forms.Label();
            this.lblDns1Result = new System.Windows.Forms.Label();
            this.lblDns2Result = new System.Windows.Forms.Label();
            this.btnOpenNetworkConnections = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDestIp
            // 
            this.txtDestIp.Location = new System.Drawing.Point(171, 365);
            this.txtDestIp.Name = "txtDestIp";
            this.txtDestIp.Size = new System.Drawing.Size(100, 21);
            this.txtDestIp.TabIndex = 1;
            this.txtDestIp.Text = "192.168.44.163";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(171, 395);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 2;
            this.txtPort.Text = "10001";
            // 
            // tbCurrentStatus
            // 
            this.tbCurrentStatus.BackColor = System.Drawing.Color.Gainsboro;
            this.tbCurrentStatus.Location = new System.Drawing.Point(20, 447);
            this.tbCurrentStatus.Name = "tbCurrentStatus";
            this.tbCurrentStatus.Size = new System.Drawing.Size(507, 14);
            this.tbCurrentStatus.TabIndex = 3;
            this.tbCurrentStatus.Text = "结果";
            // 
            // lbNic
            // 
            this.lbNic.FormattingEnabled = true;
            this.lbNic.ItemHeight = 12;
            this.lbNic.Location = new System.Drawing.Point(22, 35);
            this.lbNic.Name = "lbNic";
            this.lbNic.Size = new System.Drawing.Size(505, 136);
            this.lbNic.TabIndex = 5;
            this.lbNic.SelectedIndexChanged += new System.EventHandler(this.LbNic_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP地址:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(171, 187);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(100, 21);
            this.txtIp.TabIndex = 7;
            // 
            // txtSubMask
            // 
            this.txtSubMask.Location = new System.Drawing.Point(171, 214);
            this.txtSubMask.Name = "txtSubMask";
            this.txtSubMask.Size = new System.Drawing.Size(100, 21);
            this.txtSubMask.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "子网掩码:";
            // 
            // txtGateway
            // 
            this.txtGateway.Location = new System.Drawing.Point(171, 241);
            this.txtGateway.Name = "txtGateway";
            this.txtGateway.Size = new System.Drawing.Size(100, 21);
            this.txtGateway.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "默认网关:";
            // 
            // txtPrimaryDns
            // 
            this.txtPrimaryDns.Location = new System.Drawing.Point(171, 268);
            this.txtPrimaryDns.Name = "txtPrimaryDns";
            this.txtPrimaryDns.Size = new System.Drawing.Size(100, 21);
            this.txtPrimaryDns.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "首选DNS:";
            // 
            // txtBackupDns
            // 
            this.txtBackupDns.Location = new System.Drawing.Point(171, 295);
            this.txtBackupDns.Name = "txtBackupDns";
            this.txtBackupDns.Size = new System.Drawing.Size(100, 21);
            this.txtBackupDns.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "备用DNS:";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(84, 328);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 16;
            this.btnSet.Text = "设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.BtnSet_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 368);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "下位机IP:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 398);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "下位机端口:";
            // 
            // lblIpResult
            // 
            this.lblIpResult.AutoSize = true;
            this.lblIpResult.Location = new System.Drawing.Point(304, 196);
            this.lblIpResult.Name = "lblIpResult";
            this.lblIpResult.Size = new System.Drawing.Size(17, 12);
            this.lblIpResult.TabIndex = 19;
            this.lblIpResult.Text = "ip";
            // 
            // lblSubMaskResult
            // 
            this.lblSubMaskResult.AutoSize = true;
            this.lblSubMaskResult.Location = new System.Drawing.Point(304, 223);
            this.lblSubMaskResult.Name = "lblSubMaskResult";
            this.lblSubMaskResult.Size = new System.Drawing.Size(47, 12);
            this.lblSubMaskResult.TabIndex = 20;
            this.lblSubMaskResult.Text = "submask";
            // 
            // lblGatewayResult
            // 
            this.lblGatewayResult.AutoSize = true;
            this.lblGatewayResult.Location = new System.Drawing.Point(304, 250);
            this.lblGatewayResult.Name = "lblGatewayResult";
            this.lblGatewayResult.Size = new System.Drawing.Size(41, 12);
            this.lblGatewayResult.TabIndex = 21;
            this.lblGatewayResult.Text = "gatway";
            // 
            // lblDns1Result
            // 
            this.lblDns1Result.AutoSize = true;
            this.lblDns1Result.Location = new System.Drawing.Point(304, 277);
            this.lblDns1Result.Name = "lblDns1Result";
            this.lblDns1Result.Size = new System.Drawing.Size(29, 12);
            this.lblDns1Result.TabIndex = 22;
            this.lblDns1Result.Text = "dns1";
            // 
            // lblDns2Result
            // 
            this.lblDns2Result.AutoSize = true;
            this.lblDns2Result.Location = new System.Drawing.Point(304, 304);
            this.lblDns2Result.Name = "lblDns2Result";
            this.lblDns2Result.Size = new System.Drawing.Size(29, 12);
            this.lblDns2Result.TabIndex = 23;
            this.lblDns2Result.Text = "dns2";
            // 
            // btnOpenNetworkConnections
            // 
            this.btnOpenNetworkConnections.Location = new System.Drawing.Point(22, 4);
            this.btnOpenNetworkConnections.Name = "btnOpenNetworkConnections";
            this.btnOpenNetworkConnections.Size = new System.Drawing.Size(123, 23);
            this.btnOpenNetworkConnections.TabIndex = 24;
            this.btnOpenNetworkConnections.Text = "打开系统网络连接";
            this.btnOpenNetworkConnections.UseVisualStyleBackColor = true;
            this.btnOpenNetworkConnections.Click += new System.EventHandler(this.BtnOpenNetworkConnections_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 484);
            this.Controls.Add(this.btnOpenNetworkConnections);
            this.Controls.Add(this.lblDns2Result);
            this.Controls.Add(this.lblDns1Result);
            this.Controls.Add(this.lblGatewayResult);
            this.Controls.Add(this.lblSubMaskResult);
            this.Controls.Add(this.lblIpResult);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.txtBackupDns);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPrimaryDns);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGateway);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSubMask);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbNic);
            this.Controls.Add(this.tbCurrentStatus);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtDestIp);
            this.Name = "Form1";
            this.Text = "网络设置";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtDestIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label tbCurrentStatus;
        private System.Windows.Forms.ListBox lbNic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtSubMask;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGateway;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrimaryDns;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBackupDns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblIpResult;
        private System.Windows.Forms.Label lblSubMaskResult;
        private System.Windows.Forms.Label lblGatewayResult;
        private System.Windows.Forms.Label lblDns1Result;
        private System.Windows.Forms.Label lblDns2Result;
        private System.Windows.Forms.Button btnOpenNetworkConnections;
    }
}

