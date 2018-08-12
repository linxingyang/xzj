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
    public partial class FormMain : Form
    {
   
        public FormMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = true;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelKSGL.Visible = false;
      
        }

        //关闭窗口
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //最大化 正常化
        private void picMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;

            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            
            this.flowLayoutForm.Location = new Point(this.Width-102, 0);
        }

        //最小
        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //显示手术录入
        private void btnSSLR_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = true;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelKSGL.Visible = false;
        }

        //字典管理
        private void btnZDGL_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = false;
            this.panelZDGL.Visible = true;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelKSGL.Visible = false;
        }

        //数据查询
        private void btnSJCX_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = false;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = true;
            this.panelCJFX.Visible = false;
            this.panelKSGL.Visible = false;
        }

        //统计分析
        private void btnCJFX_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.panelCJFX.BackColor = ColorTranslator.FromHtml("#ff0033");
            this.panelSSLR.Visible = false;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = true;
            this.panelKSGL.Visible = false;
        }

        //科室管理
        private void btnKSGL_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelSSLR.Visible = false;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelKSGL.Visible = true;

            //查询科室信息

        }

        //切换科室信息管理按钮
        private void btnRoomInfoManager_Click(object sender, EventArgs e)
        {
            this.btnRoomInfoManager.ForeColor = ColorTranslator.FromHtml("#3399ff");
            this.btnRoomInfoManager.BackColor = Color.White;

            this.btnRoomEmpManager.ForeColor = Color.White;
            this.btnRoomEmpManager.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelRoomInfoManager.Visible = true;
            this.panelRoomEmpManager.Visible = false;

        }

        //切换科室职员管理按钮
        private void btnRoomEmpManager_Click(object sender, EventArgs e)
        {
            this.btnRoomEmpManager.ForeColor = ColorTranslator.FromHtml("#3399ff");
            this.btnRoomEmpManager.BackColor = Color.White;

            this.btnRoomInfoManager.ForeColor = Color.White;
            this.btnRoomInfoManager.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelRoomInfoManager.Visible = false;
            this.panelRoomEmpManager.Visible = true;

            this.setListViewEmp(null, null);//重新绑定

        }

        //填充人员信息表
        private void setListViewEmp(String account,string name)
        {
            //设置listview表头颜色
            this.listViewEmp.Items.Clear();
            this.listViewEmp.FullRowSelect = true;
            //this.listViewEmp.Items.H
            DataTable dt = DBEmp.getInstance().getEmps(account, name);
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Clear();

                //item.Text = row["id"].ToString();
                item.Text = row["e_account"].ToString();
                item.SubItems.Add(row["e_name"].ToString());
                item.SubItems.Add(row["e_sex"].ToString());
                item.SubItems.Add(row["e_birth"].ToString());
                item.SubItems.Add(row["e_tel"].ToString());

                if (i % 2 == 0)
                {
                    item.BackColor = ColorTranslator.FromHtml("#f1f3f2"); ;
                    //item.ForeColor = Color.Red;
                }

                this.listViewEmp.Items.Add(item);
                i++;
            }
        }

        //选中人员信息行
        private void listViewEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int i in this.listViewEmp.SelectedIndices)
            {
                this.listViewEmp.Items[i].Selected = true;
                //MessageBox.Show(this.listViewEmp.SelectedItems[0].Text);
            }
        }

        //通过帐户删除人员信息
        private void btnDeleteEmp_Click(object sender, EventArgs e)
        {
            if (this.listViewEmp.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要删除的科室人员");
                return;
            }
            string account = this.listViewEmp.SelectedItems[0].Text;
            string name = this.listViewEmp.SelectedItems[0].SubItems[1].Text;
            DialogResult dr = MessageBox.Show("删除【" + name + "(" + account + ")】用户?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DBEmp.getInstance().deleteEmpByAccount(account);
                this.setListViewEmp(null, null);//重新绑定
            }
            
        }

        //打开新增科室职员
        private void btnGoAddEmp_Click(object sender, EventArgs e)
        {
            FormAddEmp.ACCOUNT = null;
            FormAddEmp form = new FormAddEmp();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                this.setListViewEmp(null,null);//重新绑定
            }
        }

        //编辑人员信息
        private void btnGoEditEmp_Click(object sender, EventArgs e)
        {
            if (this.listViewEmp.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要编辑的科室人员");
                return;
            }
            //string account = this.listViewEmp.SelectedItems[0].Text;
            FormAddEmp.ACCOUNT = this.listViewEmp.SelectedItems[0].Text;
            FormAddEmp form = new FormAddEmp();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                this.setListViewEmp(null, null);//重新绑定
            }
        }

        //通过名称和用户名查询用户信息
        private void btnFindEmpByAccountAndName_Click(object sender, EventArgs e)
        {
            this.setListViewEmp(this.tbFindEmpAccount.Text,this.tbFindEmpName.Text);//重新绑定
        }

        //科室保存
        private void btnSaveRoom_Click(object sender, EventArgs e)
        {

        }


      
    }
}
