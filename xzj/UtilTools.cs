using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace xzj
{
    class UtilTools
    {
        private static string[] weeks = new string[]{"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};
        private static string[] weeks_ch = new string[] { "一", "二", "三", "四", "五", "六", "天" };

        //根据身份证获取年龄
        public static int getAgeByID(string ID)
        {
            int nowY = DateTime.Now.Year;
            int birthY = Convert.ToInt32(ID.Substring(6, 4));

            return nowY - birthY;
        }

        //获取当前周几
        public static String getWeek()
        {
            //DayOfWeek dayOfWeek = new DayOfWeek();
            return DateTime.Now.DayOfWeek.ToString();
        }

        //获取当前时间
        public static String getTime()
        {
            return DateTime.Now.ToString("hh:mm");
        }

        //获取当前日期
        public static String getDate()
        {
            return DateTime.Now.ToString("yyyy:MM:dd");
        }

        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        public static bool IsNumber(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }

        //获取当前时间
        public static String getDayAndTime()
        {
            return DateTime.Now.ToString("yyyy:MM:dd hh:mm:ss");
        }

        //获取当前时间
        public static String getDayAndTimeMM()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm");
        }

        //获取当前日期及周几
        public static String getDateAndWeek()
        {
            string result = "当前时间：";
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            int d = DateTime.Now.Day;
            

            result += y + "年"+m+"月"+d+"日";

            for (int i = 0; i < weeks.Length; i++)
            {
                if (DateTime.Now.DayOfWeek.ToString().Equals(weeks[i]))
                {
                    result += " 周" + weeks_ch[i];
                }
            }

            return result;
        }
        

       //邮箱格式判断
        public static bool isEmail(String email)
        {
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            return r.IsMatch(email);
        }

        //获取城市名称
        //public static 
    }
}
