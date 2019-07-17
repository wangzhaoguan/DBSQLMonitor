using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DBSQLMonitor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
                    {
                        String resourceName = "DBSQLMonitor." + new AssemblyName(args.Name).Name + ".dll";
                        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                        {
                            Byte[] assemblyData = new Byte[stream.Length];
                            stream.Read(assemblyData, 0, assemblyData.Length);

                            Assembly asm = Assembly.Load(assemblyData);
                            return asm;
                        }
                    };

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new FrmDB());
                    Application.Run(new FrmMain());
                }
                else
                {
                    new Thread(KillMessageBox).Start();

                    MessageBox.Show("SQL Monitor已经在运行, 3秒后此消息会自动消失。", "Warning Duplicate DBSQL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    System.Environment.Exit(1);
                }
            }
        }

        #region 定時自動关闭MessageBox
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        private static void KillMessageBox()
        {
            Thread.Sleep(3000);
            // 按照MessageBox的标题，找出Messagebox这个视窗   
            IntPtr ptr = FindWindow(null, "Warning Duplicate DBSQL");
            if (ptr != IntPtr.Zero)     // 此表示只要 ptr 不为空值的内容时 
            {
                //找到则关闭 MessageBox 視窗   
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                System.Environment.Exit(0);
            }
        }

        #endregion
    }
}
