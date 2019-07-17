using System;
using System.Windows.Forms;
using DBSQLMonitor.ExtendSPI;

namespace DBSQLMonitor
{
    public partial class FrmDB : Form
    {
        public FrmDB()
        {
            InitializeComponent();
        }

        public string DbType { get { return this.DbTypeComboBox.SelectedItem.ToString(); } }
        public string DbName { get { return this.DbNameTextBox.Text.Trim(); } }
        public string DbUser { get { return this.DbUserTextBox.Text.Trim(); } }
        public string Password { get { return this.PasswordTextBox.Text.Trim(); } }


        private void btnOK_Click(object sender, EventArgs e)
        {
            bool canConn = false;
            try
            {
                ConnStringBuilder strBuilder = new ConnStringBuilder() { DataSource = this.DbName, UserName = this.DbUser, Password = this.Password };
                IDBMonitor dbmonitor = DBMonitorFactory.CreateDBMonitor(this.DbType);
                canConn = dbmonitor.CheckConnAndAuth(strBuilder);

            } catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
                MessageBox.Show("连接失败或权限不足：" + ex.Message);
               
                canConn = false;
            }
            if (canConn == true)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inputControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Return == e.KeyCode)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void FrmDB_Load(object sender, EventArgs e)
        {
            this.DbTypeComboBox.Items.AddRange(new object[] { "Sqlserver", "Oracle" });
            this.DbTypeComboBox.SelectedIndex = 0;

            //this.DbTypeComboBox.Items.Clear();
            //this.DbTypeComboBox.Items.AddRange(ConfigurationManager.AppSettings.AllKeys);
            //this.DbTypeComboBox.SelectedIndex = 0;
        }
    }
}
