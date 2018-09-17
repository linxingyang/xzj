using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.IO;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace xzj
{
    public partial class FormMain : Form
    {
        private DataTable dataTable;
        private static int dictionary_parent_id = 1;
        static int listViewDictionaryFlag = 0;//1代表在列表上，0代表不在列表上
       
        public FormMain()
        {
            InitializeComponent();
            this.ImeMode = System.Windows.Forms.ImeMode.OnHalf;

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

            //显示当前用户
            this.labelAccountShow.BackColor = ColorTranslator.FromHtml("#0078d7");

            //获取用户名
            string account = DBSQLite.selectValue(UtilConfig.ACCOUNT_KEY);
            dataTable = DBManager.getInstance().find(string.Format("select e_name from t_emp where e_account='{0}'", account));
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    this.labelAccountShow.Text = "用户名【" + row["e_name"].ToString() + "】";
                    this.labelAccountShow.ForeColor = ColorTranslator.FromHtml("#fff");
                }
            }

            //初始化手术录入
            initSSLR();
        }

        //关闭窗口
        private void picClose_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
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

            this.flowLayoutForm.Location = new Point(this.Width - 102, 0);
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
            this.btnSSZZ.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSXGXY.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = true;
            this.panelSSZZ.Visible = false;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelSSXGXY.Visible = false;
            this.panelKSGL.Visible = false;


            initSSLR();
        }

        //字典combobox
        private void combobox(ComboBox cb,DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                ArrayList al = new ArrayList();
                foreach (DataRow row in dt.Rows)
                {
                    string text = row["d_name"].ToString();
                    int value = Convert.ToInt32(row["id"].ToString());
                    al.Add(new UtilTextAndValue(text, value));

                }
                cb.DataSource = al;
                cb.DisplayMember = "DisplayText";
                cb.ValueMember = "RealValue";
                cb.SelectedIndex = 0;
                dt = null;
            }
        }

        //初始化手术录入
        private void initSSLR()
        {
            //this.labelSSLR_SSJL_ID.Text = "1111";//手术记录与手术跟踪的id
            this.tbSSLR_NAME.Text = "";//患者姓名
            //this.cbSSLR_SEX.Text = "";//性别
            this.tbSSLR_AGE.Text = "";//年龄
            this.tbSSLR_TEL.Text = "";//手机号
            this.tbSSLR_ID.Text = "";//身份证号
            this.tbSSLR_DETAIL_ADDRESS.Text = "";//详细地址
            this.tbSSLR_CTXYY.Text = "";//常透析医院
            this.tbSSLR_CTXYYLXR.Text = "";//常透析医院联系人
            this.tbSSLR_CTXYYLXRDH.Text = "";//常透析医院联系人电话
            this.rtbSSLR_SSJL.Text = "";//手术记录
            this.tbSSLR_ZDYS.Text = "";//主刀医生
            this.tbSSLR_ZS.Text = "";//助手
            this.tbSSLR_QXFS.Text = "";//器械护士

            //性别
            this.cbSSLR_SEX.SelectedIndex = 0;

            //查询省
            DataTable dt = DBManager.getInstance().find("SELECT id,p_name as d_name FROM t_province");
            combobox(this.cbSSLR_PROVINCE, dt);//

            //查询市
            dt = DBManager.getInstance().find(string.Format("SELECT  id,c_name as d_name FROM xzj.t_city where c_province_id = {0}", this.cbSSLR_PROVINCE.SelectedValue));
            combobox(this.cbSSLR_CITY, dt);//

            //查询县
            dt = DBManager.getInstance().find(string.Format("SELECT  id,a_name as d_name FROM xzj.t_area where a_city_id = {0}", this.cbSSLR_CITY.SelectedValue));
            combobox(this.cbSSLR_COUNTY, dt);//

            //查询医保类型字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(1);
            combobox(this.cbSSLR_YBLX, dt);//

            //查询手术地点字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(5);
            combobox(this.cbSSLR_SSDD, dt);

            //查询手术类型字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(4);
            combobox(this.cbSSLR_SSLX, dt);

            //查询手术方式字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(6);
            combobox(this.cbSSLR_SSFS, dt);

            //查询穿刺方式字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(7);
            combobox(this.cbSSLR_CCFS, dt);

            //查询手术追踪期限字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(8);
            combobox(this.cbSSLR_SSZZQX, dt);

            //查询手术部位字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(9);
            combobox(this.cbSSLR_SSBW, dt);

            //手术日期
            this.dpSSLR_SSRQ.Value = Convert.ToDateTime(UtilTools.getDayAndTimeMM());

            /**
            //查询手术追踪-感染控制方式字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(11);
            combobox(this.cbSSLR_SSZZ_GRKZFS, dt);

            //查询手术追踪-内痿自我锻炼情况字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(9);
            combobox(this.cbSSLR_SSZZ_NWZWDLQK, dt);

            //查询手术追踪-穿刺部位皮肤情况字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(10);
            combobox(this.cbSSLR_SSZZ_CCBWPFQK, dt);

           
            //是否畅通
            this.cbSSLR_SSZZ_SFTC.SelectedIndex = 0;
            //有无血流不畅通
            this.cbSSLR_SSZZ_YWXLBCT.SelectedIndex = 0;
            //有胸闷
            this.cbSSLR_SSZZ_YWXM.SelectedIndex = 0;
            //有无过敏情况
            this.cbSSLR_SSZZ_YWCCBWPFGMQK.SelectedIndex = 0;
            //有无并发症
            this.cbSSLR_SSZZ_YWBFZ.SelectedIndex = 0;
            //有无胸壁静脉曲张
            this.cbSSLR_SSZZ_YWXBJMQZ.SelectedIndex = 0;
            //是否复诊
            //this.cbSSLR_SSZZ_SSFZ.SelectedIndex = 0;
           **/
        }

        //字典管理
        private void btnZDGL_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSZZ.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSXGXY.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = false;
            this.panelSSZZ.Visible = false;
            this.panelZDGL.Visible = true;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelSSXGXY.Visible = false;
            this.panelKSGL.Visible = false;

            this.setListViewDictionary("医保类型字典", 1);
        }

        //数据查询
        private void btnSJCX_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSZZ.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSXGXY.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = false;
            this.panelSSZZ.Visible = false;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = true;
            this.panelCJFX.Visible = false;
            this.panelSSXGXY.Visible = false;
            this.panelKSGL.Visible = false;

            initSJCX();
        }

        //统计分析
        private void btnCJFX_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSZZ.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnSSXGXY.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            //this.panelCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.panelSSLR.Visible = false;
            this.panelSSZZ.Visible = false;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = true;
            this.panelSSXGXY.Visible = false;
            this.panelKSGL.Visible = false;

            initTjfx();
        }

        //科室管理
        private void btnKSGL_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSZZ.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSXGXY.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelSSLR.Visible = false;
            this.panelSSZZ.Visible = false;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelSSXGXY.Visible = false;
            this.panelKSGL.Visible = true;

            //查询科室信息
            setKSGL_room();
        }

        //查询科室信息
        private void setKSGL_room()
        {
            dataTable = DBManager.getInstance().find("select id,r_hospital_name,r_address,r_province, r_rank, r_postcodes,  r_room_name, r_room_tel," +
                "r_room_fax,r_responsible,r_responsible_title, r_responsible_tel, r_responsible_phone,  r_responsible_email," +
                " r_dialyse_center_area, r_dialyse_unit_area, r_start_date from t_room where id = 0");
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                this.tbRoomHispitorName.Text = row["r_hospital_name"].ToString();//医院名称
                this.tbRoomHispitorAddress.Text = row["r_address"].ToString();//医院地址
                this.cbRoomProvince.Text = row["r_province"].ToString();//省
                this.cbRoomRank.Text = row["r_rank"].ToString();//等级
                this.tbRoomPostcode.Text = row["r_postcodes"].ToString();//邮政编码
                this.tbRoomName.Text = row["r_room_name"].ToString();//科室名称
                this.tbRoomTel.Text = row["r_room_tel"].ToString();//科室电话
                this.tbRoomFax.Text = row["r_room_fax"].ToString();//科室传真
                this.tbRoomFZR.Text = row["r_responsible"].ToString();//科室负责人
                this.tbRoomFZRZC.Text = row["r_responsible_title"].ToString();//科室负责人职称
                this.tbRoomFZRDH.Text = row["r_responsible_tel"].ToString();//科室负责人电话
                this.tbRoomFZRSJ.Text = row["r_responsible_phone"].ToString();//科室负责人手机
                this.tbRoomFZRYX.Text = row["r_responsible_email"].ToString();//科室负责人邮箱
                this.tbRoomTXZXMJ.Text = row["r_dialyse_center_area"].ToString();//透析中心面积
                this.tbRoomTXDYMJ.Text = row["r_dialyse_unit_area"].ToString();//透析单元面积
                this.dtpRoomKSRQ.Text = row["r_start_date"].ToString();//开始日期
            }
            else
            {
                MessageBox.Show("当前科室信息为空，请添加科室信息");
            }

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

            //查询科室信息
            setKSGL_room();
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
        private void setListViewEmp(String account, string name)
        {

            string sql = string.Format("select id,e_account as '账号',e_name as '姓名',e_sex as '性别',e_birth as '生日',e_tel as '电话',e_email as '邮箱',e_address as '地址' " +
                "from t_emp where e_account like '%{0}%' and e_name like '%{1}%'", account, name);
            DataTable dt = DBManager.getInstance().find(sql);

            this.dgvKSGL_EMP.DataSource = null;//清空
            this.dgvKSGL_EMP.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//选中整行

            this.dgvKSGL_EMP.DataSource = dt;//设置数据
            this.dgvKSGL_EMP.Columns[0].Visible = false;//隐藏第一列

            // Header以外所有的单元格的背景色
            this.dgvKSGL_EMP.RowsDefaultCellStyle.BackColor = Color.White;

            ////列Header的背景色 CellBorderStyle->Single   EnableHeaderVisualStyles->false
            this.dgvKSGL_EMP.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#4f81DD");
            this.dgvKSGL_EMP.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            this.dgvKSGL_EMP.DefaultCellStyle.SelectionBackColor = Color.Gray;//设置选中的颜色
            
        }


        //通过帐户删除人员信息
        private void btnDeleteEmp_Click(object sender, EventArgs e)
        {
            //if (this.listViewEmp.SelectedItems.Count == 0)
            //{
            //    MessageBox.Show("请选择要删除的科室人员");
            //    return;
            //}
            //string account = this.listViewEmp.SelectedItems[0].Text;
            //string name = this.listViewEmp.SelectedItems[0].SubItems[1].Text;
            //DialogResult dr = MessageBox.Show("删除【" + name + "(" + account + ")】用户?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dr == DialogResult.Yes)
            //{
            //    DBEmp.getInstance().deleteEmpByAccount(account);
            //    this.setListViewEmp(null, null);//重新绑定
            //}

        }

        //打开新增科室职员
        private void btnGoAddEmp_Click(object sender, EventArgs e)
        {
            FormAddEmp.ACCOUNT = null;
            FormAddEmp form = new FormAddEmp();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                this.setListViewEmp(null, null);//重新绑定
            }
        }

        //编辑人员信息
        private void btnGoEditEmp_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgvKSGL_EMP.CurrentRow.Index; //获取选中行的行号
                string account = dgvKSGL_EMP.Rows[index].Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(account))
                {
                    MessageBox.Show("请选择要编辑的科室人员");
                    return;
                }
                //string account = this.listViewEmp.SelectedItems[0].Text;
                FormAddEmp.ACCOUNT = account;
                FormAddEmp form = new FormAddEmp();
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    this.setListViewEmp(null, null);//重新绑定
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("请选择要编辑的科室人员");
            }

        }

        //通过名称和用户名查询用户信息
        private void btnFindEmpByAccountAndName_Click(object sender, EventArgs e)
        {
            this.setListViewEmp(this.tbFindEmpAccount.Text, this.tbFindEmpName.Text);//重新绑定
        }

        //查询医保类型字典
        private void btnDictionaryYBLX_Click(object sender, EventArgs e)
        {
            this.listViewDictionarySSZD.Visible = false;
            this.listViewDictionaryQKZD.Visible = false;
            this.setListViewDictionary("医保类型字典", 1);
        }

        //查询手术字典
        private void btnDictionarySSZD_Click(object sender, EventArgs e)
        {
            this.listViewDictionarySSZD.Visible = !this.listViewDictionarySSZD.Visible;
            this.listViewDictionaryQKZD.Visible = false;

            if (this.listViewDictionarySSZD.Visible)
            {
                //设置listview表头颜色
                this.listViewDictionarySSZD.Items.Clear();
                this.listViewDictionarySSZD.FullRowSelect = true;

                this.listViewDictionarySSZD.BackColor = ColorTranslator.FromHtml("#41aaeb");
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(30, 23);
                this.listViewDictionarySSZD.SmallImageList = imgList;

                DataTable dt = DBDictionary.getInstance().getDictionarysByParentId(2);
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.Text = row["d_name"].ToString();
                    item.ForeColor = Color.White;
                    item.SubItems.Add(row["id"].ToString());
                    //item.SubItems.Add(row["d_name"].ToString());
                    //记住view改成detail
                    this.listViewDictionarySSZD.Items.Add(item);
                    this.listViewDictionarySSZD.Columns[0].Width = 300;
                    //隐藏第二列
                    //this.listViewDictionarySSZD.Columns[1].Width = 0;
                    //this.listViewDictionarySSZD.Items.Add(row["d_name"].ToString());
                }
                this.listViewDictionarySSZD.EndUpdate();
            }
        }

        //查询情况字典
        private void btnDictionaryQKZD_Click(object sender, EventArgs e)
        {
           
            this.listViewDictionarySSZD.Visible = false;
            this.listViewDictionaryQKZD.Visible = !this.listViewDictionaryQKZD.Visible;


            if (this.listViewDictionaryQKZD.Visible)
            {
                //设置listview表头颜色
                this.listViewDictionaryQKZD.Items.Clear();
                this.listViewDictionaryQKZD.FullRowSelect = true;
                this.listViewDictionarySSZD.FullRowSelect = true;

                this.listViewDictionaryQKZD.BackColor = ColorTranslator.FromHtml("#41aaeb");
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(1, 23);
                this.listViewDictionaryQKZD.SmallImageList = imgList;

                DataTable dt = DBDictionary.getInstance().getDictionarysByParentId(3);
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Clear();
                    item.Text = row["d_name"].ToString();
                    item.ForeColor = Color.White;
                    item.SubItems.Add(row["id"].ToString());
                    //item.SubItems.Add(row["d_name"].ToString());
                    //记住view改成detail
                    this.listViewDictionaryQKZD.Items.Add(item);
                    this.listViewDictionaryQKZD.Columns[0].Width = 2000;
                    //this.listViewDictionarySSZD.Items.Add(row["d_name"].ToString());
                }
                this.listViewDictionaryQKZD.EndUpdate();
            }
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

            if (string.IsNullOrEmpty(hispitorName)) { MessageBox.Show("【医院名称】不能为空"); return; }
            //if (string.IsNullOrEmpty(hispitorAddress)) { MessageBox.Show("【医院地址】不能为空"); return; }
            //if (string.IsNullOrEmpty(province)) { MessageBox.Show("【省(直辖市)】不能为空"); return; }
            //if (string.IsNullOrEmpty(rank)) { MessageBox.Show("【等级】不能为空"); return; }
            //if (string.IsNullOrEmpty(postcode)) { MessageBox.Show("【邮政编码】不能为空"); return; }
            //if (postcode.Length != 6) { MessageBox.Show("【邮政编码】 格式有误"); return; }
            if (string.IsNullOrEmpty(roomName)) { MessageBox.Show("【科室名称】不能为空"); return; }
            //if (string.IsNullOrEmpty(roomTel)) { MessageBox.Show("【科室电话】不能为空"); return; }
            //if (roomTel.Length > 11) { MessageBox.Show("【科室电话】 格式有误"); return; }
            //if (string.IsNullOrEmpty(roomFax)) { MessageBox.Show("【科室传真】不能为空"); return; }
            //if (string.IsNullOrEmpty(roomFZR)) { MessageBox.Show("【科室负责人】不能为空"); return; }
            //if (string.IsNullOrEmpty(roomFZRZC)) { MessageBox.Show("【科室负责人职称】不能为空"); return; }
            //if (string.IsNullOrEmpty(roomFZRDH)) { MessageBox.Show("【科室负责人电话】不能为空"); return; }
            //if (roomFZRDH.Length > 11) { MessageBox.Show("【科室负责人电话】 格式有误"); return; }
            //if (string.IsNullOrEmpty(roomFZRSJ)) { MessageBox.Show("【科室负责人手机】不能为空"); return; }
            //if (roomFZRSJ.Length != 11) { MessageBox.Show("【科室负责人手机】 格式有误"); return; }
            //if (string.IsNullOrEmpty(roomFZRYX)) { MessageBox.Show("【科室负责人邮箱】不能为空"); return; }
            //if (!UtilTools.isEmail(roomFZRYX)) { MessageBox.Show("【科室负责人邮箱】 格式有误"); return; }
            //if (string.IsNullOrEmpty(roomTXZXMJ)) { MessageBox.Show("【透析中心面积】 不能为空"); return; }
            //if (string.IsNullOrEmpty(roomTXDYMJ)) { MessageBox.Show("【透析单元面积】 不能为空"); return; }
            //if (string.IsNullOrEmpty(roomKSRQ)) { MessageBox.Show("【开始日期】 不能为空"); return; }


            int flag = 0;
            string sql = "";
            dataTable = DBManager.getInstance().find("select * from t_room where id = 0");
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                //hispitorName, hispitorAddress, province, rank, postcode, roomName, roomTel, roomFax, roomFZR, roomFZRZC, roomFZRDH, roomFZRSJ, roomFZRYX, roomTXZXMJ, roomTXDYMJ, roomKSRQ
                sql = string.Format("update t_room set r_hospital_name='{0}',r_address='{1}',r_province='{2}', r_rank='{3}', r_postcodes='{4}', r_room_name='{5}', r_room_tel='{6}'," +
                "r_room_fax='{7}',r_responsible='{8}',r_responsible_title='{9}', r_responsible_tel='{10}', r_responsible_phone='{11}',  r_responsible_email='{12}'," +
                " r_dialyse_center_area='{13}', r_dialyse_unit_area='{14}', r_start_date='{15}' where id=0",
                 hispitorName, hispitorAddress, province, rank, postcode, roomName, roomTel, roomFax, roomFZR, roomFZRZC, roomFZRDH,
                 roomFZRSJ, roomFZRYX, roomTXZXMJ, roomTXDYMJ, roomKSRQ);
                flag = DBManager.getInstance().edit(sql);
            }
            else
            {
                sql = string.Format("insert into t_room(id,r_hospital_name,r_address,r_province, r_rank, r_postcodes,  r_room_name, r_room_tel," +
               "r_room_fax,r_responsible,r_responsible_title, r_responsible_tel, r_responsible_phone,  r_responsible_email," +
               " r_dialyse_center_area, r_dialyse_unit_area, r_start_date) values(0,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}'," +
               "'{12}','{13}','{14}','{15}')", hispitorName, hispitorAddress, province, rank, postcode, roomName, roomTel, roomFax, roomFZR, roomFZRZC, roomFZRDH,
               roomFZRSJ, roomFZRYX, roomTXZXMJ, roomTXDYMJ, roomKSRQ);
                flag = DBManager.getInstance().add(sql);
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

        //鼠标离开 隐藏手术字典列表
        private void listViewDictionarySSZD_MouseLeave(object sender, EventArgs e)
        {
            this.listViewDictionarySSZD.Visible = false;
        }

        //鼠标离开 隐藏情况字典列表
        private void listViewDictionaryQKZD_MouseLeave(object sender, EventArgs e)
        {
            this.listViewDictionaryQKZD.Visible = false;
        }

        //选中手术字典列表
        private void listViewDictionarySSZD_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (int i in this.listViewDictionarySSZD.SelectedIndices)
            {
                this.listViewDictionarySSZD.Items[i].Selected = true;
                string dName = this.listViewDictionarySSZD.SelectedItems[0].Text;
                int id = Convert.ToInt32(this.listViewDictionarySSZD.SelectedItems[0].SubItems[1].Text);
                //MessageBox.Show(this.listViewDictionarySSZD.SelectedItems[0].SubItems[1].Text);
                this.setListViewDictionary(dName, id);
            }
            this.listViewDictionarySSZD.Visible = false;
        }

        //选中情况字典列表
        private void listViewDictionaryQKZD_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int i in this.listViewDictionaryQKZD.SelectedIndices)
            {
                this.listViewDictionaryQKZD.Items[i].Selected = true;
                string dName = this.listViewDictionaryQKZD.SelectedItems[0].Text;
                int id = Convert.ToInt32(this.listViewDictionaryQKZD.SelectedItems[0].SubItems[1].Text);
                this.setListViewDictionary(dName, id);
            }
            this.listViewDictionaryQKZD.Visible = false;
        }

        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }

        //设置显示字典
        private void setListViewDictionary(string dName, int parentId)
        {
            this.dgvDictionary.DataSource = null;//清空
            this.dgvDictionary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//选中整行

            dictionary_parent_id = parentId;
            this.labelDictionaryShow.Text = dName;//显示选中的字典名称

            string sql = string.Format("select id,d_parent_id,d_order,d_name,d_desc from t_dictionary where d_parent_id={0} order by d_order,d_name", parentId);
            DataTable dt = DBManager.getInstance().find(sql);
           
            this.dgvDictionary.DataSource = dt;

            this.dgvDictionary.Columns[0].Visible = false;
            this.dgvDictionary.Columns[1].Visible = false;

            this.dgvDictionary.Columns[0].FillWeight = 1;
            this.dgvDictionary.Columns[1].FillWeight = 1;
            this.dgvDictionary.Columns[2].FillWeight = 10;
            this.dgvDictionary.Columns[3].FillWeight = 20;
            this.dgvDictionary.Columns[4].FillWeight = 40;

            this.dgvDictionary.Columns[0].HeaderText = "id";
            this.dgvDictionary.Columns[1].HeaderText = "pId";
            this.dgvDictionary.Columns[2].HeaderText = "排列序号";
            this.dgvDictionary.Columns[3].HeaderText = dName;
            this.dgvDictionary.Columns[4].HeaderText = "描述";

            // Header以外所有的单元格的背景色
            this.dgvDictionary.RowsDefaultCellStyle.BackColor = Color.White;

            ////列Header的背景色 CellBorderStyle->Single   EnableHeaderVisualStyles->false
            this.dgvDictionary.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#4f81DD");
            this.dgvDictionary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //设置选中的颜色
            this.dgvDictionary.DefaultCellStyle.SelectionBackColor = Color.Gray;

        }

        //前往添加字典界面
        private void btnGoAddDictionaryy_Click(object sender, EventArgs e)
        {
            FormAddDictionary formAddDictionary = new FormAddDictionary();
            formAddDictionary.setDesc(-1, dictionary_parent_id, "添加字典", this.labelDictionaryShow.Text);
            formAddDictionary.ShowDialog();
            if (formAddDictionary.DialogResult == DialogResult.OK)
            {
                this.setListViewDictionary(this.labelDictionaryShow.Text, dictionary_parent_id);//重新绑定
            }
        }

        //修改字典
        private void btnGoEditDictionary_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgvDictionary.CurrentRow.Index; //获取选中行的行号
                int id = Convert.ToInt32(dgvDictionary.Rows[index].Cells[0].Value.ToString());
               
                if (id == 0)
                {
                    MessageBox.Show("请选择要修改的字典");
                    return;
                }

                FormAddDictionary formAddDictionary = new FormAddDictionary();
                formAddDictionary.setDesc(id, dictionary_parent_id, "修改字典", this.labelDictionaryShow.Text);
                formAddDictionary.ShowDialog();
                if (formAddDictionary.DialogResult == DialogResult.OK)
                {
                    this.setListViewDictionary(this.labelDictionaryShow.Text, dictionary_parent_id);//重新绑定
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("请选择要修改的字典");
            }
            
        }

        //删除字典
        private void btnDeleteDictionary_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgvDictionary.CurrentRow.Index; //获取选中行的行号
                int id = Convert.ToInt32(dgvDictionary.Rows[index].Cells[0].Value.ToString());
                string name = dgvDictionary.Rows[index].Cells[3].Value.ToString();

                if (id == 0)
                {
                    MessageBox.Show("请选择要修改的字典");
                    return;
                }

                DialogResult dr = MessageBox.Show("删除【" + name + "】?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DBDictionary.getInstance().deleteDictionary(id);
                    this.setListViewDictionary(this.labelDictionaryShow.Text, dictionary_parent_id);//重新绑定
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("请选择要删除的字典");
            }
            
        }

        //保存手术记录
        private void btnSaveSSJL_Click(object sender, EventArgs e)
        {
            string name = this.tbSSLR_NAME.Text;//患者姓名
            string sex = this.cbSSLR_SEX.Text;//性别
            string age = this.tbSSLR_AGE.Text;//年龄
            string tel = this.tbSSLR_TEL.Text;//手机号
            string ID = this.tbSSLR_ID.Text;//身份证号
            string yblx = this.cbSSLR_YBLX.Text;//医保类型
            string province = this.cbSSLR_PROVINCE.Text;//省
            string city = this.cbSSLR_CITY.Text;//市
            string county = this.cbSSLR_COUNTY.Text;//县
            string deatilAddress = this.tbSSLR_DETAIL_ADDRESS.Text;//详细地址
            string ctxyy = this.tbSSLR_CTXYY.Text;//常透析医院
            string ctxyylxr = this.tbSSLR_CTXYYLXR.Text;//常透析医院联系人
            string ctxyylxrdh = this.tbSSLR_CTXYYLXRDH.Text;//常透析医院联系人电话
            DateTime ssrq = this.dpSSLR_SSRQ.Value;//手术日期
            string ssdd = this.cbSSLR_SSDD.Text;// SelectedValue.ToString();//手术地点
            string sslx = this.cbSSLR_SSLX.Text;// SelectedValue.ToString();//手术类型
            string ssfs = this.cbSSLR_SSFS.Text;//SelectedValue.ToString();//手术方式
            string ccfs = this.cbSSLR_CCFS.Text;//SelectedValue.ToString();//穿刺方式
            string sszzqx = this.cbSSLR_SSZZQX.Text;//SelectedValue.ToString();//手术追踪期限
            string ssjl = this.rtbSSLR_SSJL.Rtf;//手术记录
            string zdys = this.tbSSLR_ZDYS.Text;//主刀医生
            string zs = this.tbSSLR_ZS.Text;//助手
            string qxfs = this.tbSSLR_QXFS.Text;//器械护士

            string address = province + "-" + city + "-" + county + "-" + deatilAddress+"";

            if (string.IsNullOrEmpty(name)) { MessageBox.Show("【姓名】不能为空"); return; }
            if (string.IsNullOrEmpty(sex)) { MessageBox.Show("【性别】不能为空"); return; }
            if (string.IsNullOrEmpty(tel)) { MessageBox.Show("【电话】不能为空"); return; }
            if (tel.Length != 11) { MessageBox.Show("【电话】格式错误"); return; }
            if (string.IsNullOrEmpty(ID)) { MessageBox.Show("【身份证号】不能为空"); return; }
            if (ID.Length != 18) { MessageBox.Show("【身份证号】格式错误"); return; }
            if (string.IsNullOrEmpty(ctxyy)) { MessageBox.Show("【常透析医院】不能为空"); return; }
            if (string.IsNullOrEmpty(ctxyylxr)) { MessageBox.Show("【常透析医院联系人】不能为空"); return; }
            if (string.IsNullOrEmpty(ctxyylxrdh)) { MessageBox.Show("【常透析医院联系人电话】不能为空"); return; }
            if (ctxyylxrdh.Length != 11) { MessageBox.Show("【常透析医院联系人电话】格式错误"); return; }
            if (string.IsNullOrEmpty(ssjl)) { MessageBox.Show("【手术记录】不能为空"); return; }
            if (string.IsNullOrEmpty(zdys)) { MessageBox.Show("【主刀医生】不能为空"); return; }
            if (string.IsNullOrEmpty(zs)) { MessageBox.Show("【助手】不能为空"); return; }
            if (string.IsNullOrEmpty(qxfs)) { MessageBox.Show("【器械护士】不能为空"); return; }

            int flag = -1;
            
            string sql = "sslr_proc";   //存储过程名称myproc  

            MySqlCommand cmd = DBManager.getInstance().getMySqlCommand(sql);
            //设置Command对象的类型  
            cmd.CommandType = CommandType.StoredProcedure;

            //p_name,p_sex,p_age,p_tel,p_ID,p_health_type,p_address,p_dialyse_hospital,p_dialyse_hospital_contact,p_dialyse_hospital_tel
            //name, sex, age, tel, ID, yblx, address, ctxyy, ctxyylxr, ctxyylxrdh
            cmd.Parameters.AddWithValue("@i_p_name", name);
            cmd.Parameters.AddWithValue("@i_p_sex", sex);
            cmd.Parameters.AddWithValue("@i_p_age", age);
            cmd.Parameters.AddWithValue("@i_p_tel", tel);
            cmd.Parameters.AddWithValue("@i_p_ID", ID);
            cmd.Parameters.AddWithValue("@i_p_health_type", yblx);
            cmd.Parameters.AddWithValue("@i_p_address", address);
            cmd.Parameters.AddWithValue("@i_p_dialyse_hospital", ctxyy);
            cmd.Parameters.AddWithValue("@i_p_dialyse_hospital_contact", ctxyylxr);
            cmd.Parameters.AddWithValue("@i_p_dialyse_hospital_tel", ctxyylxrdh);//sszzqx

            //r_patient_ID,r_date,r_ss_address,r_ss_type,r_ss_method,r_cc_method,r_zd_docotor,r_zs,r_qxhs,r_ss_record,r_is_sszz
            //ID, ssrq, ssdd, sslx, ssfs, ccfs, zdys,zs, qxfs, ssjl
            cmd.Parameters.AddWithValue("@i_r_date", ssrq);
            cmd.Parameters.AddWithValue("@i_r_ss_address", ssdd);
            cmd.Parameters.AddWithValue("@i_r_ss_type", sslx);
            cmd.Parameters.AddWithValue("@i_r_ss_method", ssfs);
            cmd.Parameters.AddWithValue("@i_r_cc_method", ccfs);
            cmd.Parameters.AddWithValue("@i_r_zd_docotor", zdys);
            cmd.Parameters.AddWithValue("@i_r_zs", zs);
            cmd.Parameters.AddWithValue("@i_r_qxhs", qxfs);
            cmd.Parameters.AddWithValue("@i_r_ss_record", ssjl);
            cmd.Parameters.AddWithValue("@i_r_is_sszz", "否");
            cmd.Parameters.Add("@error_code", SqlDbType.Int).Direction = ParameterDirection.Output;

            //执行  
            cmd.ExecuteNonQuery();

            flag = Convert.ToInt32(cmd.Parameters["@error_code"].Value.ToString());
            if (flag >= 0)
            {
                MessageBox.Show("保存成功");
                this.tbSSLR_NAME.Text = "";//患者姓名
                this.cbSSLR_SEX.Text = "";//性别
                this.tbSSLR_AGE.Text = "";//年龄
                this.tbSSLR_TEL.Text = "";//手机号
                this.tbSSLR_ID.Text = "";//身份证号
                this.tbSSLR_DETAIL_ADDRESS.Text = "";//详细地址
                this.tbSSLR_CTXYY.Text = "";//常透析医院
                this.tbSSLR_CTXYYLXR.Text = "";//常透析医院联系人
                this.tbSSLR_CTXYYLXRDH.Text = "";//常透析医院联系人电话
                this.rtbSSLR_SSJL.Text = "";//手术记录
                this.tbSSLR_ZDYS.Text = "";//主刀医生
                this.tbSSLR_ZS.Text = "";//助手
                this.tbSSLR_QXFS.Text = "";//器械护士
            }
            else
            {
                DBManager.getInstance().edit(sql);
                MessageBox.Show("保存失败");
            }
            
        }

        //监听身份证输入
        private void tbSSLR_ID_TextChanged(object sender, EventArgs e)
        {
            if (this.tbSSLR_ID.Text.Length == 18)
            {
                //int flag = 0;
                for (int i = 0; i < 17; i++)
                {

                    if (!UtilTools.IsNumber(this.tbSSLR_ID.Text.ToString().Substring(i, 1)) && UtilTools.getAgeByID(this.tbSSLR_ID.Text) <=0)
                    {
                        MessageBox.Show("身份证输入有误");
                        return;
                    }
                    // flag++;
                }
                this.tbSSLR_AGE.Text = UtilTools.getAgeByID(this.tbSSLR_ID.Text) + "";
            }
        }

        //取消手术记录
        private void btnRecordClear_Click(object sender, EventArgs e)
        {
            initSSLR();
        }

        //手术记录名字输入后 回车键查询
        private void tbSSLR_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.tbSSLR_NAME.Text))
                {
                    MessageBox.Show("请输入名称");
                    return;
                }

                //r_patient_ID,r_date,r_ss_address,r_ss_type,r_ss_method,r_cc_method,r_zd_docotor,r_zs,r_qxhs,r_ss_record,r_is_sszz
                string sql = string.Format("select p_name,p_sex,p_age,p_tel,p_ID,p_health_type,p_address,p_dialyse_hospital,p_dialyse_hospital_contact," +
                    "p_dialyse_hospital_tel from t_patient where p_name='{0}'", this.tbSSLR_NAME.Text);
                dataTable = DBManager.getInstance().find(sql);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows.Count > 1)
                    {
                        MessageBox.Show("该名称有多个，请再输入身份证");
                    }
                    else
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            this.tbSSLR_NAME.Text = row["p_name"].ToString();//患者姓名
                            this.cbSSLR_SEX.Text = row["p_sex"].ToString();//性别
                            this.tbSSLR_AGE.Text = row["p_age"].ToString();//年龄
                            this.tbSSLR_TEL.Text = row["p_tel"].ToString();//手机号
                            this.tbSSLR_ID.Text = row["p_ID"].ToString();//身份证号
                            this.cbSSLR_YBLX.Text = row["p_health_type"].ToString();//医保类型
                            this.cbSSLR_PROVINCE.Text = row["p_address"].ToString().Split('-')[0];//省
                            this.cbSSLR_CITY.Text = row["p_address"].ToString().Split('-')[1];//市
                            this.cbSSLR_COUNTY.Text = row["p_address"].ToString().Split('-')[2];//县
                            this.tbSSLR_DETAIL_ADDRESS.Text = row["p_address"].ToString().Split('-')[3];//详细地址
                            this.tbSSLR_CTXYY.Text = row["p_dialyse_hospital"].ToString();//常透析医院
                            this.tbSSLR_CTXYYLXR.Text = row["p_dialyse_hospital_contact"].ToString();//常透析医院联系人
                            this.tbSSLR_CTXYYLXRDH.Text = row["p_dialyse_hospital_tel"].ToString();//常透析医院联系人电话
                        }

                        //查询该患者的最近一次的手术记录
                        sql = string.Format("select id,r_date,r_ss_address,r_ss_type,r_ss_method,r_cc_method,r_zd_docotor,r_zs,r_qxhs,r_ss_record,r_is_sszz" +
                        " from t_record where r_patient_ID='{0}' order by id DESC limit 1", this.tbSSLR_ID.Text);
                        dataTable = DBManager.getInstance().find(sql);
                        if (dataTable != null && dataTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                this.labelSSLR_SSJL_ID.Text = row["id"].ToString();//手术记录id
                                this.dpSSLR_SSRQ.Value = Convert.ToDateTime(row["r_date"].ToString());//手术日期
                                this.cbSSLR_SSDD.Text = row["r_ss_address"].ToString();//手术地点
                                this.cbSSLR_SSLX.Text = row["r_ss_type"].ToString();//手术类型
                                this.cbSSLR_SSFS.Text = row["r_ss_method"].ToString();//手术方式
                                this.cbSSLR_CCFS.Text = row["r_cc_method"].ToString();//穿刺方式
                                this.rtbSSLR_SSJL.Text = row["r_ss_record"].ToString();//手术记录
                                this.tbSSLR_ZDYS.Text = row["r_zd_docotor"].ToString();//主刀医生
                                this.tbSSLR_ZS.Text = row["r_zs"].ToString();//助手
                                this.tbSSLR_QXFS.Text = row["r_qxhs"].ToString();//器械护士
                            }
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("该名称不存在");
                }

            }
        }

        //手术记录身份证输入后 回车键查询
        private void tbSSLR_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.tbSSLR_ID.Text))
                {
                    MessageBox.Show("请输入身份证");
                    return;
                }

                string sql = string.Format("select p_name,p_sex,p_age,p_tel,p_ID,p_health_type,p_address,p_dialyse_hospital,p_dialyse_hospital_contact," +
                    "p_dialyse_hospital_tel from t_patient where p_ID='{0}'", this.tbSSLR_ID.Text);
                dataTable = DBManager.getInstance().find(sql);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        this.tbSSLR_NAME.Text = row["p_name"].ToString();//患者姓名
                        this.cbSSLR_SEX.Text = row["p_sex"].ToString();//性别
                        this.tbSSLR_AGE.Text = row["p_age"].ToString();//年龄
                        this.tbSSLR_TEL.Text = row["p_tel"].ToString();//手机号
                        this.tbSSLR_ID.Text = row["p_ID"].ToString();//身份证号
                        this.cbSSLR_YBLX.Text = row["p_health_type"].ToString();//医保类型
                        this.cbSSLR_PROVINCE.Text = row["p_address"].ToString().Split('-')[0];//省
                        this.cbSSLR_CITY.Text = row["p_address"].ToString().Split('-')[1];//市
                        this.cbSSLR_COUNTY.Text = row["p_address"].ToString().Split('-')[2];//县
                        this.tbSSLR_DETAIL_ADDRESS.Text = row["p_address"].ToString().Split('-')[3];//详细地址
                        this.tbSSLR_CTXYY.Text = row["p_dialyse_hospital"].ToString();//常透析医院
                        this.tbSSLR_CTXYYLXR.Text = row["p_dialyse_hospital_contact"].ToString();//常透析医院联系人
                        this.tbSSLR_CTXYYLXRDH.Text = row["p_dialyse_hospital_tel"].ToString();//常透析医院联系人电话
                    }

                    //查询该患者的最近一次的手术记录
                    sql = string.Format("select id,r_date,r_ss_address,r_ss_type,r_ss_method,r_cc_method,r_zd_docotor,r_zs,r_qxhs,r_ss_record,r_is_sszz" +
                    " from t_record where r_patient_ID='{0}' order by id DESC limit 1", this.tbSSLR_ID.Text);
                    dataTable = DBManager.getInstance().find(sql);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            this.labelSSLR_SSJL_ID.Text = row["id"].ToString();//手术记录id
                            this.dpSSLR_SSRQ.Value = Convert.ToDateTime(row["r_date"].ToString());//手术日期
                            this.cbSSLR_SSDD.Text = row["r_ss_address"].ToString();//手术地点
                            this.cbSSLR_SSLX.Text = row["r_ss_type"].ToString();//手术类型
                            this.cbSSLR_SSFS.Text = row["r_ss_method"].ToString();//手术方式
                            this.cbSSLR_CCFS.Text = row["r_cc_method"].ToString();//穿刺方式
                            this.rtbSSLR_SSJL.Text = row["r_ss_record"].ToString();//手术记录
                            this.tbSSLR_ZDYS.Text = row["r_zd_docotor"].ToString();//主刀医生
                            this.tbSSLR_ZS.Text = row["r_zs"].ToString();//助手
                            this.tbSSLR_QXFS.Text = row["r_qxhs"].ToString();//器械护士
                        }
                    }
                   
                }
                else
                {
                    MessageBox.Show("该身份证不存在");
                }
            }

        }

       //输入数字包括小数点
        private void decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            TextBox textBox1 = (TextBox)sender;

            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)

                e.Handled = true;


            //小数点的处理。

            if ((int)e.KeyChar == 46)                           //小数点
            {

                if (textBox1.Text.Length <= 0)

                    e.Handled = true;   //小数点不能在第一位

                else
                {

                    float f;

                    float oldf;

                    bool b1 = false, b2 = false;

                    b1 = float.TryParse(textBox1.Text, out oldf);

                    b2 = float.TryParse(textBox1.Text + e.KeyChar.ToString(), out f);

                    if (b2 == false)
                    {

                        if (b1 == true)

                            e.Handled = true;

                        else

                            e.Handled = false;

                    }

                }

            }
        }

        //新增手术追踪
        private void btnSaveSSLR_SSZZ_Click(object sender, EventArgs e)
        {
            int recordId = -1;//手术记录id
            try
            {
                recordId = Convert.ToInt32(this.labelSSLR_SSJL_ID.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show("请查询手术记录单，再进行跟踪");
                return;
            }

            string sfct = this.cbSSLR_SSZZ_SFTC.Text; //是否畅通
            string ywxlbct = this.cbSSLR_SSZZ_YWXLBCT.Text; //有无血液不畅通
            string ywxm = this.cbSSLR_SSZZ_YWXM.Text; //有无胸闷
            string ywccbwpfgmqk = this.cbSSLR_SSZZ_YWCCBWPFGMQK.Text; //有无穿刺部位皮肤过敏情况：
            string ywbfz = this.cbSSLR_SSZZ_YWBFZ.Text; //有无并发症
            string ywxbjmqz = this.cbSSLR_SSZZ_YWXBJMQZ.Text; //有无胸壁静脉曲张
            string grkzfs = this.cbSSLR_SSZZ_GRKZFS.Text;//SelectedValue.ToString(); //感染控制方式
            string nwzwdlqk = this.cbSSLR_SSZZ_NWZWDLQK.Text;//SelectedValue.ToString(); //内痿自我锻炼情况
            string ccbwpfqk = this.cbSSLR_SSZZ_CCBWPFQK.Text;//SelectedValue.ToString(); //穿刺部位皮肤情况
            string zwcmjtzqk = this.tbSSLR_SSZZ_ZWQMJTZQK.Text; //自我触摸及听诊情况

            if (string.IsNullOrEmpty(sfct)) { MessageBox.Show("【是否畅通】不能为空"); return; }
            if (string.IsNullOrEmpty(ywxlbct)) { MessageBox.Show("【有无血液不畅通】不能为空"); return; }
            if (string.IsNullOrEmpty(ywxm)) { MessageBox.Show("【有无胸闷】不能为空"); return; }
            if (string.IsNullOrEmpty(ywccbwpfgmqk)) { MessageBox.Show("【有无穿刺部位皮肤过敏情况】不能为空"); return; }
            if (string.IsNullOrEmpty(ywbfz)) { MessageBox.Show("【有无并发症】不能为空"); return; }
            if (string.IsNullOrEmpty(ywxbjmqz)) { MessageBox.Show("【有无胸壁静脉曲张】不能为空"); return; }
            if (string.IsNullOrEmpty(grkzfs)) { MessageBox.Show("【感染控制方式】不能为空"); return; }
            if (string.IsNullOrEmpty(nwzwdlqk)) { MessageBox.Show("【内痿自我锻炼情况】不能为空"); return; }
            if (string.IsNullOrEmpty(ccbwpfqk)) { MessageBox.Show("【穿刺部位皮肤情况】不能为空"); return; }
            if (string.IsNullOrEmpty(zwcmjtzqk)) { MessageBox.Show("【自我触摸及听诊情况】不能为空"); return; }

            //判断手术记录是否存在
            string sql = string.Format("select * from t_record where id={0}", recordId);
            dataTable = DBManager.getInstance().find(sql);
            if (dataTable == null && dataTable.Rows.Count == 0)
            {
                MessageBox.Show("手术记录为空，不能进行手术跟踪");
                return;
            }

            //判断手术记录的手术追踪是否存在
            sql = string.Format("select * from t_track where t_record_id={0}", recordId);
            dataTable = DBManager.getInstance().find(sql);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("该手术已追踪，是否继续追踪?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }

            //添加手术追踪
            sql = string.Format("insert into t_track(t_record_id,t_sszz_deadline,t_sfrq,t_ccfs,t_ssct,t_ywxlbct,t_ywxm,t_ywccbwphgmqk,t_ywbfz,t_ywxbjmqz," +
                "t_grkzfs,t_nwzwdlqk,t_ccbwpfqk,t_sffz,t_jmyfw,t_sjqbxsjmy,t_xjqbxsjmy,t_xll,t_ypzxsj,t_zwcmzcjtzqk,t_sfys)"+
                " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                   recordId,sfct, ywxlbct, ywxm, ywccbwpfgmqk, ywbfz, ywxbjmqz, grkzfs, nwzwdlqk, ccbwpfqk, zwcmjtzqk);
            int flag = DBManager.getInstance().add(sql);
            if (flag > 0)
            {
                //修改手术记录为已追踪
                sql = string.Format("update t_record set r_is_sszz='是' where id={0}", recordId);
                flag = DBManager.getInstance().edit(sql);
                if (flag > 0)
                {
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("保存失败");
                }
                //MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }

        //数据查询-->手术记录单
        private void btnSJCX_SSJLD_Click(object sender, EventArgs e)
        {
            initSJCX();
        }

        //数据查询-->手术追踪单
        private void btnSJCX_SSZZCX_Click(object sender, EventArgs e)
        {
            this.btnSJCX_SSZZCX.ForeColor = ColorTranslator.FromHtml("#3399ff");
            this.btnSJCX_SSZZCX.BackColor = Color.White;

            this.btnSJCX_SSJLD.ForeColor = Color.White;
            this.btnSJCX_SSJLD.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelSJCX_SSZZD.Visible = true;
            this.panelSJCX_SSJLD.Visible = false;

            string sql = "select 姓名,性别,年龄,医保类型,穿刺方式,手术类型,常透析医院,联系电话,是否手术追踪" +
                        "t.t_sszz_deadline,t.t_sfrq,t.t_ccfs,t.t_ssct,t.t_ywxlbct,t.t_ywxm, " +
                        "t.t_ywccbwphgmqk,t.t_ywbfz,t.t_ywxbjmqz,t.t_grkzfs,t.t_nwzwdlqk,t.t_ccbwpfqk, " +
                        "t.t_sffz,t.t_jmyfw,t.t_sjqbxsjmy,t.t_xjqbxsjmy,t.t_xll,t.t_ypzxsj,t.t_zwcmzcjtzqk, " +
                        "t.t_sfys " +
                    "from t_track t";


            btnSJCX_SSZZ_FIND_Click(null, null);
        }


        //初始化数据查询界面
        private void initSJCX()
        {
            this.btnSJCX_SSJLD.ForeColor = ColorTranslator.FromHtml("#3399ff");
            this.btnSJCX_SSJLD.BackColor = Color.White;

            this.btnSJCX_SSZZCX.ForeColor = Color.White;
            this.btnSJCX_SSZZCX.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelSJCX_SSJLD.Visible = true;
            this.panelSJCX_SSZZD.Visible = false;

            this.gbSJCX_SSJLD_SSZZ.Visible = false;

            string sql = string.Format("select r.id 'id',p.p_name '姓名',r.r_date '手术日期' from t_patient p,t_record r where p.p_ID = r.r_patient_ID");
            selectRecords(sql);

            sql = string.Format("select r_hospital_name,r_room_name from t_room");
            dataTable = DBManager.getInstance().find(sql);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    this.labelSSJLD_TITLE.Text = row["r_hospital_name"].ToString() + row["r_room_name"].ToString();
                }
            }

            initRecord();
            
        }

        //初始化手术记录单
        private void initRecord()
        {
            this.cbSJCX_XSSSZZ.Checked = false;

            this.all_name.Text = "";
            this.all_sex.Text = "";
            this.all_age.Text = "";
            this.all_tel.Text = "";
            this.all_ID.Text = "";
            this.all_yblx.Text = "";
            this.all_czdz.Text = "";
            this.all_ctxyy.Text = "";
            this.all_ctxyylxr.Text = "";
            this.all_ctxyylxrdh.Text = "";
            this.all_ssrq.Text = "";
            this.all_ssdd.Text = "";
            this.all_sslx.Text = "";
            this.all_ssfs.Text = "";
            this.all_ccfs.Text = "";
            //this.all_ccfs_1.Text = row["r_cc_method"].ToString();
            this.all_zdys.Text = "";
            this.all_zs.Text = "";
            this.all_qxhs.Text = "";
            this.all_ssjl.Text = "";
            //this.all_name.Text = row["r_is_sszz"].ToString();
            this.all_sszzqx.Text = "";
            this.all_sfrq.Text = "";
            this.all_ccfs_1.Text = "";
            this.all_ssct.Text = "";
            this.all_ywxlbct.Text = "";
            this.all_ywxm.Text = "";
            this.all_ywccbwpfgmqk.Text = "";
            this.all_ywbfz.Text = "";
            this.all_ywxbjmqz.Text = "";
            this.all_grkzfs.Text = "";
            this.all_nwzwdlqk.Text = "";
            this.all_ccbwpfqk.Text = "";
            this.all_sffz.Text = "";
            this.all_grkzfs.Text = "";
            this.all_jmyfw.Text = "";
            this.all_sjqbxsjmy.Text = "";
            this.all_xjqbxsjmy.Text = "";
            this.all_xll.Text = "";
            this.all_ypzxsj.Text = "";
            this.all_zwcmzcjtzqk.Text = "";
            this.all_sfys.Text = "";
        }

        //设置手术记录列表
        private void selectRecords(string sql)
        {
           
            this.dgvSJCX_RECORDS.DataSource = null;//清空
            this.dgvSJCX_RECORDS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//选中整行

            DataTable dt = DBManager.getInstance().find(sql);
            this.dgvSJCX_RECORDS.DataSource = dt;

            //第一列隐藏
            this.dgvSJCX_RECORDS.Columns[0].Visible = false;
            // Header以外所有的单元格的背景色
            dgvDictionary.RowsDefaultCellStyle.BackColor = Color.White;

            ////列Header的背景色 CellBorderStyle->Single   EnableHeaderVisualStyles->false
            dgvDictionary.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#4f81DD");
            dgvDictionary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvDictionary.DefaultCellStyle.SelectionBackColor = Color.Gray;//设置选中的颜色
        }

        //搜索手术记录单
        private void btnSJCX_FIND_RECORDS_Click(object sender, EventArgs e)
        {
            this.cbSJCX_XSSSZZ.Checked = false;
            string name = this.tbSJCX_NAME.Text;//姓名

            string kssj = this.dtpSJCX_KSSJ.Value.ToString("yyyy-MM") + "-01";//开始时间
            string jssj = this.dtpSJCX_JSSJ.Value.ToString("yyyy-MM") + "-31";//结束时间

            string sqlStr = "select r.id '身份证',p.p_name '姓名',r.r_date '手术日期' from t_patient p,t_record r where p.p_ID = r.r_patient_ID " +
                "and r.r_date > '{0}' and r.r_date <= '{1}' ";
            if (!string.IsNullOrEmpty(name))
            {
                sqlStr += "and p.p_name like '%{2}%' ";
            }

            //sqlStr += " GROUP BY p.p_ID";
            string sql = string.Format(sqlStr,kssj,jssj,name);
            selectRecords(sql);
        }

        //选中手术记录-》弹出手术记录及患者信息
        private void dgvSJCX_RECORDS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            initRecord();
            int index = this.dgvSJCX_RECORDS.CurrentRow.Index;
            string id = this.dgvSJCX_RECORDS.Rows[index].Cells[0].Value.ToString();
            
            string sql = string.Format(
                "select "+
	                "p.p_name,p.p_sex,p.p_age,p.p_tel,p.p_ID,p.p_health_type,p.p_address, "+
                    "p.p_dialyse_hospital,p.p_dialyse_hospital_contact,p.p_dialyse_hospital_tel, " +
                    "r.r_date,r.r_ss_address,r.r_ss_type,r.r_ss_method,r.r_cc_method,r.r_zd_docotor, " +
                    "r.r_zs,r_qxhs,r.r_ss_record,r.r_is_sszz " +
                "from t_patient p,t_record r " +
                "where  " +
                    "p.p_ID = r.r_patient_ID " +
                    "and r.id={0}", id
            );
            dataTable = DBManager.getInstance().find(sql);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    this.all_name.Text = row["p_name"].ToString();
                    this.all_sex.Text = row["p_sex"].ToString();
                    this.all_age.Text = row["p_age"].ToString();
                    this.all_tel.Text = row["p_tel"].ToString();
                    this.all_ID.Text = row["p_ID"].ToString();
                    this.all_yblx.Text = row["p_health_type"].ToString();
                    this.all_czdz.Text = row["p_address"].ToString();
                    this.all_ctxyy.Text = row["p_dialyse_hospital"].ToString();
                    this.all_ctxyylxr.Text = row["p_dialyse_hospital_contact"].ToString();
                    this.all_ctxyylxrdh.Text = row["p_dialyse_hospital_tel"].ToString();
                    this.all_ssrq.Text = row["r_date"].ToString();
                    this.all_ssdd.Text = row["r_ss_address"].ToString();
                    this.all_sslx.Text = row["r_ss_type"].ToString();
                    this.all_ssfs.Text = row["r_ss_method"].ToString();
                    this.all_ccfs.Text = row["r_cc_method"].ToString();
                    //this.all_ccfs_1.Text = row["r_cc_method"].ToString();
                    this.all_zdys.Text = row["r_zd_docotor"].ToString();
                    this.all_zs.Text = row["r_zs"].ToString();
                    this.all_qxhs.Text = row["r_qxhs"].ToString();
                    this.all_ssjl.Text = row["r_ss_record"].ToString();
                }
            }
        }

        //是否显示手术追踪
        private void cbSJCX_XSSSZZ_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb =  (CheckBox)sender;
            this.gbSJCX_SSJLD_SSZZ.Visible = false;

            if (this.dgvSJCX_RECORDS.CurrentRow == null)
            {
                MessageBox.Show("你还没有选择一条记录，不能显示手术追踪");
                return;
            }
            
            
            if (cb.Checked)
            {
                int index = this.dgvSJCX_RECORDS.CurrentRow.Index;
                string id = this.dgvSJCX_RECORDS.Rows[index].Cells[0].Value.ToString();

                string sql = string.Format(
                    "select " +
                        "t.t_sszz_deadline,t.t_sfrq,t.t_ccfs,t.t_ssct,t.t_ywxlbct,t.t_ywxm, " +
                        "t.t_ywccbwphgmqk,t.t_ywbfz,t.t_ywxbjmqz,t.t_grkzfs,t.t_nwzwdlqk,t.t_ccbwpfqk, " +
                        "t.t_sffz,t.t_jmyfw,t.t_sjqbxsjmy,t.t_xjqbxsjmy,t.t_xll,t.t_ypzxsj,t.t_zwcmzcjtzqk, " +
                        "t.t_sfys " +
                    "from t_track t " +
                    "where t_record_id={0}", id
                );
                dataTable = DBManager.getInstance().find(sql);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        this.all_sszzqx.Text = row["t_sszz_deadline"].ToString();
                        this.all_sfrq.Text = row["t_sfrq"].ToString();
                        this.all_ccfs_1.Text = row["t_ccfs"].ToString();
                        this.all_ssct.Text = row["t_ssct"].ToString();
                        this.all_ywxlbct.Text = row["t_ywxlbct"].ToString();
                        this.all_ywxm.Text = row["t_ywxm"].ToString();
                        this.all_ywccbwpfgmqk.Text = row["t_ywccbwphgmqk"].ToString();
                        this.all_ywbfz.Text = row["t_ywbfz"].ToString();
                        this.all_ywxbjmqz.Text = row["t_ywxbjmqz"].ToString();
                        this.all_grkzfs.Text = row["t_grkzfs"].ToString();
                        this.all_nwzwdlqk.Text = row["t_nwzwdlqk"].ToString();
                        this.all_ccbwpfqk.Text = row["t_ccbwpfqk"].ToString();
                        this.all_sffz.Text = row["t_sffz"].ToString();
                        this.all_grkzfs.Text = row["t_grkzfs"].ToString();
                        this.all_jmyfw.Text = row["t_jmyfw"].ToString();
                        this.all_sjqbxsjmy.Text = row["t_sjqbxsjmy"].ToString();
                        this.all_xjqbxsjmy.Text = row["t_xjqbxsjmy"].ToString();
                        this.all_xll.Text = row["t_xll"].ToString();
                        this.all_ypzxsj.Text = row["t_ypzxsj"].ToString();
                        this.all_zwcmzcjtzqk.Text = row["t_zwcmzcjtzqk"].ToString();
                        this.all_sfys.Text = row["t_sfys"].ToString();

                    }
                    this.gbSJCX_SSJLD_SSZZ.Visible = true;
                }
                else
                {
                    MessageBox.Show("该记录没有手术追踪");
                }
            }
        }

        //查询手术追踪
        private void btnSJCX_SSZZ_FIND_Click(object sender, EventArgs e)
        {
            this.dgvSJCX_SSZZ.DataSource = null;

            string name = this.tbSJCX_SSZZ_NAME.Text;//姓名
            
            string kssj = this.dtpSJCX_SSZZ_KSRQ.Value.ToString("yyyy-MM") + "-01";//开始时间
            string jssj = this.dtpSJCX_SSZZ_JSRQ.Value.ToString("yyyy-MM") + "-31";//结束时间
            string sszz = this.cbSJCX_SSZZ_CXTJ.Text;//追踪状态 未追踪 已追踪  全部

            string sql = "select p.p_name 姓名,p.p_sex 性别,p.p_age 年龄,p.p_health_type 医保类型,r.r_cc_method 穿刺方式,r.r_ss_type 手术类型,"+
                "p.p_dialyse_hospital 常透析医院,p.p_tel 联系电话,r.r_is_sszz 是否手术跟踪" +
                " from t_patient p left join t_record r on r.r_patient_ID=p.p_ID where r_date>'{0}' and r_date<'{1}' ";
            if (!string.IsNullOrEmpty(name))
            {
                sql += "and p.p_name like '%"+name+"%' ";
            }

            if (!string.IsNullOrEmpty(sszz))
            {
                if (sszz.Equals("已追踪"))
                {
                    sql += "and r.r_is_sszz = '是' ";
                }
                else if (sszz.Equals("未追踪"))
                {
                    sql += "and r.r_is_sszz = '否' ";
                }
            }

            dataTable = DBManager.getInstance().find(string.Format(sql, kssj, jssj));

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("当前数据为空");
            }
            else
            {
                this.dgvSJCX_SSZZ.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//选中整行
                this.dgvSJCX_SSZZ.DataSource = null;//清空
                this.dgvSJCX_SSZZ.DataSource = dataTable;

                // Header以外所有的单元格的背景色
                this.dgvSJCX_SSZZ.RowsDefaultCellStyle.BackColor = Color.White;

                ////列Header的背景色 CellBorderStyle->Single   EnableHeaderVisualStyles->false
                this.dgvSJCX_SSZZ.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#4f81DD");
                this.dgvSJCX_SSZZ.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                this.dgvSJCX_SSZZ.DefaultCellStyle.SelectionBackColor = Color.Gray;//设置选中的颜色

            }
        }

        static int TJ_FLAG = 0;//0表示是基本信息统计，1表示是手术统计
        //基本信息统计
        private void btnTJFX_JBXXTJ_Click(object sender, EventArgs e)
        {
            initTjfx();
        }

        //初始化信息统计
        private void initTjfx()
        {
            TJ_FLAG = 0;

            this.btnTJFX_JBXXTJ.ForeColor = ColorTranslator.FromHtml("#3399ff");
            this.btnTJFX_JBXXTJ.BackColor = Color.White;

            this.btnTJFX_SSTJ.ForeColor = Color.White;
            this.btnTJFX_SSTJ.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.btnTJFX_GZLTJ.ForeColor = Color.White;
            this.btnTJFX_GZLTJ.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelTJFX_PIE.Visible = true;
            this.panelTJFX_GZLTJ.Visible = false;

            JBXXTJ_PIE();
        }

        //基本信息统计饼图
        private void JBXXTJ_PIE()
        {
            tjSex(null,null);
            tjAge(null, null);
            tjZWDL(null,null);
            tjCCBWPFTJ(null,null);
        }

        //统计性别
        private void tjSex(string kssj,string jssj)
        {
            this.chartPie_1.Series[0].Points.Clear();
            List<int> data = new List<int>();
            if(string.IsNullOrEmpty(kssj) &&　string.IsNullOrEmpty(jssj))
            {
                string sql = 
                    "select p.p_sex,count(*) as total "+
                    "from t_patient p,t_record r "+
                    "where p.p_ID=r.r_patient_ID  group by p.p_sex";
                dataTable = DBManager.getInstance().find(sql);
            }else{
                string sql = string.Format(
                     "select p.p_sex,count(*) as total "+
                    "from t_patient p,t_record r "+
                    "where p.p_ID=r.r_patient_ID and r.r_date > '{0}' and r.r_date < '{1}' "+
                    "group by p.p_sex", kssj, jssj);
                dataTable = DBManager.getInstance().find(sql);
            }

            if(dataTable != null && dataTable.Rows.Count > 0)
            {
                List<int> arrays = new List<int>();
                List<string> sexs = new List<string>();
                foreach(DataRow row in dataTable.Rows)
                {
                    int y = Convert.ToInt32(row["total"].ToString());
                    string x = row["p_sex"].ToString();
                   // string legel = row["p_sex"].ToString() + "[" + y + "](" + row["percent"].ToString() + ")";
                    arrays.Add(y);
                    sexs.Add(x);
                }

                this.chartPie_1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                this.chartPie_1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                this.chartPie_1.Series[0].Points.DataBindXY(sexs, arrays);
                //this.chartPie_1.Series[0].Legend =
            }
           
            this.labelTJFX_PIE_1.Text = "男女比例";
        }

        //统计年龄
        private void tjAge(string kssj, string jssj)
        {
            this.chartPie_2.Series[0].Points.Clear();
            List<int> data = new List<int>();
            if (string.IsNullOrEmpty(kssj) && string.IsNullOrEmpty(jssj))
            {
                string sql =
                    "select p_age,count(*) as total,"+
	                    " case"+
		                    " when p_age between 1 and 18 then '[<=18]'"+
		                    " when p_age between 10 and 30 then '[18-30]'"+
		                    " when p_age between 30 and 40 then '[30-40]'"+
		                    " when p_age between 40 and 50 then '[40-50]'"+
		                    " when p_age between 50 and 60 then '[50-60]'"+
		                    " when p_age between 60 and 70 then '[60-70]'"+
		                    " when p_age between 70 and 80 then '[70-80]'"+
		                    " when p_age > 80 then '[>80]'"+
	                    " end as age_temp"+
                    " from t_patient p,t_record r"+
                    " where"+
                    " p.p_ID = r.r_patient_ID"+
                    " group by age_temp";
                dataTable = DBManager.getInstance().find(sql);
            }
            else
            {
                string sql =
                    "select p_age,count(*) as total,"+
                        " case" +
                            " when p_age between 1 and 18 then '[<=18]'" +
                            " when p_age between 10 and 30 then '[18-30]'" +
                            " when p_age between 30 and 40 then '[30-40]'" +
                            " when p_age between 40 and 50 then '[40-50]'" +
                            " when p_age between 50 and 60 then '[50-60]'" +
                            " when p_age between 60 and 70 then '[60-70]'" +
                            " when p_age between 70 and 80 then '[70-80]'" +
                            " when p_age > 80 then '[>80]'" +
                        " end as age_temp" +
                    " from t_patient p,t_record r" +
                    " where" +
                    " p.p_ID = r.r_patient_ID" +
                    " and r.r_date > '{0}' and r.r_date < '{1}'"+
                    " group by age_temp";
                sql = string.Format(sql, kssj, jssj);
                dataTable = DBManager.getInstance().find(sql);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<int> arrays = new List<int>();
                List<string> ages = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int y = Convert.ToInt32(row["total"].ToString());
                    string x = row["age_temp"].ToString();
                    //string legel = row["p_age"].ToString() + "[" + y + "](" + row["percent"].ToString() + ")";
                    arrays.Add(Convert.ToInt32(row["total"].ToString()));
                    ages.Add(x);
                }

                this.chartPie_2.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                this.chartPie_2.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                this.chartPie_2.Series[0].Points.DataBindXY(ages, arrays);
            }

            this.labelTJFX_PIE_2.Text = "年龄比例";
        }

        //统计自我锻炼情况统计
        private void tjZWDL(string kssj, string jssj)
        {
            this.chartPie_3.Series[0].Points.Clear();
            List<int> data = new List<int>();
            if (string.IsNullOrEmpty(kssj) && string.IsNullOrEmpty(jssj))
            {
                string sql =
                    "select t.t_nwzwdlqk,count(*) as total "+
                    "from t_track t,t_record r  "+
                    "where t.t_record_id = r.id "+
                    "group by t.t_nwzwdlqk";
                dataTable = DBManager.getInstance().find(sql);
            }
            else
            {
                string sql = string.Format(
                     "select t.t_nwzwdlqk,count(*) as total "+
                    "from t_track t,t_record r  "+
                    "where t.t_record_id = r.id "+
                    "and r.r_date > '{0}' and r.r_date < '{1}' "+
                    "group by t.t_nwzwdlqk", kssj, jssj);
                dataTable = DBManager.getInstance().find(sql);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<int> arrays = new List<int>();
                List<string> values = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int y = Convert.ToInt32(row["total"].ToString());
                    string x = row["t_nwzwdlqk"].ToString();
                    //string legel = row["t_nwzwdlqk"].ToString() + "[" + y + "](" + row["percent"].ToString() + "%)";
                    arrays.Add(y);
                    values.Add(x);
                }

                this.chartPie_3.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                this.chartPie_3.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                this.chartPie_3.Series[0].Points.DataBindXY(values, arrays);
                
            }

            this.labelTJFX_PIE_3.Text = "自我锻炼情况统计";
        }

        //穿刺部位皮肤情况统计
        private void tjCCBWPFTJ(string kssj, string jssj)
        {
            this.chartPie_4.Series[0].Points.Clear();
            List<int> data = new List<int>();
            if (string.IsNullOrEmpty(kssj) && string.IsNullOrEmpty(jssj))
            {
                string sql =
                    "select t.t_ccbwpfqk,count(*) as total " +
                    "from t_track t,t_record r  "+
                    "where t.t_record_id = r.id "+
                    "group by t.t_ccbwpfqk";
                dataTable = DBManager.getInstance().find(sql);
            }
            else
            {
                string sql = string.Format(
                     "select t.t_ccbwpfqk,count(*) as total " +
                    "from t_track t,t_record r  "+
                    "where t.t_record_id = r.id "+
                    "and r.r_date > '{0}' and r.r_date < '{1}' "+
                    "group by t.t_ccbwpfqk", kssj, jssj);
                dataTable = DBManager.getInstance().find(sql);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<int> arrays = new List<int>();
                List<string> values = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int y = Convert.ToInt32(row["total"].ToString());
                    string x = row["t_ccbwpfqk"].ToString();
                    //string legel = row["t_nwzwdlqk"].ToString() + "[" + y + "](" + row["percent"].ToString() + "%)";
                    arrays.Add(y);
                    values.Add(x);
                }

                this.chartPie_4.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                this.chartPie_4.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                this.chartPie_4.Series[0].Points.DataBindXY(values, arrays);
                
            }

            this.labelTJFX_PIE_4.Text = "穿刺部位皮肤情况统计";
        }

        //根据时间查询统计
        private void btnCJCX_time_Click(object sender, EventArgs e)
        {
            string kssj = this.dtpTJCX_KSSJ.Value.ToString("yyyy-MM")+"-01";
            string jssj = this.dtpTJCX_JSSJ.Value.ToString("yyyy-MM")+"-31";

            //基本信息查询
            if (TJ_FLAG == 0)
            {
                //MessageBox.Show("基本信息查询");
                tjSex(kssj, jssj);
                tjAge(kssj, jssj);
                tjZWDL(kssj, jssj);
                tjCCBWPFTJ(kssj, jssj);
            }
            else
            {
                //TJ_FLAG = 1;
                ccfstj(kssj, jssj);
                sslxtj(kssj, jssj);
                ssfstj(kssj, jssj);
                ctxyytj(kssj, jssj);
            }
            
        }
        
        //手术统计
        private void btnTJFX_SSTJ_Click(object sender, EventArgs e)
        {
            this.btnTJFX_SSTJ.ForeColor = ColorTranslator.FromHtml("#3399ff");
            this.btnTJFX_SSTJ.BackColor = Color.White;

            this.btnTJFX_JBXXTJ.ForeColor = Color.White;
            this.btnTJFX_JBXXTJ.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.btnTJFX_GZLTJ.ForeColor = Color.White;
            this.btnTJFX_GZLTJ.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelTJFX_PIE.Visible = true;
            this.panelTJFX_GZLTJ.Visible = false;

            initTjss();
        }

        //工作量统计
        private void btnTJFX_GZLTJ_Click(object sender, EventArgs e)
        {
            this.btnTJFX_GZLTJ.ForeColor = ColorTranslator.FromHtml("#3399ff");
            this.btnTJFX_GZLTJ.BackColor = Color.White;

            this.btnTJFX_JBXXTJ.ForeColor = Color.White;
            this.btnTJFX_JBXXTJ.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.btnTJFX_SSTJ.ForeColor = Color.White;
            this.btnTJFX_SSTJ.BackColor = ColorTranslator.FromHtml("#3399ff");

            this.panelTJFX_PIE.Visible = false;
            this.panelTJFX_GZLTJ.Visible = true;

            //工作量统计
           // initGJLTJ();
        }

        //手术统计
        private void initTjss()
        {
            TJ_FLAG = 1;
            ccfstj(null, null);
            sslxtj(null, null);
            ssfstj(null, null);
            ctxyytj(null, null);
        }

        //穿刺方式统计
        private void ccfstj(string kssj, string jssj)
        {
            this.chartPie_1.Series[0].Points.Clear();
            List<int> data = new List<int>();
            if (string.IsNullOrEmpty(kssj) && string.IsNullOrEmpty(jssj))
            {
                string sql =
                    "select t.t_ccfs,count(*) as total " +
                    "from t_track t,t_record r  " +
                    "where t.t_record_id = r.id " +
                    "group by t.t_ccfs";
                dataTable = DBManager.getInstance().find(sql);
            }
            else
            {
                string sql = string.Format(
                     "select t.t_ccfs,count(*) as total " +
                    "from t_track t,t_record r  " +
                    "where t.t_record_id = r.id " +
                    "and r.r_date > '{0}' and r.r_date < '{1}' " +
                    "group by t.t_ccfs", kssj, jssj);
                dataTable = DBManager.getInstance().find(sql);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<int> arrays = new List<int>();
                List<string> values = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int y = Convert.ToInt32(row["total"].ToString());
                    string x = row["t_ccfs"].ToString();
                    //string legel = row["t_nwzwdlqk"].ToString() + "[" + y + "](" + row["percent"].ToString() + "%)";
                    arrays.Add(y);
                    values.Add(x);
                }

                this.chartPie_1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                this.chartPie_1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                this.chartPie_1.Series[0].Points.DataBindXY(values, arrays);

            }

            this.labelTJFX_PIE_1.Text = "穿刺方式统计";
        }

        //手术类型统计
        private void sslxtj(string kssj, string jssj)
        {
            this.chartPie_2.Series[0].Points.Clear();
            List<int> data = new List<int>();
            if (string.IsNullOrEmpty(kssj) && string.IsNullOrEmpty(jssj))
            {
                string sql =
                    "select r.r_ss_type,count(*) as total " +
                    "from t_track t,t_record r  " +
                    "where t.t_record_id = r.id " +
                    "group by r.r_ss_type";
                dataTable = DBManager.getInstance().find(sql);
            }
            else
            {
                string sql = string.Format(
                     "select r.r_ss_type,count(*) as total " +
                    "from t_track t,t_record r  " +
                    "where t.t_record_id = r.id " +
                    "and r.r_date > '{0}' and r.r_date < '{1}' " +
                    "group by r.r_ss_type", kssj, jssj);
                dataTable = DBManager.getInstance().find(sql);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<int> arrays = new List<int>();
                List<string> values = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int y = Convert.ToInt32(row["total"].ToString());
                    string x = row["r_ss_type"].ToString();
                    //string legel = row["t_nwzwdlqk"].ToString() + "[" + y + "](" + row["percent"].ToString() + "%)";
                    arrays.Add(y);
                    values.Add(x);
                }

                this.chartPie_2.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                this.chartPie_2.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                this.chartPie_2.Series[0].Points.DataBindXY(values, arrays);

            }

            this.labelTJFX_PIE_2.Text = "手术类型统计";
        }

        //手术方式统计
        private void ssfstj(string kssj, string jssj)
        {
            this.chartPie_3.Series[0].Points.Clear();
            List<int> data = new List<int>();
            if (string.IsNullOrEmpty(kssj) && string.IsNullOrEmpty(jssj))
            {
                string sql =
                    "select r.r_ss_method,count(*) as total " +
                    "from t_track t,t_record r  " +
                    "where t.t_record_id = r.id " +
                    "group by r.r_ss_method";
                dataTable = DBManager.getInstance().find(sql);
            }
            else
            {
                string sql = string.Format(
                     "select r.r_ss_method,count(*) as total " +
                    "from t_track t,t_record r  " +
                    "where t.t_record_id = r.id " +
                    "and r.r_date > '{0}' and r.r_date < '{1}' " +
                    "group by r.r_ss_method", kssj, jssj);
                dataTable = DBManager.getInstance().find(sql);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<int> arrays = new List<int>();
                List<string> values = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int y = Convert.ToInt32(row["total"].ToString());
                    string x = row["r_ss_method"].ToString();
                    //string legel = row["t_nwzwdlqk"].ToString() + "[" + y + "](" + row["percent"].ToString() + "%)";
                    arrays.Add(y);
                    values.Add(x);
                }

                this.chartPie_3.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                this.chartPie_3.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                this.chartPie_3.Series[0].Points.DataBindXY(values, arrays);

            }

            this.labelTJFX_PIE_3.Text = "手术方式统计";
        }

        //常透析医院统计
        private void ctxyytj(string kssj, string jssj)
        {
            this.chartPie_4.Series[0].Points.Clear();
            List<int> data = new List<int>();
            if (string.IsNullOrEmpty(kssj) && string.IsNullOrEmpty(jssj))
            {
                string sql =
                    "select p.p_dialyse_hospital,count(*) as total " +
                     "from t_patient p,t_record r  " +
                    "where p.p_ID = r.r_patient_ID " +
                    "group by  p.p_dialyse_hospital";
                dataTable = DBManager.getInstance().find(sql);
            }
            else
            {
                string sql = string.Format(
                     "select p.p_dialyse_hospital,count(*) as total " +
                    "from t_patient p,t_record r  " +
                    "where p.p_ID = r.r_patient_ID " +
                    "and r.r_date > '{0}' and r.r_date < '{1}' " +
                    "group by  p.p_dialyse_hospital", kssj, jssj);
                dataTable = DBManager.getInstance().find(sql);
            }

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                List<int> arrays = new List<int>();
                List<string> values = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    int y = Convert.ToInt32(row["total"].ToString());
                    string x = row["p_dialyse_hospital"].ToString();
                    //string legel = row["t_nwzwdlqk"].ToString() + "[" + y + "](" + row["percent"].ToString() + "%)";
                    arrays.Add(y);
                    values.Add(x);
                }

                this.chartPie_4.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                this.chartPie_4.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                this.chartPie_4.Series[0].Points.DataBindXY(values, arrays);

            }

            this.labelTJFX_PIE_4.Text = "常透析医院统计";
        }

        //工作量统计
        private void btnTJFX_GZL_Click(object sender, EventArgs e)
        {
            initTJFX_XXTJ();
        }

        //详细统计
        private void initTJFX_XXTJ()
        {
            string kssj = this.dtpTJFX_GZLGL_KSSJ.Value.ToString("yyyy-MM") + "-01";
            string jssj = this.dtpTJFX_GZLGL_JSSJ.Value.ToString("yyyy-MM") + "-31";

            DataTable dt1 = null, dt2 = null, dt3 = null;
            //详细统计
            if (this.tbTJCX_XXTJ.Checked)
            {
             
                string sql = string.Format("select " +
                           " r.r_zd_docotor as '主刀医生'," +
                           " r.r_zs as '助手'," +
                           " r.r_qxhs as '器械护士'," +
                           " p.p_name as '患者姓名'," +
                          "  p.p_sex as '患者性别'," +
                           " r.r_ss_type as '手术类型'," +
                          "  r.r_ss_method as '手术方式'," +
                          "  r.r_cc_method as '穿刺方式'" +
                          "  from " +
                          "  t_record r left join t_patient p on r.r_patient_ID = p.p_ID" +
                          "  where r.r_date > '{0}' and  r.r_date < '{1}'", kssj, jssj);

                dt1 = DBManager.getInstance().find(sql);
            }

            //统计模式
            if (this.tbTJCX_TJMS.Checked)
            {
                dt1 = this.findZDYS_TJ(kssj, jssj);
                dt2 = this.findZS_TJ(kssj, jssj);
                dt3 = this.findQXHS_TJ(kssj, jssj);
                dt1.Merge(dt2, false, MissingSchemaAction.AddWithKey);
                dt1.Merge(dt3, false, MissingSchemaAction.AddWithKey);
            }
           

            this.dgvTJCX_GZL.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//选中整行
            this.dgvTJCX_GZL.DataSource = null;//清空
            this.dgvTJCX_GZL.DataSource = dt1;

            // Header以外所有的单元格的背景色
            this.dgvTJCX_GZL.RowsDefaultCellStyle.BackColor = Color.White;

            ////列Header的背景色 CellBorderStyle->Single   EnableHeaderVisualStyles->false
            this.dgvTJCX_GZL.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#4f81DD");
            this.dgvTJCX_GZL.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            this.dgvTJCX_GZL.DefaultCellStyle.SelectionBackColor = Color.Gray;//设置选中的颜色
        }

        //统计模式-》查询主刀医生
        private DataTable findZDYS_TJ(string kssj, string jssj)
        {
            string sql = string.Format("select " +
                       " r.r_zd_docotor as '医护人员姓名'," +
                       " '主刀医生' as '工作性质'," +
                       " count(*) as '手术台次' " +
                      "  from " +
                      "  t_record r left join t_patient p on r.r_patient_ID = p.p_ID" +
                      "  where r.r_date > '{0}' and  r.r_date < '{1}'" +
                      " group by r.r_zd_docotor", kssj, jssj);

            dataTable = DBManager.getInstance().find(sql);
            return dataTable;
        }

        //统计模式-》查询助手
        private DataTable findZS_TJ(string kssj, string jssj)
        {
            string sql = string.Format("select " +
                       " r.r_zs as '医护人员姓名'," +
                       " '助手' as '工作性质'," +
                       " count(*) as '手术台次' " +
                      "  from " +
                      "  t_record r left join t_patient p on r.r_patient_ID = p.p_ID" +
                      "  where r.r_date > '{0}' and  r.r_date < '{1}'" +
                      " group by r.r_zs", kssj, jssj);


            dataTable = DBManager.getInstance().find(sql);
            return dataTable;
        }

        //统计模式-》器械护士
        private DataTable findQXHS_TJ(string kssj, string jssj)
        {
            string sql = string.Format("select " +
                       " r.r_qxhs as '医护人员姓名'," +
                       " '器械护士' as '工作性质'," +
                        " count(*) as '手术台次' " +
                      "  from " +
                      "  t_record r left join t_patient p on r.r_patient_ID = p.p_ID" +
                      "  where r.r_date > '{0}' and  r.r_date < '{1}'"+
                      " group by r.r_qxhs", kssj, jssj);

            dataTable = DBManager.getInstance().find(sql);
            return dataTable;
        }

        //打印手术记录单
        private void btnPrintSSCX_SSJL_Click(object sender, EventArgs e)
        {
            this.printDialogSSCX_SSJL.Document = this.printDucumentSSCX_SSJL;
            if (this.printDialogSSCX_SSJL.ShowDialog() == DialogResult.OK)
            {
                this.printDucumentSSCX_SSJL.Print();
            }
        }

        private void printDucumentSSCX_SSJL_PrintPage(object sender, PrintPageEventArgs e)
        {
            //打印内容 为 局部的 this.panel1
            Bitmap _NewBitmap = new Bitmap(this.panelSSCX_SSJLD.Width, this.panelSSCX_SSJLD.Height);
            this.panelSSCX_SSJLD.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
            e.Graphics.DrawImage(_NewBitmap, 0, 0, _NewBitmap.Width, _NewBitmap.Height);
        }

        //打印手术追踪查询
        private void printDocumentSJCX_SSZZ_PrintPage(object sender, PrintPageEventArgs e)
        {
            //打印内容 为 局部的 this.panel1
            Bitmap _NewBitmap = new Bitmap(this.panelSJCX_SSZZ.Width, this.panelSJCX_SSZZ.Height);
            this.panelSJCX_SSZZ.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
            e.Graphics.DrawImage(_NewBitmap, 0, 0, _NewBitmap.Width, _NewBitmap.Height);
        }

        //手术追踪打印
        private void btnSSCX_SSZZDY_Click(object sender, EventArgs e)
        {
            this.printDialogSSCX_SSJL.Document = this.printDucumentSSCX_SSJL;
            if (this.printDialogSSCX_SSJL.ShowDialog() == DialogResult.OK)
            {
                this.printDocumentSJCX_SSZZ.Print();
            }
        }

    
        //设置字体加粗
        private void menuSSLR_CT_Click(object sender, EventArgs e)
        {
            string fontType = this.menuSSLR_ZZLX.Text;
            int fontSize = Convert.ToInt32(this.menuSSLR_ZZDX.Text);
            if (string.IsNullOrEmpty(fontType))
            {
                fontType = "宋体";
            }

            this.rtbSSLR_SSJL.SelectionFont = new Font(fontType, fontSize, FontStyle.Bold);
        }

        //设置字体 斜体
        private void menuSSLR_XT_Click(object sender, EventArgs e)
        {
            string fontType = this.menuSSLR_ZZLX.Text;
            int fontSize = Convert.ToInt32(this.menuSSLR_ZZDX.Text);
            if (string.IsNullOrEmpty(fontType))
            {
                fontType = "宋体";
            }

            this.rtbSSLR_SSJL.SelectionFont = new Font(fontType, fontSize, FontStyle.Italic);
        }

        //设置字体下划线
        private void menuSSLR_XHX_Click(object sender, EventArgs e)
        {
            string fontType = this.menuSSLR_ZZLX.Text;
            int fontSize = Convert.ToInt32(this.menuSSLR_ZZDX.Text);
            if (string.IsNullOrEmpty(fontType))
            {
                fontType = "宋体";
            }

            this.rtbSSLR_SSJL.SelectionFont = new Font(fontType, fontSize, FontStyle.Underline);
        }

        //文本左对齐
        private void menuSSLR_ZDQ_Click(object sender, EventArgs e)
        {
            this.rtbSSLR_SSJL.SelectionAlignment = HorizontalAlignment.Left;
        }

        //文本右对齐
        private void menuSSLR_YDQ_Click(object sender, EventArgs e)
        {
            this.rtbSSLR_SSJL.SelectionAlignment = HorizontalAlignment.Right;
        }

        //文本剧中
        private void menuSSLR_JZ_Click(object sender, EventArgs e)
        {
            this.rtbSSLR_SSJL.SelectionAlignment = HorizontalAlignment.Center;
        }

        //设置字体类型 粗体 宋体
        private void menuSSLR_setFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fontType = this.menuSSLR_ZZLX.Text;
            int fontSize = Convert.ToInt32(this.menuSSLR_ZZDX.Text);
            if (string.IsNullOrEmpty(fontType))
            {
                fontType = "宋体";
            }

            this.rtbSSLR_SSJL.SelectionFont = new Font(fontType, fontSize, FontStyle.Bold);
        }

        //监听窗体的变化
        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.Hide();
                //this.notifyIcon1.Visible = true;
                this.notifyIcon1.Text = "析之助手术登记管理系统";//鼠标放在托盘时显示的文字
                this.notifyIcon1.BalloonTipText = "析之助手术登记管理系统";//气泡显示的文字
                this.notifyIcon1.ShowBalloonTip(1000);//托盘气泡的显现时间
                
            }
        }

        //双击托盘，显示应用
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        //让datagridview自动 编号
        private void dgvTJCX_GZL_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            autoAddId(sender,e);
        }

        //工作量统计让datagridview自动 编号
        public void autoAddId(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dt = (DataGridView)sender;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvTJCX_GZL.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                  (e.RowIndex + 1).ToString(),
                   dt.RowHeadersDefaultCellStyle.Font,
                   rectangle,
                   dt.RowHeadersDefaultCellStyle.ForeColor,
                   TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //选择省 -》改变市与县
        private void cbSSLR_PROVINCE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //查询市
            dataTable = DBManager.getInstance().find(string.Format("SELECT  id,c_name as d_name FROM xzj.t_city where c_province_id = {0}", this.cbSSLR_PROVINCE.SelectedValue));
            combobox(this.cbSSLR_CITY, dataTable);//

            //查询县
            dataTable = DBManager.getInstance().find(string.Format("SELECT  id,a_name as d_name FROM xzj.t_area where a_city_id = {0}", this.cbSSLR_CITY.SelectedValue));
            combobox(this.cbSSLR_COUNTY, dataTable);//

        }

        //查询县
        private void cbSSLR_CITY_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //查询县
            dataTable = DBManager.getInstance().find(string.Format("SELECT  id,a_name as d_name FROM xzj.t_area where a_city_id = {0}", this.cbSSLR_CITY.SelectedValue));
            combobox(this.cbSSLR_COUNTY, dataTable);//
        }

        //托盘item事件
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
            {

                if (contextMenuStrip1.Items[i].Selected)
                {

                    if (contextMenuStrip1.Items[i].Text.Trim() == "退出登录")
                    {
                        this.Hide();
                        FormSignIn f = new FormSignIn();
                        f.Show();
                        
                    }

                    else if (contextMenuStrip1.Items[i].Text.Trim() == "退出")
                    {
                        Application.Exit(); 
                    }

                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.labelNowDate.Text = UtilTools.getDateAndWeek();
        }

        //切换到手术追踪
        private void btnSSZZ_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSZZ.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSXGXY.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = false;
            this.panelSSZZ.Visible = true;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelSSXGXY.Visible = false;
            this.panelKSGL.Visible = false;
        }

        //切换到手术相关协议
        private void btnSSXGXY_Click(object sender, EventArgs e)
        {
            this.btnSSLR.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSZZ.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnZDGL.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSJCX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnCJFX.BackColor = ColorTranslator.FromHtml("#0000cd");
            this.btnSSXGXY.BackColor = ColorTranslator.FromHtml("#3399ff");
            this.btnKSGL.BackColor = ColorTranslator.FromHtml("#0000cd");

            this.panelSSLR.Visible = false;
            this.panelSSZZ.Visible = false;
            this.panelZDGL.Visible = false;
            this.panelSJCX.Visible = false;
            this.panelCJFX.Visible = false;
            this.panelSSXGXY.Visible = true;
            this.panelKSGL.Visible = false;
        }

        //去添加手术追踪界面
        private void btnGoAddSSZZ_Click(object sender, EventArgs e)
        {

        }

        //手术记录添加图片
        private void btnSSJLa_addPic_Click(object sender, EventArgs e)
        {
            //打开的文件选择对话框上的标题
            this.openFileDialogSSLR.Title = "请选择图片";
            //设置文件类型
            this.openFileDialogSSLR.Filter = "PNG图片|*.png|JPG图片|*.jpg|BMP图片|*.bmp|Gif图片|*.gif";
            //设置默认文件类型显示顺序
            this.openFileDialogSSLR.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录
            this.openFileDialogSSLR.RestoreDirectory = true;
            //设置是否允许多选
            this.openFileDialogSSLR.Multiselect = true;
            //按下确定选择的按钮
            if (this.openFileDialogSSLR.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in this.openFileDialogSSLR.FileNames)
                {
                    
                } 
            }
        }

        private void panel122_MouseHover(object sender, EventArgs e)
        {
            this.listViewDictionarySSZD.Visible = false;
            this.listViewDictionaryQKZD.Visible = false;
        }

        //去增加协议界面
        private void btnSSXGXY_add_control_Click(object sender, EventArgs e)
        {
            FormAddControl formAddControl = new FormAddControl();
            //formAddControl.setDesc(-1, dictionary_parent_id, "添加字典", this.labelDictionaryShow.Text);
            formAddControl.ShowDialog();
            if (formAddControl.DialogResult == DialogResult.OK)
            {
                //this.setListViewDictionary(this.labelDictionaryShow.Text, dictionary_parent_id);//重新绑定
            }
        }

        

    }

       
}