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
        public FormSignIn()
        {
            InitializeComponent();
        }

       
        //登录
        private void btnSignIn_Click(object sender, EventArgs e)
        {
           

            string account = this.textBoxAcount.Text;
            string pwd = this.textBoxPwd.Text;

            if (account == null)
            {
                MessageBox.Show("账号不能为空");
                return;
            }

            if (pwd == null)
            {
                MessageBox.Show("密码不能为空");
                return;
            }

            bool flag = DBEmp.isAccount(account);
            
            if (flag)
            {
                flag = DBEmp.isLogin(account, pwd);
                if (flag)
                {
                    FormMain mainForm = new FormMain();
                    mainForm.Show();
                    Hide();
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
            Application.Exit();
        }

        //退出系统
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //弹出数据库配置
        private void btnConfigDB_Click(object sender, EventArgs e)
        {
            FormConfigDB configDBForm = new FormConfigDB();
            configDBForm.Show();
        }

      
        
    }
}
