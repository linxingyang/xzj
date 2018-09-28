using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xzj.utils;

namespace xzj
{
    public partial class FormAddControl : Form
    {
        public FormAddControl()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string orderId = this.tbRank.Text;
            string name = this.tbName.Text;
            string desc = this.tbDesc.Text;

            if (string.IsNullOrEmpty(orderId))
            {
                MessageBox.Show("请输入排列顺序!");
                return;
            }
            else {
                try
                {
                    Convert.ToInt32(orderId);
                }
                catch (Exception)
                {
                    MessageBox.Show("排列顺序请输入数字!");
                    return;
                }
            }
            if (string.IsNullOrEmpty(name)) {
                MessageBox.Show("请输入协议名称!");
                return;
            }
            if (string.IsNullOrEmpty(desc))
            {
                MessageBox.Show("请输入协议描述!");
                return;
            }


            MySqlParameter[] ps = new MySqlParameter[] {
                new MySqlParameter("@c_order_id",orderId),
                new MySqlParameter("@c_name", name),
                new MySqlParameter("@c_desc", desc)
            };
            SqlHelper4MySql.ExecuteNonQuery(SqlCommandHelpler.T_CONTROL_INSERT, ps);
            MessageBox.Show("保存成功!");

            this.tbRank.Text = "";
            this.tbName.Text = "";
            this.tbDesc.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
