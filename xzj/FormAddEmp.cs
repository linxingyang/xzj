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
    public partial class FormAddEmp : Form
    {
        public FormAddEmp()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //添加用户
        private void btnSave_Click(object sender, EventArgs e)
        {
            string account = this.tbAccount.Text;
            string name = this.tbName.Text;
            //string sex = this.cbSex.Text;
            string birth = this.dtpBirth.Text;
            string tel = this.tbTel.Text;
            string email = this.tbEmail.Text;
            string address = this.tbAddress.Text;
            string pwd = this.tbPwd.Text;
            string surePwd = this.tbSurePwd.Text;

            if (account == null)
            {
                MessageBox.Show("用户名不能为空");
                return;
            }

        }
    }
}
