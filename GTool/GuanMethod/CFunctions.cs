using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GuanMethod
{
    public class CFunctions
    {
        #region 字符串相关功能
        /// <summary>
        /// 字符串是否包含某子字符串
        /// </summary>
        /// <param name="str1">字符串</param>
        /// <param name="str2">子字符串</param>
        /// <returns>True或Flase</returns>
        public static bool IsInclude(string str1, string str2)
        {
            if (str1.IndexOf(str2) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取字符串中的子字符串(截取左侧的字符串)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="overstr">截止字符串
        /// <para>(注：该字符串以从左侧起检索到的第一个字符串为准)</para></param>
        /// <returns>字符串中的子字符串</returns>
        public static string GetLeftString(string str, string overstr)
        {
            return str.Substring(0, str.IndexOf(overstr));
        }

        /// <summary>
        /// 获取字符串中的子字符串(截取右侧的字符串)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="startstr">起始字符串
        /// <para>(注：该字符串以从左侧起检索到的第一个字符串为准)</para></param>
        /// <returns>字符串中的子字符串</returns>
        public static string GetRightString(string str, string startstr)
        {
            return str.Substring(str.IndexOf(startstr) + startstr.Length);
        }

        /// <summary>
        /// 获取字符串中的子字符串(截取中间部分的字符串)
        /// </summary>
        /// <param name="str">字符串</</param>
        /// <param name="leftcut"></param>
        /// <param name="rightcut"></param>
        /// <returns></returns>
        public static string GetMiddleString(string str, string startstr, string overstr)
        {
            return str.Substring(str.IndexOf(startstr) + startstr.Length, str.IndexOf(overstr) - str.IndexOf(startstr) - startstr.Length);
        }

        /// <summary>
        /// 从字符串中获取一维数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator1">分隔符1</param>
        /// <returns>ArrayList类型的一维数组</returns>
        public static ArrayList GetArrayListFromString(string str, string separator1)
        {
            ArrayList arr = new ArrayList();
            string[] stritem = str.Split(separator1.ToCharArray());
            foreach (string item in stritem)
            {
                arr.Add(item);
            }
            return arr;
        }

        /// <summary>
        /// 从字符串中获取二维数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator1">分隔符1</param>
        /// <param name="separator2">分隔符2</param>
        /// <returns>ArrayList类型的二维数组</returns>
        public static ArrayList GetArrayListFromString(string str, string separator1, string separator2)
        {
            ArrayList arr = new ArrayList();
            string[] stritem1 = str.Split(separator1.ToCharArray());
            foreach (string item1 in stritem1)
            {
                string[] stritem2 = item1.Split(separator2.ToCharArray());
                arr.Add(stritem2);
            }
            return arr;
        }

        /// <summary>
        /// 移除无效的0
        /// </summary>
        /// <param name="str">需移除无效的0的字符串</param>
        /// <returns>移除无效的0后的字符串</returns>
        public static string RemoveInvalidZero(string str)
        {
            int num = 0;
            string str1 = "";
            string str2 = "";
            foreach (char a in str)
            {
                if (a >= '0' && a <= '9')
                {
                    if (string.IsNullOrEmpty(str1))
                    {
                        str1 = str.Substring(0, num);
                    }
                    str2 = str.Substring(num + 1);
                    if (str2[0] == '0' && str2.Length > 1)
                    {
                        str2 = str2.Substring(1);
                    }
                    else
                    {
                        break;
                    }
                }
                num++;
            }
            str = str1 + str2;
            return str;
        }

        #endregion

        #region 创建随机数

        /// <summary>创建随机ID，需输入ID位数：
        /// <para>10位：年+月+日+2位随机数</para>
        /// <para>12位：年+月+日+4位随机数</para>
        /// <para>16位：年+月+日+时+分+秒+2位随机数</para>
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string CreateID(int Num)
        {
            string ID = "";
            switch (Num)
            {
                default: break;
                case 10:

                    break;
                case 12:

                    break;
                case 16:

                    break;
            }
            return ID;
        }

        #endregion
    }
}
