namespace WIFIHelper
{
    partial class FrmMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.dwmLabel1 = new DWMLib.DWMLabel();
            this.dwmLabel2 = new DWMLib.DWMLabel();
            this.dwmLabel3 = new DWMLib.DWMLabel();
            this.lblState = new DWMLib.DWMLabel();
            this.txtSSID = new DWMLib.DWMTextBox();
            this.txtKey = new DWMLib.DWMTextBox();
            this.chkShowKey = new DWMLib.DWMCheckBox();
            this.chkRun = new DWMLib.DWMCheckBox();
            this.btnOpenOrClose = new DWMLib.DWMButton();
            this.dwmLabel4 = new DWMLib.DWMLabel();
            this.btnExit = new DWMLib.DWMButton();
            this.dwmLabel5 = new DWMLib.DWMLabel();
            this.cmbConnections = new DWMLib.DWMComboBox();
            this.btnShowClient = new DWMLib.DWMButton();
            this.SuspendLayout();
            // 
            // dwmLabel1
            // 
            this.dwmLabel1.AutoSize = true;
            this.dwmLabel1.DWMBackColor = System.Drawing.SystemColors.Control;
            this.dwmLabel1.EnabledDWM = true;
            this.dwmLabel1.Location = new System.Drawing.Point(7, 14);
            this.dwmLabel1.Name = "dwmLabel1";
            this.dwmLabel1.Size = new System.Drawing.Size(89, 12);
            this.dwmLabel1.TabIndex = 14;
            this.dwmLabel1.Text = "无线网络SSID：";
            // 
            // dwmLabel2
            // 
            this.dwmLabel2.AutoSize = true;
            this.dwmLabel2.DWMBackColor = System.Drawing.SystemColors.Control;
            this.dwmLabel2.EnabledDWM = true;
            this.dwmLabel2.Location = new System.Drawing.Point(7, 45);
            this.dwmLabel2.Name = "dwmLabel2";
            this.dwmLabel2.Size = new System.Drawing.Size(83, 12);
            this.dwmLabel2.TabIndex = 15;
            this.dwmLabel2.Text = "无线网络KEY：";
            // 
            // dwmLabel3
            // 
            this.dwmLabel3.AutoSize = true;
            this.dwmLabel3.DWMBackColor = System.Drawing.SystemColors.Control;
            this.dwmLabel3.EnabledDWM = true;
            this.dwmLabel3.Location = new System.Drawing.Point(7, 107);
            this.dwmLabel3.Name = "dwmLabel3";
            this.dwmLabel3.Size = new System.Drawing.Size(65, 12);
            this.dwmLabel3.TabIndex = 16;
            this.dwmLabel3.Text = "当前状态：";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.DWMBackColor = System.Drawing.SystemColors.Control;
            this.lblState.EnabledDWM = true;
            this.lblState.ForeColor = System.Drawing.Color.Red;
            this.lblState.Location = new System.Drawing.Point(100, 107);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(59, 12);
            this.lblState.TabIndex = 17;
            this.lblState.Text = "dwmLabel4";
            // 
            // txtSSID
            // 
            this.txtSSID.DWMBackColor = System.Drawing.SystemColors.Control;
            this.txtSSID.EnabledDWM = true;
            this.txtSSID.Location = new System.Drawing.Point(102, 11);
            this.txtSSID.Name = "txtSSID";
            this.txtSSID.Size = new System.Drawing.Size(206, 21);
            this.txtSSID.TabIndex = 2;
            this.txtSSID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSSID_KeyPress);
            // 
            // txtKey
            // 
            this.txtKey.DWMBackColor = System.Drawing.SystemColors.Control;
            this.txtKey.EnabledDWM = true;
            this.txtKey.Location = new System.Drawing.Point(102, 42);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(128, 21);
            this.txtKey.TabIndex = 3;
            // 
            // chkShowKey
            // 
            this.chkShowKey.AutoSize = true;
            this.chkShowKey.DWMBackColor = System.Drawing.SystemColors.Control;
            this.chkShowKey.EnabledDWM = true;
            this.chkShowKey.Location = new System.Drawing.Point(236, 44);
            this.chkShowKey.Name = "chkShowKey";
            this.chkShowKey.Size = new System.Drawing.Size(72, 16);
            this.chkShowKey.TabIndex = 4;
            this.chkShowKey.Text = "明文密码";
            this.chkShowKey.UseVisualStyleBackColor = true;
            this.chkShowKey.Click += new System.EventHandler(this.chkShowKey_CheckedChanged);
            // 
            // chkRun
            // 
            this.chkRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRun.AutoSize = true;
            this.chkRun.DWMBackColor = System.Drawing.SystemColors.Control;
            this.chkRun.EnabledDWM = true;
            this.chkRun.Location = new System.Drawing.Point(9, 137);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(132, 16);
            this.chkRun.TabIndex = 6;
            this.chkRun.Text = "开机时自动打开热点";
            this.chkRun.UseVisualStyleBackColor = true;
            // 
            // btnOpenOrClose
            // 
            this.btnOpenOrClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenOrClose.DWMBackColor = System.Drawing.SystemColors.Control;
            this.btnOpenOrClose.EnabledDWM = true;
            this.btnOpenOrClose.Location = new System.Drawing.Point(108, 168);
            this.btnOpenOrClose.Name = "btnOpenOrClose";
            this.btnOpenOrClose.Size = new System.Drawing.Size(99, 23);
            this.btnOpenOrClose.TabIndex = 0;
            this.btnOpenOrClose.Text = "打开WIFI";
            this.btnOpenOrClose.UseVisualStyleBackColor = true;
            this.btnOpenOrClose.Click += new System.EventHandler(this.btnOpenOrClose_Click);
            // 
            // dwmLabel4
            // 
            this.dwmLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dwmLabel4.AutoSize = true;
            this.dwmLabel4.DWMBackColor = System.Drawing.SystemColors.Control;
            this.dwmLabel4.EnabledDWM = true;
            this.dwmLabel4.Location = new System.Drawing.Point(7, 173);
            this.dwmLabel4.Name = "dwmLabel4";
            this.dwmLabel4.Size = new System.Drawing.Size(53, 12);
            this.dwmLabel4.TabIndex = 18;
            this.dwmLabel4.Text = "小虾作品";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.DWMBackColor = System.Drawing.SystemColors.Control;
            this.btnExit.EnabledDWM = true;
            this.btnExit.Location = new System.Drawing.Point(233, 168);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dwmLabel5
            // 
            this.dwmLabel5.AutoSize = true;
            this.dwmLabel5.DWMBackColor = System.Drawing.SystemColors.Control;
            this.dwmLabel5.EnabledDWM = true;
            this.dwmLabel5.Location = new System.Drawing.Point(7, 76);
            this.dwmLabel5.Name = "dwmLabel5";
            this.dwmLabel5.Size = new System.Drawing.Size(65, 12);
            this.dwmLabel5.TabIndex = 21;
            this.dwmLabel5.Text = "上网通过：";
            // 
            // cmbConnections
            // 
            this.cmbConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnections.DWMBackColor = System.Drawing.SystemColors.Control;
            this.cmbConnections.EnabledDWM = true;
            this.cmbConnections.FormattingEnabled = true;
            this.cmbConnections.Location = new System.Drawing.Point(102, 73);
            this.cmbConnections.Name = "cmbConnections";
            this.cmbConnections.Size = new System.Drawing.Size(206, 20);
            this.cmbConnections.TabIndex = 5;
            // 
            // btnShowClient
            // 
            this.btnShowClient.DWMBackColor = System.Drawing.SystemColors.Control;
            this.btnShowClient.EnabledDWM = true;
            this.btnShowClient.Location = new System.Drawing.Point(233, 102);
            this.btnShowClient.Name = "btnShowClient";
            this.btnShowClient.Size = new System.Drawing.Size(75, 23);
            this.btnShowClient.TabIndex = 22;
            this.btnShowClient.Text = "查看客户端";
            this.btnShowClient.UseVisualStyleBackColor = true;
            this.btnShowClient.Visible = false;
            this.btnShowClient.Click += new System.EventHandler(this.btnShowClient_Click);
            // 
            // FrmMain
            // 
            this.AcceptButton = this.btnOpenOrClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(317, 198);
            this.Controls.Add(this.btnShowClient);
            this.Controls.Add(this.dwmLabel1);
            this.Controls.Add(this.cmbConnections);
            this.Controls.Add(this.dwmLabel2);
            this.Controls.Add(this.chkShowKey);
            this.Controls.Add(this.dwmLabel5);
            this.Controls.Add(this.chkRun);
            this.Controls.Add(this.dwmLabel3);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.dwmLabel4);
            this.Controls.Add(this.btnOpenOrClose);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.txtSSID);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "虚拟WIFI助手";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DWMLib.DWMLabel dwmLabel1;
        private DWMLib.DWMLabel dwmLabel2;
        private DWMLib.DWMLabel dwmLabel3;
        private DWMLib.DWMLabel lblState;
        private DWMLib.DWMTextBox txtSSID;
        private DWMLib.DWMTextBox txtKey;
        private DWMLib.DWMCheckBox chkShowKey;
        private DWMLib.DWMCheckBox chkRun;
        private DWMLib.DWMButton btnOpenOrClose;
        private DWMLib.DWMLabel dwmLabel4;
        private DWMLib.DWMButton btnExit;
        private DWMLib.DWMLabel dwmLabel5;
        private DWMLib.DWMComboBox cmbConnections;
        private DWMLib.DWMButton btnShowClient;
    }
}

