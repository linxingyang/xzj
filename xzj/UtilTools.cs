using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace xzj
{
    class UtilTools
    {
        //获取当前周几
        public static String getWeek()
        {
            DayOfWeek dayOfWeek = new DayOfWeek();
            return dayOfWeek.ToString();
        }

        //获取当前时间
        public static String getTime()
        {
            return DateTime.Now.ToString("hh:mm:ss");
        }

        //获取当前日期
        public static String getDate()
        {
            return DateTime.Now.ToString("yyyy:mm:dd");
        }

        //创建文件，用来保存数据
        public static FileStream createFile(string fileName)
        {
            if (!File.Exists(fileName))
            {

            }

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
            return fs;
        }

        //用来保存数据,返回值 0 插入异常 1 插入失败("保存的字符不能有 '|' 符号") 2 插入成功
        public static int setData(string key, string value)
        {
            int flag = 0;
            string fileName = "xzj";
            FileStream fs = null;

            if (key.Contains("|") || value.Contains("|"))
            {
                //flag = "保存的字符不能有 '|' 符号";
                return 1;
            }

            try
            {
                if (!File.Exists(fileName))
                {
                    fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
                }

                StreamWriter sw = new StreamWriter(fileName, true); // 创建写入流
                sw.WriteLine(key+"|"+value); // 写入Hello World
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
