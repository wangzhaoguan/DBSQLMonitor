namespace DBSQLMonitor
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.lblDBName = new System.Windows.Forms.Label();
            this.DbNameTextBox = new System.Windows.Forms.TextBox();
            this.lblDBUser = new System.Windows.Forms.Label();
            this.DbUserTextBox = new System.Windows.Forms.TextBox();
            this.lblMonitorMode = new System.Windows.Forms.Label();
            this.lblMonitorInteval = new System.Windows.Forms.Label();
            this.lblBlockTime = new System.Windows.Forms.Label();
            this.MonitorIntervalMumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.BlockTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.MonitorModeComboBox = new System.Windows.Forms.ComboBox();
            this.btnModifyDB = new System.Windows.Forms.Button();
            this.KillBlockCheckBox = new System.Windows.Forms.CheckBox();
            this.lblLogPath = new System.Windows.Forms.Label();
            this.LogPathTextBox = new System.Windows.Forms.TextBox();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.LogAutoSaveCheckBox = new System.Windows.Forms.CheckBox();
            this.btnMonitorDB = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.idx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.spid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.kpid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.blocked = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastWaittype = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.waitResource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.waitSeconds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.programName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hostProcess = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.remarks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sqlText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listviewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.stopDateTime = new System.Windows.Forms.DateTimePicker();
            this.CPU = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logicalReads = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.diskReads = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.elapsed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.MonitorIntervalMumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlockTimeNumericUpDown)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.listviewContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDBName
            // 
            this.lblDBName.AutoSize = true;
            this.lblDBName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDBName.Location = new System.Drawing.Point(12, 12);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new System.Drawing.Size(59, 12);
            this.lblDBName.TabIndex = 0;
            this.lblDBName.Text = "DB服务器:";
            // 
            // DbNameTextBox
            // 
            this.DbNameTextBox.Location = new System.Drawing.Point(71, 8);
            this.DbNameTextBox.Name = "DbNameTextBox";
            this.DbNameTextBox.Size = new System.Drawing.Size(202, 21);
            this.DbNameTextBox.TabIndex = 1;
            this.DbNameTextBox.Text = "192.168.0.1/ora11r2";
            // 
            // lblDBUser
            // 
            this.lblDBUser.AutoSize = true;
            this.lblDBUser.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDBUser.Location = new System.Drawing.Point(313, 12);
            this.lblDBUser.Name = "lblDBUser";
            this.lblDBUser.Size = new System.Drawing.Size(59, 12);
            this.lblDBUser.TabIndex = 2;
            this.lblDBUser.Text = "DB用户名:";
            // 
            // DbUserTextBox
            // 
            this.DbUserTextBox.Location = new System.Drawing.Point(370, 8);
            this.DbUserTextBox.Name = "DbUserTextBox";
            this.DbUserTextBox.Size = new System.Drawing.Size(153, 21);
            this.DbUserTextBox.TabIndex = 2;
            // 
            // lblMonitorMode
            // 
            this.lblMonitorMode.AutoSize = true;
            this.lblMonitorMode.Location = new System.Drawing.Point(12, 38);
            this.lblMonitorMode.Name = "lblMonitorMode";
            this.lblMonitorMode.Size = new System.Drawing.Size(59, 12);
            this.lblMonitorMode.TabIndex = 4;
            this.lblMonitorMode.Text = "监控方式:";
            // 
            // lblMonitorInteval
            // 
            this.lblMonitorInteval.AutoSize = true;
            this.lblMonitorInteval.Location = new System.Drawing.Point(174, 38);
            this.lblMonitorInteval.Name = "lblMonitorInteval";
            this.lblMonitorInteval.Size = new System.Drawing.Size(59, 12);
            this.lblMonitorInteval.TabIndex = 5;
            this.lblMonitorInteval.Text = "监控间隔:";
            // 
            // lblBlockTime
            // 
            this.lblBlockTime.AutoSize = true;
            this.lblBlockTime.Location = new System.Drawing.Point(313, 38);
            this.lblBlockTime.Name = "lblBlockTime";
            this.lblBlockTime.Size = new System.Drawing.Size(59, 12);
            this.lblBlockTime.TabIndex = 4;
            this.lblBlockTime.Text = "持续时间:";
            // 
            // MonitorIntervalMumericUpDown
            // 
            this.MonitorIntervalMumericUpDown.Location = new System.Drawing.Point(233, 33);
            this.MonitorIntervalMumericUpDown.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.MonitorIntervalMumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.MonitorIntervalMumericUpDown.Name = "MonitorIntervalMumericUpDown";
            this.MonitorIntervalMumericUpDown.Size = new System.Drawing.Size(40, 21);
            this.MonitorIntervalMumericUpDown.TabIndex = 3;
            this.MonitorIntervalMumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(273, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "秒";
            // 
            // BlockTimeNumericUpDown
            // 
            this.BlockTimeNumericUpDown.Location = new System.Drawing.Point(370, 33);
            this.BlockTimeNumericUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.BlockTimeNumericUpDown.Name = "BlockTimeNumericUpDown";
            this.BlockTimeNumericUpDown.Size = new System.Drawing.Size(41, 21);
            this.BlockTimeNumericUpDown.TabIndex = 5;
            this.BlockTimeNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(412, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "秒";
            // 
            // MonitorModeComboBox
            // 
            this.MonitorModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MonitorModeComboBox.FormattingEnabled = true;
            this.MonitorModeComboBox.Items.AddRange(new object[] {
            "仅阻塞",
            "活动会话",
            "所有会话"});
            this.MonitorModeComboBox.Location = new System.Drawing.Point(71, 35);
            this.MonitorModeComboBox.Name = "MonitorModeComboBox";
            this.MonitorModeComboBox.Size = new System.Drawing.Size(74, 20);
            this.MonitorModeComboBox.TabIndex = 4;
            this.MonitorModeComboBox.SelectedIndexChanged += new System.EventHandler(this.MonitorModeComboBox_SelectedIndexChanged);
            // 
            // btnModifyDB
            // 
            this.btnModifyDB.Location = new System.Drawing.Point(545, 6);
            this.btnModifyDB.Name = "btnModifyDB";
            this.btnModifyDB.Size = new System.Drawing.Size(69, 23);
            this.btnModifyDB.TabIndex = 21;
            this.btnModifyDB.Text = "修改";
            this.btnModifyDB.UseVisualStyleBackColor = true;
            this.btnModifyDB.Click += new System.EventHandler(this.btnModifyDB_Click);
            // 
            // KillBlockCheckBox
            // 
            this.KillBlockCheckBox.AutoSize = true;
            this.KillBlockCheckBox.ForeColor = System.Drawing.Color.Red;
            this.KillBlockCheckBox.Location = new System.Drawing.Point(446, 37);
            this.KillBlockCheckBox.Name = "KillBlockCheckBox";
            this.KillBlockCheckBox.Size = new System.Drawing.Size(84, 16);
            this.KillBlockCheckBox.TabIndex = 99;
            this.KillBlockCheckBox.Text = "Kill阻塞者";
            this.KillBlockCheckBox.UseVisualStyleBackColor = true;
            // 
            // lblLogPath
            // 
            this.lblLogPath.AutoSize = true;
            this.lblLogPath.Location = new System.Drawing.Point(12, 64);
            this.lblLogPath.Name = "lblLogPath";
            this.lblLogPath.Size = new System.Drawing.Size(59, 12);
            this.lblLogPath.TabIndex = 10;
            this.lblLogPath.Text = "日志目录:";
            // 
            // LogPathTextBox
            // 
            this.LogPathTextBox.Location = new System.Drawing.Point(71, 60);
            this.LogPathTextBox.Name = "LogPathTextBox";
            this.LogPathTextBox.Size = new System.Drawing.Size(140, 21);
            this.LogPathTextBox.TabIndex = 17;
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Location = new System.Drawing.Point(545, 58);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(69, 23);
            this.btnSaveLog.TabIndex = 32;
            this.btnSaveLog.Text = "保存";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(545, 105);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(69, 23);
            this.btnClearLog.TabIndex = 31;
            this.btnClearLog.Text = "清空";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // LogAutoSaveCheckBox
            // 
            this.LogAutoSaveCheckBox.AutoSize = true;
            this.LogAutoSaveCheckBox.Checked = true;
            this.LogAutoSaveCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LogAutoSaveCheckBox.Location = new System.Drawing.Point(221, 63);
            this.LogAutoSaveCheckBox.Name = "LogAutoSaveCheckBox";
            this.LogAutoSaveCheckBox.Size = new System.Drawing.Size(72, 16);
            this.LogAutoSaveCheckBox.TabIndex = 19;
            this.LogAutoSaveCheckBox.Text = "自动转储";
            this.LogAutoSaveCheckBox.UseVisualStyleBackColor = true;
            // 
            // btnMonitorDB
            // 
            this.btnMonitorDB.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnMonitorDB.AutoSize = true;
            this.btnMonitorDB.BackColor = System.Drawing.SystemColors.Control;
            this.btnMonitorDB.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMonitorDB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMonitorDB.Location = new System.Drawing.Point(545, 32);
            this.btnMonitorDB.Name = "btnMonitorDB";
            this.btnMonitorDB.Size = new System.Drawing.Size(69, 22);
            this.btnMonitorDB.TabIndex = 1;
            this.btnMonitorDB.Text = "   监控  ";
            this.btnMonitorDB.UseVisualStyleBackColor = false;
            this.btnMonitorDB.CheckedChanged += new System.EventHandler(this.btnMonitorDB_CheckedChanged);
            this.btnMonitorDB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnMonitorDB_KeyDown);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "DB SQL Monitor";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "DB SQL Monitor";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem.Text = "退出";
            this.toolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idx,
            this.logTime,
            this.spid,
            this.kpid,
            this.blocked,
            this.status,
            this.lastWaittype,
            this.waitResource,
            this.waitSeconds,
            this.CPU,
            this.logicalReads,
            this.diskReads,
            this.elapsed,
            this.loginName,
            this.hostName,
            this.programName,
            this.hostProcess,
            this.remarks,
            this.sqlText});
            this.listView1.ContextMenuStrip = this.listviewContextMenuStrip;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(12, 87);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(602, 334);
            this.listView1.TabIndex = 100;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // idx
            // 
            this.idx.Text = "idx";
            this.idx.Width = 40;
            // 
            // logTime
            // 
            this.logTime.Text = "logTime";
            this.logTime.Width = 130;
            // 
            // spid
            // 
            this.spid.Text = "spid";
            this.spid.Width = 55;
            // 
            // kpid
            // 
            this.kpid.Text = "kpid";
            this.kpid.Width = 55;
            // 
            // blocked
            // 
            this.blocked.Text = "blocked";
            this.blocked.Width = 55;
            // 
            // status
            // 
            this.status.Text = "status";
            this.status.Width = 85;
            // 
            // lastWaittype
            // 
            this.lastWaittype.Text = "lastWaittype";
            this.lastWaittype.Width = 100;
            // 
            // waitResource
            // 
            this.waitResource.Text = "waitResource";
            this.waitResource.Width = 90;
            // 
            // waitSeconds
            // 
            this.waitSeconds.Text = "waitSeconds";
            this.waitSeconds.Width = 80;
            // 
            // loginName
            // 
            this.loginName.Text = "loginName";
            this.loginName.Width = 90;
            // 
            // hostName
            // 
            this.hostName.Text = "hostName";
            this.hostName.Width = 90;
            // 
            // programName
            // 
            this.programName.Text = "programName";
            this.programName.Width = 90;
            // 
            // hostProcess
            // 
            this.hostProcess.Text = "hostProcess";
            // 
            // remarks
            // 
            this.remarks.Text = "remarks";
            // 
            // sqlText
            // 
            this.sqlText.Text = "sqlText";
            this.sqlText.Width = 300;
            // 
            // listviewContextMenuStrip
            // 
            this.listviewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.listviewContextMenuStrip.Name = "listviewContextMenuStrip";
            this.listviewContextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.clearToolStripMenuItem.Text = "清空";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "停止时间:";
            // 
            // stopDateTime
            // 
            this.stopDateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.stopDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.stopDateTime.Location = new System.Drawing.Point(370, 59);
            this.stopDateTime.Name = "stopDateTime";
            this.stopDateTime.Size = new System.Drawing.Size(153, 21);
            this.stopDateTime.TabIndex = 101;
            this.stopDateTime.Value = new System.DateTime(2017, 3, 31, 0, 0, 0, 0);
            // 
            // CPU
            // 
            this.CPU.Text = "CPU";
            // 
            // logicalReads
            // 
            this.logicalReads.Text = "logicalReads";
            // 
            // diskReads
            // 
            this.diskReads.Text = "diskReads";
            // 
            // elapsed
            // 
            this.elapsed.Text = "elapsed";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 426);
            this.Controls.Add(this.stopDateTime);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLogPath);
            this.Controls.Add(this.LogAutoSaveCheckBox);
            this.Controls.Add(this.btnMonitorDB);
            this.Controls.Add(this.KillBlockCheckBox);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnSaveLog);
            this.Controls.Add(this.btnModifyDB);
            this.Controls.Add(this.MonitorModeComboBox);
            this.Controls.Add(this.BlockTimeNumericUpDown);
            this.Controls.Add(this.MonitorIntervalMumericUpDown);
            this.Controls.Add(this.lblMonitorInteval);
            this.Controls.Add(this.lblBlockTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblMonitorMode);
            this.Controls.Add(this.LogPathTextBox);
            this.Controls.Add(this.DbUserTextBox);
            this.Controls.Add(this.lblDBUser);
            this.Controls.Add(this.DbNameTextBox);
            this.Controls.Add(this.lblDBName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DB SQL Monitor v1.7.7";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.MonitorIntervalMumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlockTimeNumericUpDown)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.listviewContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDBName;
        private System.Windows.Forms.TextBox DbNameTextBox;
        private System.Windows.Forms.Label lblDBUser;
        private System.Windows.Forms.TextBox DbUserTextBox;
        private System.Windows.Forms.Label lblMonitorMode;
        private System.Windows.Forms.Label lblMonitorInteval;
        private System.Windows.Forms.Label lblBlockTime;
        private System.Windows.Forms.NumericUpDown MonitorIntervalMumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown BlockTimeNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox MonitorModeComboBox;
        private System.Windows.Forms.Button btnModifyDB;
        private System.Windows.Forms.CheckBox KillBlockCheckBox;
        private System.Windows.Forms.Label lblLogPath;
        private System.Windows.Forms.TextBox LogPathTextBox;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.CheckBox LogAutoSaveCheckBox;
        private System.Windows.Forms.CheckBox btnMonitorDB;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        private System.Windows.Forms.ColumnHeader idx;
        private System.Windows.Forms.ColumnHeader logTime;
        private System.Windows.Forms.ColumnHeader spid;
        private System.Windows.Forms.ColumnHeader kpid;
        private System.Windows.Forms.ColumnHeader blocked;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader lastWaittype;
        private System.Windows.Forms.ColumnHeader waitSeconds;
        private System.Windows.Forms.ColumnHeader loginName;
        private System.Windows.Forms.ColumnHeader hostName;
        private System.Windows.Forms.ColumnHeader programName;
        private System.Windows.Forms.ColumnHeader sqlText;
        private System.Windows.Forms.ColumnHeader waitResource;
        private System.Windows.Forms.ColumnHeader hostProcess;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader remarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker stopDateTime;
        private System.Windows.Forms.ContextMenuStrip listviewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader CPU;
        private System.Windows.Forms.ColumnHeader logicalReads;
        private System.Windows.Forms.ColumnHeader diskReads;
        private System.Windows.Forms.ColumnHeader elapsed;
    }
}