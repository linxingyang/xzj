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
        private static int dictionary_parent_id = 1;

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

            //显示当前时间
            this.labelNowDate.Text = UtilTools.getDateAndWeek();
            //显示当前用户
            this.labelAccountShow.BackColor = ColorTranslator.FromHtml("#0078d7");
            this.labelAccountShow.Text = "用户【"+UtilConfig.ACCOUNT+"】";
            this.labelAccountShow.ForeColor = ColorTranslator.FromHtml("#fff");

            //初始化手术录入
            initSSLR();
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


            initSSLR();
        }

        //初始化手术录入
        private void initSSLR()
        {
            //查询医保类型字典
            DataTable dt = DBDictionary.getInstance().getDictionarysByParentId(1);

            this.cbSSLR_YBLX.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                this.cbSSLR_YBLX.Text = row["d_name"].ToString();
                this.cbSSLR_YBLX.Items.Add(row["d_name"].ToString());
            }

            //查询手术地点字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(5);
            this.cbSSLR_SSDD.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                this.cbSSLR_SSDD.Text = row["d_name"].ToString();
                this.cbSSLR_SSDD.Items.Add(row["d_name"].ToString());
            }

            //查询手术类型字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(4);
            this.cbSSLR_SSLX.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                this.cbSSLR_SSLX.Text = row["d_name"].ToString();
                this.cbSSLR_SSLX.Items.Add(row["d_name"].ToString());
            }

            //查询手术方式字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(6);
            this.cbSSLR_SSFS.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                this.cbSSLR_SSFS.Text = row["d_name"].ToString();
                this.cbSSLR_SSFS.Items.Add(row["d_name"].ToString());
            }

            //查询穿刺方式字典
            dt = DBDictionary.getInstance().getDictionarysByParentId(7);
            this.cbSSLR_CCFS.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                this.cbSSLR_CCFS.Text = row["d_name"].ToString();
                this.cbSSLR_CCFS.Items.Add(row["d_name"].ToString());
            }

            //手术日期
            this.tbSSLR_SSRQ.Text = UtilTools.getDayAndTime();
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

            this.listViewDictionary.FullRowSelect = true;

            this.setListViewDictionary("医保类型字典", 1);
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
            this.setListViewDictionary("医保类型字典", 1);
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
            this.listViewDictionaryQKZD.Visible = ! this.listViewDictionaryQKZD.Visible;
            

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
                //MessageBox.Show(this.listViewDictionarySSZD.SelectedItems[0].SubItems[1].Text);
                this.setListViewDictionary(dName, id);
            }
            this.listViewDictionaryQKZD.Visible = false;
        }

        //设置显示字典
        private void setListViewDictionary(string dName, int parentId)
        {
            dictionary_parent_id = parentId;
            this.labelDictionaryShow.Text = dName;//显示选中的字典名称

            this.listViewDictionary.Clear();
            //this.listViewDictionary.Items.Clear();
            this.listViewDictionary.FullRowSelect = true;//选中指定
            //this.listViewDictionary.BackColor = ColorTranslator.FromHtml("#41aaeb");

            //设置标题
            //隐藏标题id
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "id";   //设置列标题
            ch.Width = 0;    //设置列宽度
            ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式
            this.listViewDictionary.Columns.Add(ch);
            //标题一
            ch = new ColumnHeader();
            ch.Text = "排列顺序";   //设置列标题
            ch.Width = 96 + 10;    //设置列宽度
            ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式
            this.listViewDictionary.Columns.Add(ch);
            //标题二
            ch = new ColumnHeader();
            ch.Text = dName;   //设置列标题
            ch.Width = 3*96;    //设置列宽度
            ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式
            this.listViewDictionary.Columns.Add(ch);    //将列头添加到ListView控件。
            //标题三
            ch = new ColumnHeader();
            ch.Text = "描述";   //设置列标题
            ch.Width = 6*96-10;    //设置列宽度
            ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式
            this.listViewDictionary.Columns.Add(ch);    //将列头添加到ListView控件。

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 25);
            this.listViewDictionary.SmallImageList = imgList;

            DataTable dt = DBDictionary.getInstance().getDictionarysByParentId(parentId);
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Clear();
                item.Text = row["id"].ToString();
                //item.ForeColor = Color.White;
                item.SubItems.Add(row["d_order"].ToString());
                item.SubItems.Add(row["d_name"].ToString());
                item.SubItems.Add(row["d_desc"].ToString());
                //记住view改成detail
                this.listViewDictionary.Items.Add(item);
               // this.listViewDictionary.Columns[0].Width = 2000;
                //this.listViewDictionarySSZD.Items.Add(row["d_name"].ToString());
            }
            this.listViewDictionary.EndUpdate();
        }
        
        //前往添加字典界面
        private void btnGoAddDictionaryy_Click(object sender, EventArgs e)
        {
            //FormAddDictionary.dictionary_id = dictionary_id;
            //FormAddDictionary.title = "添加字典";
            //FormAddDictionary.dictionary_parent_id = dictionary_parent_id;
            FormAddDictionary formAddDictionary = new FormAddDictionary();
            formAddDictionary.setDesc(-1, dictionary_parent_id, "添加字典", this.labelDictionaryShow.Text);
            formAddDictionary.ShowDialog();
            if (formAddDictionary.DialogResult == DialogResult.OK)
            {
                this.setListViewDictionary(this.labelDictionaryShow.Text,dictionary_parent_id);//重新绑定
            }
        }

        //修改字典
        private void btnGoEditDictionary_Click(object sender, EventArgs e)
        {
            if (this.listViewDictionary.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要修改的字典");
                return;
            }
           
            int id = Convert.ToInt32(this.listViewDictionary.SelectedItems[0].Text);


            FormAddDictionary formAddDictionary = new FormAddDictionary();
            formAddDictionary.setDesc(id, dictionary_parent_id, "修改字典", this.labelDictionaryShow.Text);
            formAddDictionary.ShowDialog();
            if (formAddDictionary.DialogResult == DialogResult.OK)
            {
                this.setListViewDictionary(this.labelDictionaryShow.Text, dictionary_parent_id);//重新绑定
            }
        }

        //字典列表选中设置
        private void listViewDictionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int i in this.listViewDictionary.SelectedIndices)
            {
                this.listViewDictionary.Items[i].Selected = true;
                //string dName = this.listViewDictionary.SelectedItems[0].Text;
            }
        }

        //删除字典
        private void btnDeleteDictionary_Click(object sender, EventArgs e)
        {
            if (this.listViewDictionary.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要删除的字典");
                return;
            }
            int id = Convert.ToInt32(this.listViewDictionary.SelectedItems[0].Text);
            string name = this.listViewDictionary.SelectedItems[0].SubItems[2].Text;
            DialogResult dr = MessageBox.Show("删除【" + name + "(" + id + ")】?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DBDictionary.getInstance().deleteDictionary(id);
                this.setListViewDictionary(this.labelDictionaryShow.Text, dictionary_parent_id);//重新绑定
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
            string province = this.tbSSLR_PROVINCE.Text;//省
            string city = this.tbSSLR_CITY.Text;//市
            string county = this.tbSSLR_COUNTY.Text;//县
            string ctxyy = this.tbSSLR_CTXYY.Text;//常透析医院
            string ctxyylxr = this.tbSSLR_CTXYYLXR.Text;//常透析医院联系人
            string ctxyylxrdh = this.tbSSLR_CTXYYLXRDH.Text;//常透析医院联系人电话
            string ssrq = this.tbSSLR_SSRQ.Text;//手术日期
            string ssdd = this.cbSSLR_SSDD.Text;//手术地点
            string sslx = this.cbSSLR_SSLX.Text;//手术类型
            string ssfs = this.cbSSLR_SSFS.Text;//手术方式
            string ccfs = this.cbSSLR_CCFS.Text;//穿刺方式
            string ssjl = this.rtbSSLR_SSJL.Text;//手术记录
            string zdys = this.tbSSLR_ZDYS.Text;//主刀医生
            string zs = this.tbSSLR_ZS.Text;//助手
            string qxfs = this.tbSSLR_QXFS.Text;//器械护士

            string address = province + "-" + city + "-" + county;

            //

            if (string.IsNullOrEmpty(name)) { MessageBox.Show("【姓名】不能为空"); return; }
            if (string.IsNullOrEmpty(sex)) { MessageBox.Show("【性别】不能为空"); return; }
            if (string.IsNullOrEmpty(tel)) { MessageBox.Show("【电话】不能为空"); return; }
            if (tel.Length != 11) { MessageBox.Show("【电话】格式错误"); return; }
            if (string.IsNullOrEmpty(ID)) { MessageBox.Show("【身份证号】不能为空"); return; }
            if (ID.Length != 18) { MessageBox.Show("【身份证号】格式错误"); return; }
            if (string.IsNullOrEmpty(yblx)) { MessageBox.Show("【医保类型】不能为空"); return; }
            if (string.IsNullOrEmpty(province)) { MessageBox.Show("【省】不能为空"); return; }
            if (string.IsNullOrEmpty(city)) { MessageBox.Show("【市】不能为空"); return; }
            if (string.IsNullOrEmpty(county)) { MessageBox.Show("【县】不能为空"); return; }
            if (string.IsNullOrEmpty(ctxyy)) { MessageBox.Show("【常透析医院】不能为空"); return; }
            if (string.IsNullOrEmpty(ctxyylxr)) { MessageBox.Show("【常透析医院联系人】不能为空"); return; }
            if (string.IsNullOrEmpty(ctxyylxrdh)) { MessageBox.Show("【常透析医院联系人电话】不能为空"); return; }
            if (ctxyylxrdh.Length != 11) { MessageBox.Show("【常透析医院联系人电话】格式错误"); return; }
            if (string.IsNullOrEmpty(ssdd)) { MessageBox.Show("【手术地点】不能为空"); return; }
            if (string.IsNullOrEmpty(sslx)) { MessageBox.Show("【手术类型】不能为空"); return; }
            if (string.IsNullOrEmpty(ssfs)) { MessageBox.Show("【手术方式】不能为空"); return; }
            if (string.IsNullOrEmpty(ccfs)) { MessageBox.Show("【穿刺方式】不能为空"); return; }
            if (string.IsNullOrEmpty(ssjl)) { MessageBox.Show("【手术记录】不能为空"); return; }
            if (string.IsNullOrEmpty(zdys)) { MessageBox.Show("【主刀医生】不能为空"); return; }
            if (string.IsNullOrEmpty(zs)) { MessageBox.Show("【助手】不能为空"); return; }
            if (string.IsNullOrEmpty(qxfs)) { MessageBox.Show("【器械护士】不能为空"); return; }

            //添加
            DBRecords.getInstance().addRecord(name, sex, age, tel, ID, yblx, address, ctxyy, ctxyylxr, ctxyylxrdh, ssrq, ssdd, sslx, ssfs, ccfs, zs, qxfs, ssjl, zdys);
        }

        //监听身份证输入
        private void tbSSLR_ID_TextChanged(object sender, EventArgs e)
        {
            if (this.tbSSLR_ID.Text.Length == 18)
            {
                //int flag = 0;
                for (int i = 0; i < 17; i++)
                {
                    
                    if (!UtilTools.IsNumber(this.tbSSLR_ID.Text.ToString().Substring(i, 1)))
                    {
                        MessageBox.Show("身份证输入有误");
                        return;
                    }
                   // flag++;
                }
                this.tbSSLR_AGE.Text = UtilTools.getAgeByID(this.tbSSLR_ID.Text)+"";
            }
        }

        //取消手术记录
        private void btnRecordClear_Click(object sender, EventArgs e)
        {
            this.tbSSLR_NAME.Text = "";//患者姓名
            this.cbSSLR_SEX.Text = "";//性别
            this.tbSSLR_AGE.Text = "";//年龄
            this.tbSSLR_TEL.Text = "";//手机号
            this.tbSSLR_ID.Text = "";//身份证号
            this.cbSSLR_YBLX.Text = "";//医保类型
            this.tbSSLR_PROVINCE.Text = "";//省
            this.tbSSLR_CITY.Text = "";//市
            this.tbSSLR_COUNTY.Text = "";//县
            this.tbSSLR_CTXYY.Text = "";//常透析医院
            this.tbSSLR_CTXYYLXR.Text = "";//常透析医院联系人
            this.tbSSLR_CTXYYLXRDH.Text = "";//常透析医院联系人电话
            this.tbSSLR_SSRQ.Text = UtilTools.getDayAndTime();;//手术日期
            this.cbSSLR_SSDD.Text = "";//手术地点
            this.cbSSLR_SSLX.Text = "";//手术类型
            this.cbSSLR_SSFS.Text = "";//手术方式
            this.cbSSLR_CCFS.Text = "";//穿刺方式
            this.rtbSSLR_SSJL.Text = "";//手术记录
            this.tbSSLR_ZDYS.Text = "";//主刀医生
            this.tbSSLR_ZS.Text = "";//助手
            this.tbSSLR_QXFS.Text = "";//器械护士
        }

       

        

       
    }
}
