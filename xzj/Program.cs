using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //创建本地存储
            DBSQLite.CreateDB();
            DBSQLite.CreateTable();
            UtilConfig.SQL_ADDRESS = DBSQLite.selectValue(UtilConfig.SQL_ADDRESS_KEY);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormSignIn());
        }
    }
}
