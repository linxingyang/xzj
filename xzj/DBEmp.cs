using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace xzj
{
    class DBEmp
    {
        private static MySqlCommand cmd;
        private static MySqlConnection conn;
        private static string sqlAddress = "";

        public DBEmp()
        {
            sqlAddress = UtilTools.getData(UtilConfig.SQL_ADDRESS_KEY);
        }

        // 登录
        public static bool isLogin(string account, string pwd)
        {
            bool result = false;
            string sql = string.Format("select id from t_emp where e_account='{0}' and e_pwd='{1}'", account,  pwd);
            conn = new MySqlConnection(sqlAddress);
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(sql, conn);
                int flag = (int)comm.ExecuteScalar();
                if (flag > 0)
                {
                    result = true;
                }

            }
            catch (Exception err)
            {
                Console.Write(err);
            }
            conn.Close();
            return result;
        }

        // 用户是否存在
        public static bool isAccount(string account)
        {
            bool result = false;
            string sql = string.Format("select id from t_emp where e_account='{0}'", account);
            conn = new MySqlConnection(sqlAddress);
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(sql, conn);
                int flag = (int)comm.ExecuteScalar();
                if (flag > 0)
                {
                    result = true;
                }

            }
            catch (Exception err)
            {
                Console.Write(err);
            }
            conn.Close();
            return result;
        } 

        /**
     id int not null auto_increment,##唯一标识 自增 
 	e_account varchar(50),##用户名
	e_name varchar(50),##姓名
 	e_sex char(2),##性别
 	e_birth varchar(20),##出生日期
 	e_tel varchar(11),##手机号
 	e_email varchar(20),##邮箱
 	e_address varchar(100),##地址
 	e_pwd varchar(20),##密码
    * */
        // 添加用户
        public static bool addEmp(string account, string name, string sex, string birth, string tel, string email, string address, string pwd)
        {
            bool result = false;
            string sql = string.Format("insert into t_emp(e_account,e_name,e_sex,e_birth,e_tel,e_email,e_address,e_pwd)"
                + "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", account, name, sex, birth, tel, email, address, pwd);
            conn = new MySqlConnection(sqlAddress);
            conn.Open();
            try
            {
                MySqlCommand comm = new MySqlCommand(sql, conn);
                int flag = comm.ExecuteNonQuery();
                Console.Write("flag:{0}", flag);
                if (flag > 0)
                {
                    result = true;
                }
                
            }
            catch (Exception err)
            {
                Console.Write(err);
            }
            conn.Close();
            return result;
        } 
    
    }
}
