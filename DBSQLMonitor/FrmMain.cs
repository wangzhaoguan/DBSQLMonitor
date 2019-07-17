using Aspose.Cells;
using DBSQLMonitor.ExtendSPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DBSQLMonitor
{
    public partial class FrmMain : Form
    {
        private string _DbType;
        private string _DbName;
        private string _DbUser;
        private string _Password;
        private Timer _timer;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!LoadDB())
            {
                Application.Exit();
            }

            this.DbNameTextBox.Enabled = false;
            this.DbUserTextBox.Enabled = false;

            this.MonitorModeComboBox.SelectedIndex = 0;
            this.LogPathTextBox.Text = Environment.CurrentDirectory;
            this.stopDateTime.Value = DateTime.Now.AddDays(14);

            this._timer = new Timer();
            this._timer.Enabled = false;
            this._timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            List<SessionInfo> sessionList = new List<SessionInfo>(0);
            ConnStringBuilder strBuilder = new ConnStringBuilder() { DataSource = this._DbName, UserName = this._DbUser, Password = this._Password };
            IDBMonitor dbmonitor = DBMonitorFactory.CreateDBMonitor(this._DbType);
            sessionList = dbmonitor.GetSessionList(strBuilder);

            switch (this.MonitorModeComboBox.SelectedIndex)
            {
                case 0:
                    ShowBlockingLog(sessionList);
                    break;
                case 1:
                    ShowActiveLog(sessionList);
                    break;
                default:
                    ShowAllLog(sessionList);
                    break;
            }

            if (this.listView1.Items.Count > 10000 && this.LogAutoSaveCheckBox.Checked)
                DumpLog();

            if (DateTime.Now > this.stopDateTime.Value)
            {
                this.btnMonitorDB.Checked = false;
            }
        }

        private void DumpLog()
        {
            string path = string.Format("{0}\\{1}", string.IsNullOrEmpty(this.LogPathTextBox.Text.Trim()) ? Environment.CurrentDirectory : this.LogPathTextBox.Text.Trim(), DateTime.Now.ToString("yyyyMMdd"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = string.Format("{0}\\SqlTrace{1}.xls", path, DateTime.Now.ToString("yyyyMMddHHmmss"));
            for (int num = 2; File.Exists(fileName); num++)
                fileName += num;

            Workbook wb = new Workbook();
            Worksheet ws = wb.Worksheets[0];
            ws.FreezePanes("H2", 1, 7);     // 冻结窗口行列

            Style styleHeader = wb.Styles[wb.Styles.Add()];
            styleHeader.Font.IsBold = true;


            Style styleDateTime = wb.Styles[wb.Styles.Add()];
            styleDateTime.Number = 14;
            styleDateTime.Custom = "yyyy/mm/dd hh:mm:ss";
            styleDateTime.HorizontalAlignment = TextAlignmentType.Right;
            styleDateTime.VerticalAlignment = TextAlignmentType.Right;

            Style styleNumberic = wb.Styles[wb.Styles.Add()];
            styleNumberic.Number = 3;
            styleNumberic.Custom = "####0";
            styleNumberic.HorizontalAlignment = TextAlignmentType.Right;
            styleNumberic.VerticalAlignment = TextAlignmentType.Right;

            Style styleText = wb.Styles[wb.Styles.Add()];
            styleText.Number = 49;

            Cells cell = ws.Cells;
            cell.Columns[0].Width = 5;          //idx
            cell.Columns[1].Width = 18;         //logTime
            cell.Columns[2].Width = 6;          //spid
            cell.Columns[3].Width = 6;          //kpid
            cell.Columns[4].Width = 6;          //blocked
            cell.Columns[5].Width = 10;         //status
            cell.Columns[6].Width = 18;         //lastWaittype
            cell.Columns[7].Width = 10;         //waitResource
            cell.Columns[8].Width = 7;          //waitSeconds

            cell.Columns[9].Width = 7;         //cpu time
            cell.Columns[10].Width = 7;        //logical reads
            cell.Columns[11].Width = 7;        //disk reads
            cell.Columns[12].Width = 7;        //elapsed time

            cell.Columns[13].Width = 12;        //loginName
            cell.Columns[14].Width = 15;        //hostName
            cell.Columns[15].Width = 15;        //programName
            cell.Columns[16].Width = 12;        //hostProcess
            cell.Columns[17].Width = 15;        //remarks sql_id/sql_plan
            cell.Columns[18].Width = 30;        //sqlText


            //设置Execl列名
            for (int i = 0; i < this.listView1.Columns.Count; i++)
            {
                cell[0, i].PutValue(this.listView1.Columns[i].Text);
                cell[0, i].SetStyle(styleHeader);
            }

            //设置单元格内容
            for (int rowIndex = 0; rowIndex < this.listView1.Items.Count; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.listView1.Columns.Count; colIndex++)
                {
                    string cellContent = this.listView1.Items[rowIndex].SubItems[colIndex].Text;
                    if (!string.IsNullOrEmpty(cellContent) && cellContent.Length > 32000)
                    {
                        cellContent = cellContent.Substring(0, 32000);
                    }

                    if (colIndex == 1)
                    {
                        cell[rowIndex + 1, colIndex].SetStyle(styleDateTime);
                        cell[rowIndex + 1, colIndex].PutValue(cellContent);
                    }
                    else if (colIndex == 0 || colIndex == 2 || colIndex == 3 || colIndex == 4 || colIndex == 8 || colIndex == 9 || colIndex == 10 || colIndex == 11 || colIndex == 12)
                    {
                        cell[rowIndex + 1, colIndex].SetStyle(styleNumberic);
                        cell[rowIndex + 1, colIndex].PutValue(Convert.ToDecimal(cellContent));
                    }
                    else if (colIndex == 13)
                    {
                        cell[rowIndex + 1, colIndex].SetStyle(styleText);
                        cell[rowIndex + 1, colIndex].PutValue(cellContent);
                    }
                    else
                    {
                        cell[rowIndex + 1, colIndex].PutValue(cellContent);
                    }
                }
            }


            //保存excel表格
            wb.Save(fileName);

            this.listView1.Items.Clear();
        }


        private void btnModifyDB_Click(object sender, EventArgs e)
        {
            if (btnMonitorDB.Checked)
                MessageBox.Show("修改连接数据库，请先停止监视！");
            else
                LoadDB();
        }

        private bool LoadDB()
        {
            this.ImeMode = ImeMode.Off;
            using (FrmDB frmDb = new FrmDB())
            {
                if (frmDb.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return false;

                this.DbNameTextBox.Text = frmDb.DbName;
                this.DbUserTextBox.Text = frmDb.DbUser;

                this._DbType = frmDb.DbType;
                this._DbName = frmDb.DbName;
                this._DbUser = frmDb.DbUser;
                this._Password = frmDb.Password;
            }

            return true;
        }

        void ShowBlockingLog(List<SessionInfo> sessionList)
        {
            int blockedTime = Convert.ToInt32(this.BlockTimeNumericUpDown.Value);
            List<SessionInfo> blockedList = sessionList.Where(s => s.blocked != 0 && s.waitTime >= blockedTime).ToList();
            if (blockedList == null || blockedList.Count <= 0)
                return;
            

            // 找出所有阻塞链条上的session信息和阻塞源头
            List<SessionInfo> tmpList = new List<SessionInfo>();
            List<SessionInfo> rootList = new List<SessionInfo>();
            foreach (var item in blockedList)
            {
                int blockId = item.blocked;
                SessionInfo info = null;
                do
                {
                    // 数据库启用并行时，同一个客户端请求可能对应多个服务器端会话
                    //info = sessionList.Single(s => s.spid == blockId);
                    //if (!tmpList.Any(s => s.spid == info.spid))
                    //    tmpList.Add(info);

                    List<SessionInfo> parentList = sessionList.Where(s=> s.spid == blockId).ToList();
                    if (parentList == null || parentList.Count <= 0)
                    {
                        break;
                    }
                    else
                    {
                        foreach (var p in parentList)
                        {
                            if (!tmpList.Any(s => s.spid == p.spid && s.kpid == p.kpid))
                                tmpList.Add(p);
                        }
                    }
                    
                    info = sessionList.First(s=> s.spid == blockId);
                    blockId = info.blocked;
                    if (tmpList.Any(s => s.spid == blockId))
                        break;
                } while (blockId != 0);

                if (info != null && !rootList.Any(s => s.spid == info.spid))
                    rootList.Add(info);
            }

            foreach (var item in tmpList)
            {
                if (!blockedList.Any(s => s.spid == item.spid && s.kpid == item.kpid))
                    blockedList.Add(item);
            }
            
            int rowIndex = 0;
            string dt = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ListViewItem[] list = new ListViewItem[blockedList.Count];
            foreach (var sessionInfo in blockedList)
            {
                ListViewItem lvItem = new ListViewItem((++rowIndex).ToString()); //(++rowIndex).ToString());
                lvItem.SubItems.Add(dt);
                lvItem.SubItems.Add(sessionInfo.spid.ToString());
                lvItem.SubItems.Add(sessionInfo.kpid.ToString());
                lvItem.SubItems.Add(sessionInfo.blocked.ToString());
                lvItem.SubItems.Add(sessionInfo.status);
                lvItem.SubItems.Add(sessionInfo.lastWaitType);
                lvItem.SubItems.Add(sessionInfo.waitResource);
                lvItem.SubItems.Add(sessionInfo.waitTime.ToString());
                lvItem.SubItems.Add(sessionInfo.cpu_time.ToString());
                lvItem.SubItems.Add(sessionInfo.logical_reads.ToString());
                lvItem.SubItems.Add(sessionInfo.disk_reads.ToString());
                lvItem.SubItems.Add(sessionInfo.elapsed_time.ToString());
                lvItem.SubItems.Add(sessionInfo.loginName);
                lvItem.SubItems.Add(sessionInfo.hostName);
                lvItem.SubItems.Add(sessionInfo.programName);
                lvItem.SubItems.Add(sessionInfo.hostProcess);
                lvItem.SubItems.Add(sessionInfo.remarks);
                lvItem.SubItems.Add(sessionInfo.sqlText);

                list[rowIndex - 1] = lvItem;
            }
            this.listView1.BeginUpdate();
            this.listView1.Items.AddRange(list);
            this.listView1.EndUpdate();

            if (this.KillBlockCheckBox.Checked && rootList.Count > 0)
            {
                ConnStringBuilder strBuilder = new ConnStringBuilder() { DataSource = this._DbName, UserName = this._DbUser, Password = this._Password };
                IDBMonitor dbmonitor = DBMonitorFactory.CreateDBMonitor(this._DbType);
                dbmonitor.KillRootBlockingSession(strBuilder, rootList);
            }
        }

        private void ShowActiveLog(List<SessionInfo> sessionList)
        {
            int runningTime = Convert.ToInt32(this.BlockTimeNumericUpDown.Value);
            List<SessionInfo> activeList = sessionList.Where(s => (s.status == "ACTIVE" || s.status == "running" || s.status == "runnable" || s.status == "suspended") && s.elapsed_time >= runningTime).ToList();
            if (activeList == null || activeList.Count <= 0)
                return;



            int rowIndex = 0;
            string dt = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ListViewItem[] list = new ListViewItem[activeList.Count];
            foreach (var sessionInfo in activeList)
            {
                ListViewItem lvItem = new ListViewItem((++rowIndex).ToString()); //(++rowIndex).ToString());
                lvItem.SubItems.Add(dt);
                lvItem.SubItems.Add(sessionInfo.spid.ToString());
                lvItem.SubItems.Add(sessionInfo.kpid.ToString());
                lvItem.SubItems.Add(sessionInfo.blocked.ToString());
                lvItem.SubItems.Add(sessionInfo.status);
                lvItem.SubItems.Add(sessionInfo.lastWaitType);
                lvItem.SubItems.Add(sessionInfo.waitResource);
                lvItem.SubItems.Add(sessionInfo.waitTime.ToString());
                lvItem.SubItems.Add(sessionInfo.cpu_time.ToString());
                lvItem.SubItems.Add(sessionInfo.logical_reads.ToString());
                lvItem.SubItems.Add(sessionInfo.disk_reads.ToString());
                lvItem.SubItems.Add(sessionInfo.elapsed_time.ToString());

                lvItem.SubItems.Add(sessionInfo.loginName);
                lvItem.SubItems.Add(sessionInfo.hostName);
                lvItem.SubItems.Add(sessionInfo.programName);
                lvItem.SubItems.Add(sessionInfo.hostProcess);
                lvItem.SubItems.Add(sessionInfo.remarks);
                lvItem.SubItems.Add(sessionInfo.sqlText);

                list[rowIndex - 1] = lvItem;
            }
            this.listView1.BeginUpdate();
            this.listView1.Items.AddRange(list);
            this.listView1.EndUpdate();
        }

        private void ShowAllLog(List<SessionInfo> sessionList)
        {
            int rowIndex = 0;
            string dt = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ListViewItem[] list = new ListViewItem[sessionList.Count];
            foreach (var sessionInfo in sessionList)
            {
                ListViewItem lvItem = new ListViewItem((++rowIndex).ToString()); //(++rowIndex).ToString());
                lvItem.SubItems.Add(dt);
                lvItem.SubItems.Add(sessionInfo.spid.ToString());
                lvItem.SubItems.Add(sessionInfo.kpid.ToString());
                lvItem.SubItems.Add(sessionInfo.blocked.ToString());
                lvItem.SubItems.Add(sessionInfo.status);
                lvItem.SubItems.Add(sessionInfo.lastWaitType);
                lvItem.SubItems.Add(sessionInfo.waitResource);
                lvItem.SubItems.Add(sessionInfo.waitTime.ToString());
                lvItem.SubItems.Add(sessionInfo.cpu_time.ToString());
                lvItem.SubItems.Add(sessionInfo.logical_reads.ToString());
                lvItem.SubItems.Add(sessionInfo.disk_reads.ToString());
                lvItem.SubItems.Add(sessionInfo.elapsed_time.ToString());

                lvItem.SubItems.Add(sessionInfo.loginName);
                lvItem.SubItems.Add(sessionInfo.hostName);
                lvItem.SubItems.Add(sessionInfo.programName);
                lvItem.SubItems.Add(sessionInfo.hostProcess);
                lvItem.SubItems.Add(sessionInfo.remarks);
                lvItem.SubItems.Add(sessionInfo.sqlText);

                list[rowIndex - 1] = lvItem;
            }
            this.listView1.BeginUpdate();
            this.listView1.Items.AddRange(list);
            this.listView1.EndUpdate();
        }

        private void btnMonitorDB_CheckedChanged(object sender, EventArgs e)
        {
            if (btnMonitorDB.Checked)
            {
                this.btnModifyDB.ForeColor = Color.LimeGreen;
                this.MonitorIntervalMumericUpDown.Enabled = false;
                this.MonitorModeComboBox.Enabled = false;
                this.BlockTimeNumericUpDown.Enabled = false;
                this.btnModifyDB.Enabled = false;
                this.btnMonitorDB.Text = "   停止  ";

                this._timer.Interval = Convert.ToInt32(this.MonitorIntervalMumericUpDown.Value) * 1000;
                this._timer.Enabled = true;
            }
            else
            {
                this.btnModifyDB.ForeColor = Color.Black;
                this.MonitorIntervalMumericUpDown.Enabled = true;
                this.MonitorModeComboBox.Enabled = true;
                this.BlockTimeNumericUpDown.Enabled = this.MonitorModeComboBox.SelectedIndex != 2;
                this.KillBlockCheckBox.Enabled = this.MonitorModeComboBox.SelectedIndex == 0;
                this.btnModifyDB.Enabled = true;
                this.btnMonitorDB.Text = "   监控  ";

                this._timer.Enabled = false;
            }
        }

        private void MonitorModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MonitorModeComboBox.SelectedIndex == 0)
            {
                this.BlockTimeNumericUpDown.Enabled = true;

                this.KillBlockCheckBox.Enabled = true;
            }
            else if (this.MonitorModeComboBox.SelectedIndex == 1)
            {
                this.BlockTimeNumericUpDown.Enabled = true;

                this.KillBlockCheckBox.Checked = false;
                this.KillBlockCheckBox.Enabled = false;
            }
            else
            {
                this.BlockTimeNumericUpDown.Enabled = false;
                this.BlockTimeNumericUpDown.Value = 0;
                this.KillBlockCheckBox.Checked = false;
                this.KillBlockCheckBox.Enabled = false;
            }
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            DumpLog();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
        }

        private void btnMonitorDB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send(" ");
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
                base.Hide();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(this._DbName))
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Application.Exit();
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon.Visible = true;
                this.Hide();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://zhaoguan_wang.cnblogs.com");
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(256);
                    for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                        strBuilder.Append(listView1.Columns[i].Text + "\t");
                    }
                    strBuilder.AppendLine();

                    for (int selectedIndex = 0; selectedIndex < listView1.SelectedItems.Count; selectedIndex++)
                    {
                        for (int colIndex = 0; colIndex < listView1.Columns.Count; colIndex++)
                        {
                            strBuilder.Append(listView1.SelectedItems[selectedIndex].SubItems[colIndex].Text + "\t");
                        }
                        strBuilder.AppendLine();
                    }
                    
                    Clipboard.SetDataObject(strBuilder.ToString());
                }
            }
        }
    }
}

