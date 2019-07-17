using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Oracle.ManagedDataAccess.Client;
using System.Data.OracleClient;
//using Oracle.DataAccess.Client;
using System.Data.SqlClient;

namespace TestSQL
{
    public partial class OracleSql : Form
    {
        public OracleSql()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            int rows = MSOracleClient();

            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss.fff");
            lblTime.Text = (DateTime.Now - dt1).TotalMilliseconds.ToString();
            lblRows.Text = rows.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            int rows = ODP();

            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss.fff");
            lblTime.Text = (DateTime.Now - dt1).TotalMilliseconds.ToString();
            lblRows.Text = rows.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            int rows = ManagedOracleClient();

            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss.fff");
            lblTime.Text = (DateTime.Now - dt1).TotalMilliseconds.ToString();
            lblRows.Text = rows.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            int rows = DevartOracleClient();

            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss.fff");
            lblTime.Text = (DateTime.Now - dt1).TotalMilliseconds.ToString();
            lblRows.Text = rows.ToString();
        }

        int MSOracleClient()
        {
            System.Data.OracleClient.OracleConnectionStringBuilder connBuilder = new System.Data.OracleClient.OracleConnectionStringBuilder();
            connBuilder.DataSource = txtDataSource.Text.Trim();
            connBuilder.UserID = txtUserId.Text.Trim();
            connBuilder.Password = txtPwd.Text.Trim();
            connBuilder.LoadBalanceTimeout = 60;
            connBuilder.MinPoolSize = 0;
            connBuilder.MaxPoolSize = 50;

            int rows = 0;
            using(System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(connBuilder.ConnectionString))
            {
                //System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection(connBuilder.ConnectionString);
                System.Data.OracleClient.OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = txtSql.Text.Trim();
                cmd.CommandTimeout = 300;
                //cmd.ResetCommandTimeout();
                
                conn.Open();
                using (System.Data.OracleClient.OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        object[] objs = new object[500];
                        dr.GetValues(objs);
                        rows++;
                    }
                }

                return rows;
            }
        }

        int ODP()
        {
            Oracle.ManagedDataAccess.Client.OracleConnectionStringBuilder connBuilder = new Oracle.ManagedDataAccess.Client.OracleConnectionStringBuilder();
            connBuilder.DataSource = txtDataSource.Text.Trim();
            connBuilder.UserID = txtUserId.Text.Trim();
            connBuilder.Password = txtPwd.Text.Trim();
            connBuilder.ConnectionTimeout = 300;
            connBuilder.ConnectionLifeTime = 60;
            connBuilder.MinPoolSize = 0;

            int rows = 0;
            using (IDbConnection conn = ODPClientFactory.CreateConnection())
            {
                conn.ConnectionString = connBuilder.ConnectionString;
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = txtSql.Text.Trim();
                cmd.CommandTimeout = 300;

                conn.Open();
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        object[] objs = new object[500];
                        dr.GetValues(objs);
                        rows++;
                    }
                }

                return rows;
            }
        }

        int ManagedOracleClient()
        {
            Oracle.ManagedDataAccess.Client.OracleConnectionStringBuilder connBuilder = new Oracle.ManagedDataAccess.Client.OracleConnectionStringBuilder();
            connBuilder.DataSource = txtDataSource.Text.Trim();
            connBuilder.UserID = txtUserId.Text.Trim();
            connBuilder.Password = txtPwd.Text.Trim();
            connBuilder.ConnectionTimeout = 300;
            connBuilder.ConnectionLifeTime = 10;
            connBuilder.MinPoolSize = 0;

            int rows = 0;
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connBuilder.ConnectionString))
            {
                Oracle.ManagedDataAccess.Client.OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = txtSql.Text.Trim();
                cmd.CommandTimeout = 300;

                conn.Open();
                using (Oracle.ManagedDataAccess.Client.OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        object[] objs = new object[500];
                        dr.GetValues(objs);
                        rows++;
                    }
                }

                return rows;
            }
        }

        int DevartOracleClient()
        {
            System.Data.OracleClient.OracleConnectionStringBuilder connBuilder = new System.Data.OracleClient.OracleConnectionStringBuilder();
            connBuilder.DataSource = txtDataSource.Text.Trim();
            connBuilder.UserID = txtUserId.Text.Trim();
            connBuilder.Password = txtPwd.Text.Trim();

            int rows = 0;
            using (Devart.Data.Oracle.OracleConnection conn = new Devart.Data.Oracle.OracleConnection(connBuilder.ConnectionString))
            {
                Devart.Data.Oracle.OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = txtSql.Text.Trim();
                cmd.CommandTimeout = 300;

                conn.Open();
                using (Devart.Data.Oracle.OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        object[] objs = new object[500];
                        dr.GetValues(objs);
                        rows++;
                    }
                }

                return rows;
            }
        }

        private void inputControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Return == e.KeyCode)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            int rows = SqlClient();

            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss.fff");
            lblTime.Text = (DateTime.Now - dt1).TotalMilliseconds.ToString();
            lblRows.Text = rows.ToString();
        }

        int SqlClient()
        {
            SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
            connBuilder.DataSource = txtDataSource.Text.Trim();
            connBuilder.UserID = txtUserId.Text.Trim();
            connBuilder.Password = txtPwd.Text.Trim();
            connBuilder.ConnectTimeout = 300;
            connBuilder.LoadBalanceTimeout = 10;
            connBuilder.MinPoolSize = 0;
            connBuilder.InitialCatalog = "master";

            int rows = 0;
            using (SqlConnection conn = new SqlConnection(connBuilder.ConnectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = txtSql.Text.Trim();
                cmd.CommandTimeout = 300;

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        object[] objs = new object[500];
                        dr.GetValues(objs);
                        rows++;
                    }
                }

                return rows;
            }
        }

    }
}
