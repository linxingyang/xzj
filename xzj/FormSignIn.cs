using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xzj
{
    public partial class FormSignIn : Form
    {
        private static string ACCOUNT = "";
        public FormSignIn()
        {
            InitializeComponent();

        }

        private void FormSignIn_Load(object sender, EventArgs e)
        {
            labelTitle.BackColor = ColorTranslator.FromHtml("#014684");
            btnConfigDB.BackColor = ColorTranslator.FromHtml("#014684");
            btnTime.BackColor = ColorTranslator.FromHtml("#014684");
        }
       
        //登录
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string account = this.textBoxAcount.Text;
            string pwd = this.textBoxPwd.Text;

            string sqlAddress = DBSQLite.selectValue(UtilConfig.SQL_ADDRESS_KEY) + "";
            if (sqlAddress.Length == 0)
            {
                MessageBox.Show("你还没有配置数据库，需要先配置数据库，才能登录");
                return;
            }

            if (string.IsNullOrEmpty(account))
            {
                MessageBox.Show("账号不能为空");
                return;
            }

            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("密码不能为空");
                return;
            }


            bool flag = DBEmp.getInstance().isAccount(account);

            if (flag)
            {
                flag = DBEmp.getInstance().isLogin(account, pwd);
                if (flag)
                {
                    //UtilConfig.ACCOUNT = account;
                    DBSQLite.updateValue(UtilConfig.ACCOUNT_KEY, account);
                    DBSQLite.updateValue(UtilConfig.PWD_KEY, pwd);
                    goMainForm();
                }
                else
                {
                    MessageBox.Show("用户名与密码不匹配");
                }
                ;
            }
            else
            {
                MessageBox.Show("账号不存在");
            }

            
        }

        //退出系统
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            DBSQLite.clearByKey(UtilConfig.ACCOUNT_KEY);
            Application.Exit();
        }

        //退出系统
        private void btnClose_Click(object sender, EventArgs e)
        {
            DBSQLite.clearByKey(UtilConfig.ACCOUNT_KEY);
            Application.Exit();
        }

        //弹出数据库配置
        private void btnConfigDB_Click(object sender, EventArgs e)
        {
            FormConfigDB form = new FormConfigDB();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                UtilConfig.SQL_ADDRESS = DBSQLite.selectValue(UtilConfig.SQL_ADDRESS_KEY);
            }
        }

        //获取实时的时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.btnTime.Text = UtilTools.getDayAndTime();
        }

        private void goMainForm()
        {
            FormMain mainForm = new FormMain();
            mainForm.Show();
            Hide();
        }
    }
}
