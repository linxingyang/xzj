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
        private DataTable dataTable;

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

        //查询医保类型字典
        private void btnDictionaryYBLX_Click(object sender, EventArgs e)
        {
            labelDictionaryShow.Text = "医保类型字典";
        }

        //查询手术字典
        private void btnDictionarySSZD_Click(object sender, EventArgs e)
        {
            this.listViewDictionarySSZD.Visible = ! this.listViewDictionarySSZD.Visible;

            if (this.listViewDictionarySSZD.Visible)
            {
                //设置listview表头颜色
                this.listViewDictionarySSZD.Items.Clear();
                this.listViewDictionarySSZD.FullRowSelect = true;
                this.listViewDictionarySSZD.BackColor = ColorTranslator.FromHtml("#41aaeb");

                DataTable dt = DBDictionary.getInstance().getDictionarys("top", "手术字典");
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.Text = row["d_name"].ToString();
                    //item.SubItems.Add(row["d_name"].ToString());
                    this.listViewDictionarySSZD.Items.Add(item);
                }
            }
        }

        //查询情况字典
        private void btnDictionaryQKZD_Click(object sender, EventArgs e)
        {
            
        }

        //科室增加
        private void btnSaveRoom_Click_1(object sender, EventArgs e)
        {
            string hispitorName = this.tbRoomHispitorName.Text;//医院名称
            string hispitorAddress = this.tbRoomHispitorAddress.Text;//医院地址
            string province = this.cbRoomProvince.Text;//省
            string rank = this.cbRoomRank.Text;//等级
            string postcode = this.tbRoomPostcode.Text;//邮政编码
            string roomName = this.tbRoomName.Text;//科室名称
            string roomTel = this.tbRoomTel.Text;//科室电话
            string roomFax = this.tbRoomFax.Text;//科室传真
            string roomFZR = this.tbRoomFZR.Text;//科室负责人
            string roomFZRZC = this.tbRoomFZRZC.Text;//科室负责人职称
            string roomFZRDH = this.tbRoomFZRDH.Text;//科室负责人电话
            string roomFZRSJ = this.tbRoomFZRSJ.Text;//科室负责人手机
            string roomFZRYX = this.tbRoomFZRYX.Text;//科室负责人邮箱
            string roomTXZXMJ = this.tbRoomTXZXMJ.Text;//透析中心面积
            string roomTXDYMJ = this.tbRoomTXDYMJ.Text;//透析单元面积
            string roomKSRQ = this.dtpRoomKSRQ.Text;//开始日期
           // hispitorName,hispitorAddress,province,rank,postcode,roomName,roomTel,roomFax,roomFZR,roomFZRZC,roomFZRDH,roomFZRSJ,roomFZRYX,roomTXZXMJ,roomTXDYMJ,roomKSRQ

            if (string.IsNullOrEmpty(hispitorName))
            {
                MessageBox.Show("医院名称不能为空");
                return;
            }

            if (string.IsNullOrEmpty(hispitorAddress))
            {
                MessageBox.Show("医院地址不能为空");
                return;
            }

            if (string.IsNullOrEmpty(province))
            {
                MessageBox.Show("省(直辖市)不能为空");
                return;
            }

            if (string.IsNullOrEmpty(rank))
            {
                MessageBox.Show("等级不能为空");
                return;
            }

            if (string.IsNullOrEmpty(postcode))
            {
                MessageBox.Show("邮政编码不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomName))
            {
                MessageBox.Show("科室名称不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomTel))
            {
                MessageBox.Show("科室电话不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomFax))
            {
                MessageBox.Show("科室传真不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomFZR))
            {
                MessageBox.Show("科室负责人不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomFZRZC))
            {
                MessageBox.Show("科室负责人职称不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomFZRDH))
            {
                MessageBox.Show("科室负责人电话不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomFZRSJ))
            {
                MessageBox.Show("科室负责人手机不能为空");
                return;
            }

            if (roomFZRSJ.Length != 11)
            {
                MessageBox.Show("科室负责人手机 格式有误");
                return;
            }

            if (string.IsNullOrEmpty(roomFZRYX))
            {
                MessageBox.Show("科室负责人邮箱不能为空");
                return;
            }

            if (!UtilTools.isEmail(roomFZRYX))
            {
                MessageBox.Show("科室负责人邮箱 格式有误");
                return;
            }

            if (string.IsNullOrEmpty(roomTXZXMJ))
            {
                MessageBox.Show("透析中心面积 不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomTXDYMJ))
            {
                MessageBox.Show("透析单元面积 不能为空");
                return;
            }

            if (string.IsNullOrEmpty(roomKSRQ))
            {
                MessageBox.Show("开始日期 不能为空");
                return;
            }

            int flag = 0;

            dataTable = DBRoom.getInstance().getRooms(hispitorName, roomName);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                flag = DBRoom.getInstance().editRoom(hispitorName, hispitorAddress, province, rank, postcode, roomName, roomTel, roomFax, roomFZR, roomFZRZC, roomFZRDH, roomFZRSJ, roomFZRYX, roomTXZXMJ, roomTXDYMJ, roomKSRQ);
            }
            else
            {
                flag = DBRoom.getInstance().addRoom(hispitorName, hispitorAddress, province, rank, postcode, roomName, roomTel, roomFax, roomFZR, roomFZRZC, roomFZRDH, roomFZRSJ, roomFZRYX, roomTXZXMJ, roomTXDYMJ, roomKSRQ);
            }
            if (flag > 0)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }

        //只能输入数字
        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

    }
}
