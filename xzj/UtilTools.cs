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

        //根据身份证获取年龄5221261995
        public static int getAgeByID(string ID)
        {
            int nowY = DateTime.Now.Year;
            int birthY = Convert.ToInt32(ID.Substring(5, 4));

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
            return DateTime.Now.ToString("hh:mm:ss");
        }

        //获取当前日期
        public static String getDate()
        {
            return DateTime.Now.ToString("yyyy:MM:dd");
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

        //用来保存数据,返回值 0 插入异常 1 插入失败("保存的字符不能有 '|' 和 "&" 符号") 2 插入成功
        public static int setData(string key, string value)
        {
            int flag = 0;
            string fileName = "xzj";
            FileStream fs = null;

            if (key.Contains("|") || value.Contains("|") || key.Contains("&") || value.Contains("&"))
            {
                //flag = "保存的字符不能有 '|' 或 '&'符号";
                return 1;
            }


            try
            {
                if (!File.Exists(fileName))
                {
                    fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
                }
                string[] datas = File.ReadAllLines("xzj");
                if (datas != null)
                {
                    for (int i = 0; i < datas.Length; i++)
                    {
                        string[] arrays = datas[i].Split(new char[1] { '|' });
                        
                    }
                }

               
                //StreamWriter sw = new StreamWriter(fileName, true); // 创建写入流
                StreamWriter sw = new StreamWriter(fileName); // 创建写入流
                sw.WriteLine("{key:{"+key+",value:"+value+"}|"); // 写入
                sw.Close(); //关闭文件
                flag = 2;
            }
            catch
            {
                flag = 0;
            }
            return flag;
        }

        //读取数据
        public static string getData(string key)
        {
            string flag = null;
            FileStream fs = null; ;
            try
            {
                string[] datas = File.ReadAllLines("xzj");
                if (datas == null) return null;

                for (int i = 0; i < datas.Length; i++)
                {
                    string[] arrays = datas[i].Split(new char[1]{'|'});
                    if (arrays[0].Equals(key))
                    {
                        return arrays[1];
                    }
                }
            }
            catch
            {
                //flag = false;
            }
            return flag;
        }

    }
}
