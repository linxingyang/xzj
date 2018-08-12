using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace xzj
{
    class DBRoom
    {
        private static DBRoom instance = null;
        private static MySqlCommand cmd;
        private static MySqlConnection conn = null;
        //private static string sqlAddress = "";

        private DBRoom()
        {
            conn = new MySqlConnection(UtilConfig.SQL_ADDRESS);
            conn.Open();
        }

        public static DBRoom getInstance()
        {
            if (null == instance)
            {
                instance = new DBRoom();
            }
            
            return instance;
            
        }

        /*r_hospital_name varchar(50),##医院名称
	r_address varchar(100),##地址
	r_province varchar(20),##省/直辖市
	r_rank varchar(20),##等级
	r_postcodes char(6),##邮编
	r_room_name varchar(50),##科室名称
	r_room_tel varchar(11),##科室电话
	r_room_fax varchar(50),##科室传真
	r_responsible varchar(50),##负责人
	r_responsible_title varchar(20),##负责人职称
	r_responsible_tel varchar(11),##负责人电话
	r_responsible_phone varchar(11),##负责人手机
	r_responsible_email varchar(20),##负责人邮箱
	r_dialyse_center_area varchar(10),##透析中心面积
	r_dialyse_unit_area varchar(10),##透析单元面积
	r_start_date varchar(20),##启用日期*/
        // 添加科室
        public int addRoom(string r_hospital_name, string r_address, string r_province, string r_rank, string r_postcodes, string r_room_name, string r_room_tel,
            string r_room_fax,string r_responsible, string r_responsible_title, string r_responsible_tel, string r_responsible_phone, string r_responsible_email,
            string r_dialyse_center_area,string r_dialyse_unit_area,string r_start_date)
        {
            int flag = 0;
            string sql = string.Format("insert into t_room(r_hospital_name,r_address,r_province, sr_rank, r_postcodes,  r_room_name, r_room_tel,"+
                "r_room_fax,r_responsible,r_responsible_title, r_responsible_tel, r_responsible_phone,  r_responsible_email,"+
                " r_dialyse_center_area, r_dialyse_unit_area, r_start_date) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',"+
                "'{12}','{13}','{14}')",r_hospital_name,r_address,r_province, r_rank, r_postcodes,  r_room_name, r_room_tel,r_room_fax,r_responsible,r_responsible_title,
                r_responsible_tel, r_responsible_phone, r_responsible_email, r_dialyse_center_area, r_dialyse_unit_area, r_start_date);

            try
            {
                MySqlCommand comm = new MySqlCommand(sql, conn);
                flag = comm.ExecuteNonQuery();
                return flag;

            }
            catch (Exception err)
            {
                Console.Write(err);
            }
            //conn.Close();
            return flag;
        }

        // 编辑科室
        public int editRoom(string r_hospital_name, string r_address, string r_province, string r_rank, string r_postcodes, string r_room_name, string r_room_tel,
            string r_room_fax, string r_responsible, string r_responsible_title, string r_responsible_tel, string r_responsible_phone, string r_responsible_email,
            string r_dialyse_center_area, string r_dialyse_unit_area, string r_start_date)
        {
            int flag = 0;
            string sql = string.Format("update t_room set r_hospital_name='{0}',r_address,r_province='{1}', sr_rank='{2}', r_postcodes='{3}',  r_room_name='{4}', r_room_tel='{5}'," +
                "r_room_fax='{6}',r_responsible='{7}',r_responsible_title='{8}', r_responsible_tel='{9}', r_responsible_phone='{10}',  r_responsible_email='{11}'," +
                " r_dialyse_center_area='{12}', r_dialyse_unit_area='{13}', r_start_date='{14}'",
                r_hospital_name, r_address, r_province, r_rank, r_postcodes, r_room_name, r_room_tel, r_room_fax, r_responsible, r_responsible_title,
                r_responsible_tel, r_responsible_phone, r_responsible_email, r_dialyse_center_area, r_dialyse_unit_area, r_start_date);

            try
            {
                MySqlCommand comm = new MySqlCommand(sql, conn);
                flag = comm.ExecuteNonQuery();
                return flag;

            }
            catch (Exception err)
            {
                Console.Write(err);
            }
            //conn.Close();
            return flag;
        }
    }
}
