using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace xzj
{
    public partial class FormConfigDB : Form
    {
       
        public FormConfigDB()
        {
            InitializeComponent();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfigDBForm_Load(object sender, EventArgs e)
        {
            string dbName = UtilTools.getData(UtilConfig.DB_NAME_KEY);
            string ipAdd = UtilTools.getData(UtilConfig.DB_IP_KEY);
            string user = UtilTools.getData(UtilConfig.DB_USER_KEY);
            string pwd = UtilTools.getData(UtilConfig.DB_PWD_KEY);

            if (dbName != null)
            {
                this.tbDbName.Text = dbName;
            }

            if (dbName != null)
            {
                this.tbIP.Text = ipAdd;
            }

            if (dbName != null)
            {
                this.tbUserName.Text = user;
            }

            if (dbName != null)
            {
                this.tbPwd.Text = pwd;
            }

        }

        //保存配置
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tbDbName.Text != null)
            {
                UtilTools.setData(UtilConfig.DB_NAME_KEY, this.tbDbName.Text);
            }

            if (this.tbIP.Text != null)
            {
                UtilTools.setData(UtilConfig.DB_IP_KEY, this.tbIP.Text);
            }

            if (this.tbUserName.Text != null)
            {
                UtilTools.setData(UtilConfig.DB_USER_KEY, this.tbUserName.Text);
            }

            if (this.tbPwd.Text != null)
            {
                UtilTools.setData(UtilConfig.DB_PWD_KEY, this.tbPwd.Text);
            }

            string dbName = UtilTools.getData(UtilConfig.DB_NAME_KEY);
            string ipAdd = UtilTools.getData(UtilConfig.DB_IP_KEY);
            string user = UtilTools.getData(UtilConfig.DB_USER_KEY);
            string pwd = UtilTools.getData(UtilConfig.DB_PWD_KEY);

            if (dbName == null || ipAdd == null || user == null || pwd == null)
            {
                MessageBox.Show("保存失败");
            }
            else
            {
                string sqlAddress = "server=" + ipAdd + ";port=3306;database=" + dbName + ";user=" + user + ";password=" + pwd + ";SslMode = none;";
                UtilTools.setData(UtilConfig.SQL_ADDRESS_KEY, sqlAddress);
                MessageBox.Show("保存成功");
            }

        }

        //测试连接
        private void btnTest_Click(object sender, EventArgs e)
        {
            string dbName = this.tbDbName.Text;
            string ipAdd = this.tbIP.Text;
            string user = this.tbUserName.Text;
            string pwd = this.tbPwd.Text;
            string sqlAddress = "server=" + ipAdd + ";port=3306;database=" + dbName + ";user=" + user + ";password=" + pwd + ";SslMode = none;";
            try
            {
                 MySqlConnection conn = new MySqlConnection(sqlAddress);
                 conn.Open();
                 MessageBox.Show("连接成功");
            }
            catch (Exception err)
            {
                MessageBox.Show("连接失败，失败信息：" + err); ;
            }
           
        }
       
    }
}
