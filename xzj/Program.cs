using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace xzj
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process[] tProcess = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (tProcess.Length > 1)
            {
                Application.Exit();
            }
            else
            {
                UtilConfig.SQL_ADDRESS = DBSQLite.selectValue(UtilConfig.SQL_ADDRESS_KEY);
                string ACCOUNT = DBSQLite.selectValue(UtilConfig.ACCOUNT_KEY);
                if (!string.IsNullOrEmpty(ACCOUNT))
                {
                    Application.Run(new FormMain());
                }
                else
                {
                    Application.Run(new FormSignIn());
                }
            }
        }

      
    }
}
