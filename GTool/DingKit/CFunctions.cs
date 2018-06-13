using System;
using System.Text;
//using System.Web.Security;
using System.Data;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.UI;
//using System.Web.UI.WebControls;

namespace DingKit
{
	/// <summary>
	/// 该类主要实现一些公共操作函数
	/// </summary>
	public class CFun
	{
        /// <summary>
        /// 默认显示图片
        /// </summary>
        public static string DefaultPic = CConfig.GetValueByKey("DefaultPic");


       /// <summary>
       /// 出错处理方式
       /// </summary>
        public enum CERR_REPORT_TYPE:int
        {
            /// <summary>
            /// 警告框；
            /// </summary>
            ALERT=0,
            /// <summary>
            /// 错误页面；
            /// </summary>
            ERRPAGE,
            /// <summary>
            /// 警告框后，转错误页面；
            /// </summary>
            ALERT_ERRPAGE,
            /// <summary>
            /// 返回上级页面；
            /// </summary>
            BAKE,
            /// <summary>
            /// 警告框后，返回上级页面；
            /// </summary>
            ALERT_BAKE
        }

        /// <summary>
        /// 构造
        /// </summary>
        public CFun()
		{

		}

       
        /// <summary>
        /// 参数检测-防止恶意代码嵌入
        /// </summary>
        /// <param name="Param">待检测参数</param>
        /// <returns>处理过的参数</returns>
        public static string Quote(string Param)
        {
            if (Param == null || Param.Length == 0)
            {
                return "";
            }
            else
            {
                //防止恶意代码嵌入
                string str = Param.Replace("'", "");
                str = str.Replace("--", "");
                return str;
            }
        }

     

        /// <summary>
        /// 图片显示（如无着采用替换图片）
        /// </summary>
        /// <param name="curPic">当前图片地址</param>
        /// <param name="ReplacePiC">替换地址</param>
        /// <returns></returns>
        public static string ShowPic(string curPic,string ReplacePiC)
        {
            if (curPic == "")
            {
                return ReplacePiC;
            }
            else
            {
                return curPic;
            }
 
        }

        /// <summary>
        /// 图片显示（如无着采用web.config中定义的默认图片）
        /// </summary>
        /// <param name="curPic">当前图片地址</param>
        /// <param name="ReplacePiC">替换地址</param>
        /// <returns></returns>
        public static string ShowPicDefalut(string curPic)
        {
            if (curPic == "")
            {
                return DefaultPic;
            }
            else
            {
                return curPic;
            }

        }

        /// <summary>
        /// 处理插入值中单引号
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static string ToSqlString(string strSql)
        {
            if (strSql != null)
            {
                return strSql.Replace("'", "''");
            }
            return null;
        }



        /// <summary>
        /// 数值型检测
        /// </summary>
        /// <param name="value">待检测值</param>
        /// <returns>是否数值型</returns>
        public static bool IsNumeric(string value)
        {
            try
            {
                Decimal temp = Convert.ToDecimal(value);
                return true;
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// 获取指定QueryString的值
        /// </summary>
        /// <param name="ParamName">指定QueryString的名称</param>
        /// <returns>相应的值</returns>
        public static string GetParam(string ParamName)
        {
            string Param = HttpContext.Current.Request.QueryString[ParamName];
            if (Param == null)
                Param = HttpContext.Current.Request.Form[ParamName];
            if (Param == null)
                return "";
            else
                return Quote(Param);
        }

        /// <summary>
        /// 隐藏出错提示(需要JS配合)
        /// </summary>
        public static void HideFailHint()
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>setTimeout('hideFailHint()',8000)</script>");
        }


        /// <summary>
        /// 隐藏成功提示(需要JS配合)
        /// </summary>
        public static void HideSuccHint()
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>setTimeout('hideSuccHint()',8000)</script>");
        }




        /// <summary>
        /// 用Javascript的Window.alert()方法输出提示信息
        /// </summary>
        /// <param name="strMsg">要输出的信息</param>
        /// <param name="Back">输出信息后是否要history.back()</param>
        /// <param name="End">输出信息后是否要Response.end()</param>
        public static void WriteMessage(string strMsg, bool Back, bool End)
        {
       
            //将单引号替换，防止javascript出错
            strMsg = strMsg.Replace("'", "“");
            //将回车换行符去掉，防止javascript出错
            strMsg = strMsg.Replace("\r\n", "");
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>alert('" + strMsg + "');</script>");
            if (Back)
                System.Web.HttpContext.Current.Response.Write("<script language=javascript>history.back();</script>");
            if (End)
                System.Web.HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 出错时弹出提示对话框
        /// </summary>
        /// <param name="str_Prompt">提示信息</param>
        public static string JsAlerT(string str_Prompt)
        {
            string str = "<script language=\"javascript\">alert('" + str_Prompt + "');</" + "script>";
            System.Web.HttpContext.Current.Response.Write(str);
            return str;
        }

        /// <summary>
        /// 以Response.Write()方式输出一条JavaScript语句
        /// </summary>
        /// <param name="strJavaScript">要输出的JavaScript语句</param>
        public static void WriteJavaScript(string strJavaScript)
        {
            System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">" + strJavaScript + "</script>");
        }


        /// <summary>
        /// 以Response.Write()方式输出一条JavaScript语句
        /// </summary>
        /// <param name="strJavaScript">要输出的JavaScript语句</param>
        public static string  FortmatJavaScript(string strJavaScript)
        {
            return "<script type=\"text/javascript\">" + strJavaScript + "</script>";
        }




        /// <summary>
        /// 字符串添加引号
        /// </summary>
        /// <param name="attr">数据库对应字段名</param>
        /// <param name="mvalue">数据库对应字段名值</param>
        public static string FormatWhere(string attr, string mvalue)
        {
            mvalue = CFun.CleanString(mvalue);
            if (mvalue == null)
            {
                mvalue = "";
            }

            return attr + "='" + mvalue + "' and ";
        }

        /// <summary>
        /// 字符串添加引号
        /// </summary>
        /// <param name="attr">数据库对应字段名</param>
        /// <param name="mvalue">数据库对应字段名值</param>
        /// <param name="isstring">数据库对应是否是字符型</param>
        /// <returns>添加引号后的字符串</returns>
        public static string FormatWhere(string attr, string mvalue, bool isstring)
        {
            mvalue = CFun.CleanString(mvalue);
            if (mvalue == null)
            {
                mvalue = "";
            }

            if (isstring == true)
                return attr + "='" + mvalue + "' and ";
            else
                return attr + "=" + mvalue + " and  ";
        }


        /// <summary>
        /// 字符串添加引号
        /// </summary>
        /// <param name="attr">数据库对应字段名</param>
        /// <param name="mvalue">数据库对应字段名值</param>
        /// <param name="isstring">数据库对应是否是字符型</param>
        /// <param name="islast">是否是最后项</param>
        /// <returns>添加引号后的字符串</returns>
        public static string FormatWhere(string attr, string mvalue, bool isstring, bool islast)
        {
            mvalue = CFun.CleanString(mvalue);
            if (mvalue == null)
            {
                mvalue = "";
            }

            if (islast == true)
            {
                if (isstring == true)
                    return attr + "='" + mvalue + "' ";
                else
                    return attr + "=" + mvalue;
            }
            else
                return FormatWhere(attr, mvalue, isstring);

        }



        /// <summary>
        /// 字符串添加引号
        /// </summary>
        /// <param name="attr">数据库对应字段名</param>
        /// <param name="mvalue">数据库对应字段名值</param>
        public static string FormatUpdate(string attr, string mvalue)
        {
            mvalue = CFun.CleanString(mvalue);
            return attr + "='" + mvalue + "' , ";

        }

        /// <summary>
        /// 字符串添加引号
        /// </summary>
        /// <param name="attr">数据库对应字段名</param>
        /// <param name="mvalue">数据库对应字段名值</param>
        /// <param name="isstring">数据库对应是否是字符型</param>
        /// <returns>添加引号后的字符串</returns>
        public static string FormatUpdate(string attr, string mvalue, bool isstring)
        {
            mvalue = CFun.CleanString(mvalue);
            if (isstring == true)
                return attr + "='" + mvalue + "',";
            else
                return attr + "=" + mvalue + " , ";
        }


        /// <summary>
        /// 字符串添加引号
        /// </summary>
        /// <param name="attr">数据库对应字段名</param>
        /// <param name="mvalue">数据库对应字段名值</param>
        /// <param name="isstring">数据库对应是否是字符型</param>
        /// <param name="islast">是否是最后项</param>
        /// <returns>添加引号后的字符串</returns>
        public static string FormatUpdate(string attr, string mvalue, bool isstring, bool islast)
        {
            mvalue = CFun.CleanString(mvalue);
            if (islast == true)
            {
                if (isstring == true)
                    return attr + "='" + mvalue + "' ";
                else
                    return attr + "=" + mvalue;
            }
            else
                return FormatUpdate(attr, mvalue, isstring);

        }


        /// <summary>
        /// 用于SQL插入语句合成
        /// </summary>
        /// <param name="str">字符串添加引号</param>
        /// <returns>添加引号后的字符串</returns>
        public static string FormatInsert(string str)
        {
            str = CFun.CleanString(str);
            if (str == null) 
            {
                str = "";
            }
  
           return  "'" + str.Trim() + "',";
        }

        /// <summary>
        /// 用于SQL插入语句合成,字符串添加引号
        /// </summary>
        /// <param name="str">需添加引号字符串</param>
        /// <param name="isstring">数据库对应是否是字符型</param>
        /// <returns>添加引号后的字符串</returns>
        public static string FormatInsert(string str, bool isstring)
        {
            str = CFun.CleanString(str);
            if (str == null)
            {
                str = "";
            }

            if (isstring == true)
                return "'" + str.Trim() + "',";
            else
                return "" + str.Trim() + ",";
        }


        /// <summary>
        /// 用于SQL插入语句合成,字符串添加引号
        /// </summary>
        /// <param name="str">需添加引号字符串</param>
        /// <param name="isstring">数据库对应是否是字符型</param>
        /// <param name="islast">是否是SQL语句的最后项</param>
        /// <returns>添加引号后的字符串</returns>
        public static string FormatInsert(string str, bool isstring, bool islast)
        {
            str = CFun.CleanString(str);
            if (str == null)
            {
                str = "";
            }

            if (islast == true)
            {
                if (isstring == true)
                    return "'" + str.Trim() + "'";
                else
                    return str.Trim();
            }
            else
                return FormatInsert(str, isstring);

        }


        /// <summary>
        /// HTMLEncode转化
        /// </summary>
        /// <param name="str">字符</param>
        /// <returns>HTML</returns>
		public static string Encode(string str)
		{			
            //str = str.Replace("&","&amp;");
			str = str.Replace("'","''");
			str = str.Replace("\"","&quot;");
            //str = str.Replace(" ","&nbsp;");
			str = str.Replace("<","&lt;");
			str = str.Replace(">","&gt;");
			str = str.Replace("\n","<br>");
			return str;
		}

		/// <summary>
        /// HTMLdecode
		/// </summary>
        /// <param name="str">HTML</param>
        /// <returns>字符</returns>
		public static string Decode(string str)
		{			
			str = str.Replace("<br>","\n");
			str = str.Replace("&gt;",">");
			str = str.Replace("&lt;","<");
            //str = str.Replace("&nbsp;"," ");
			str = str.Replace("&quot;","\"");
			return str;
		}

        /// <summary>
        /// 文本加颜色
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="ColorName">颜色</param>
        /// <returns>加过颜色的文本</returns>
        public static string WithColor(string str,string ColorName)
        {
            return "<font color='" + ColorName + "'>" + str + "</font>";
        }
        

        /// <summary>
        /// 字符串是否包含
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="subStr">子串</param>
        /// <returns></returns>
        public static bool IsStrInclude(string str,string subStr)
        {
            if (str.IndexOf(subStr) > -1)
            {
                return true;
            }
            else
                return false;
        }

       /// <summary>
       ///获取网页名称（含扩展名不含url参数）
       /// </summary>
       /// <param name="Url">URL地址</param>
       /// <param name="HaveSubDir">是否包含上级目录</param>
       /// <returns></returns>
        public static string GetPageName(string Url,bool HaveSubDir)
        {
            if (Url == "")
            {
                return "";
            }
            string[] Arr_str, Arr_str1;
            Char[] split ={ '/'};
            Arr_str = Url.Split(split);
            Url = Arr_str[Arr_str.Length - 1];
            Char[] split1 ={ '?' };
            Arr_str1 = Url.Split(split1);

            if (HaveSubDir == true)
            {
                string Pagename = Arr_str[Arr_str.Length-2]+"/"+Arr_str1[0];
                return Pagename;
            }
            else
            {
                return Arr_str1[0];
            }
        }


        /// <summary>
        /// 获取网页名称（不包含扩展名）
        /// </summary>
        /// <param name="pageName">网页名称</param>
        /// <returns></returns>
        public static string GetPageNameWithoutExt(string pageName)
        {
            if (pageName == "")
            {
                return "";
            }
            string[] Arr_str;
            Char[] split ={ '.' };
            Arr_str = pageName.Split(split);

            return Arr_str[0];
           
        }


        /// <summary>
        /// 获取网页名称（包含扩展名及url参数）
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="HaveSubDir"></param>
        /// <returns></returns>
        public static string GetPageNameWithParam(string Url, bool HaveSubDir)
        {
            if (Url == "")
            {
                return "";
            }
            string[] Arr_str;
            Char[] split ={ '/' };
            Arr_str = Url.Split(split);
            if (HaveSubDir == true)
            {
                string Pagename = Arr_str[Arr_str.Length - 2] + "/" + Arr_str[Arr_str.Length - 1];
                return Pagename;
            }
            else
            {
                return Arr_str[Arr_str.Length - 1];
            }
        }


        /// <summary>
        /// 字符串Left截取
        /// </summary>
        /// <param name="sSource">字符串</param>
        /// <param name="iLength">长度</param>
        /// <returns>截取后</returns>
		public static string Left(string   sSource,   int   iLength)   
		{   
			sSource = sSource.Trim();
			return sSource.Substring(0,iLength > sSource.Length?sSource.Length:iLength);   
		}

        /// <summary>
        /// 字符串限制截取（超长加…）
        /// </summary>
        /// <param name="sSource">字符串</param>
        /// <param name="iLength">长度</param>
        /// <returns>截取后</returns>
        public static string LimitCut(string sSource, int iLength)
        {
            sSource = sSource.Trim();
            if (sSource.Length < iLength)
            {
                return sSource;
            }
            else
            {
                iLength = iLength - 2;
                sSource = sSource.Substring(0, iLength > sSource.Length ? sSource.Length : iLength);
                sSource = sSource+"…";
                return sSource;
            }
        }

        /// <summary>
        /// HTML文字提纯（超长加…）
        /// </summary>
        /// <param name="sSource">HTML字符串</param>
        /// <param name="iLength">截取长度</param>
        /// <returns></returns>
        public string LimitCutHTML(string sSource, int iLength)
        {
            return CFun.LimitCut(CHtml.DelHTML(sSource), iLength);
        }


        /// <summary>
        /// 字符串Right截取
        /// </summary>
        /// <param name="sSource">字符串</param>
        /// <param name="iLength">长度</param>
        /// <returns>截取后</returns>
		public static string Right(string sSource,int iLength)   
		{   
			return sSource.Substring(iLength>sSource.Length?0 : sSource.Length - iLength);   
		}   

        /// <summary>
        /// 字符串A中包含字符串B
        /// </summary>
        /// <param name="a">母体字符串</param>
        /// <param name="b">包含的字符串</param>
        /// <returns></returns>
        public static bool IsInclude(string a,string b )
        {
            if (a.IndexOf(b) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    
        /// <summary>
        /// 字符串Mid截取
        /// </summary>
        /// <param name="sSource">字符串</param>
        /// <param name="iStart">起始位置</param>
        /// <param name="iLength">截止位置</param>
        /// <returns>截取后</returns>
		public static string Mid(string sSource, int iStart,int iLength)   
		{   
			int iStartPoint = iStart>sSource.Length ? sSource.Length : iStart;   
			return  sSource.Substring(iStartPoint,iStartPoint + iLength> sSource.Length ? sSource.Length - iStartPoint : iLength);   
		}


        /// <summary>
        /// HTML文字提纯
        /// </summary>
        /// <param name="sSource">HTML字符串</param>
        /// <param name="iLength">截取长度</param>
        /// <returns></returns>
        public static string LeftCutHTML(string sSource, int iLength)
        {
            string str = sSource;
            str = CFun.Decode(str);
            str = CHtml.DelHTML(str);
            if (sSource.Length > iLength)
            {
                str = sSource.Trim();
                str = str.Substring(0, iLength > str.Length ? sSource.Length : iLength);
                return str;
            }
            else
            {
                return str;
            }

        }


        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="sSource">字符串</param>
        /// <param name="iLength">长度</param>
        /// <returns>截取后</returns>
        public static string LeftCut(string sSource, int iLength)
        {
            string str = sSource;
            if (sSource.Length > iLength)
            {
                str = sSource.Trim();
                str = str.Substring(0, iLength > str.Length ? sSource.Length : iLength);

                return str;
            }
            else
            {
                return str;
            }
           
        }   

      /// <summary>
        /// 字符加密,md5,32位
      /// </summary>
      /// <param name="Password">待加密字段</param>
      /// <returns>加密后</returns>
		public static string Encrypt(string Password)
		{
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "md5");
  
		}

        /// <summary>
        /// 字符加密,md5,16位
        /// </summary>
        /// <param name="ConvertString">待加密字段</param>
        /// <returns>加密后</returns>
        public static string GetMd5Str(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }





		/// <summary>
        /// 字符加密(0：SHA1,1： MD5)
		/// </summary>
        /// <param name="Password">待加密字段</param>
		/// <param name="Format">加密格式,0：SHA1,1： MD5</param>
		/// <returns>加密后</returns>
		public static string Encrypt(string Password,int Format)
		{
			string str = "";
			switch(Format)
			{
				case 0:
					str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Password,"SHA1");
					break;
				case 1:
					str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Password,"MD5");
					break;
			}
			return str;
		}



		/// <summary>
        /// 字符解密
		/// </summary>
        /// <param name="Passowrd">待解密字段</param>
		/// <returns></returns>
		public static string Decrypt(string Passowrd)
		{
			string str="";
			str= System.Web.Security.FormsAuthentication.Decrypt(Passowrd).Name.ToString();
			return str;
		}

        /// <summary>
        ///  Cookie加密
        /// </summary>
        /// <param name="strCookie">待加密cookie</param>
        /// <param name="type"> 加密类型：奇数：DeTransform2；偶数：DeTransform1</param>
        /// <returns>加密后cookie</returns>
		public static string EncryptCookie(string strCookie,int type)
		{
			string str=En(strCookie,type);
			StringBuilder sb = new StringBuilder();
			foreach(char a in str)
			{		
				sb.Append(Convert.ToString(a,16).PadLeft(4,'0'));
			}
			return sb.ToString();
		}



        /// <summary>
        ///  Cookie解密
        /// </summary>
        /// <param name="strCookie">待解密cookie</param>
        /// <param name="type"> 解密类型：奇数：DeTransform2；偶数：DeTransform1</param>
        /// <returns>解密后cookie</returns>
		public static string DecryptCookie(string strCookie,int type)
		{
			StringBuilder sb = new StringBuilder();
			string [] strarr = new String[255]; 
			int i,j,count=strCookie.Length/4;
			string strTmp;

			for(i=0;i<count;i++)
			{
				for(j=0;j<4;j++)
				{
					sb.Append(strCookie.Substring(i*4+j,1));
				}
				strarr[i] = sb.ToString();
				sb.Remove(0,sb.Length);
			}

			for(i=0;i<count;i++)
			{		
				strTmp = uint.Parse(strarr[i],System.Globalization.NumberStyles.AllowHexSpecifier).ToString("D");
				char ch = (char)uint.Parse(strTmp);
				sb.Append(ch);
			}

			return De(sb.ToString(),type);
		}


        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strCookie"> 加密Cookie</param>
        /// <param name="type"> 加密类型：奇数：DeTransform2；偶数：DeTransform1</param>
        /// <returns></returns>
        private static string En(string strCookie, int type)
        {
            string str;
            if (type % 2 == 0)
            {
                str = Transform1(strCookie);
            }
            else
            {
                str = Transform3(strCookie);
            }

            str = Transform2(strCookie);
            return str;
        }




		/// <summary>
		/// 解密
		/// </summary>
        /// <param name="strCookie">解密Cookie</param>
        /// <param name="type">解密类型：奇数：DeTransform2；偶数：DeTransform1</param>
		/// <returns></returns>
		private static string De(string strCookie,int type)
		{
			string str;
			if(type % 2==0)
			{
				str = DeTransform1(strCookie);
			}
			else
			{
				str = DeTransform3(strCookie);
			}

			str = Transform2(strCookie);	
			return str;
		}


		/// <summary>
		/// 解密方法1
		/// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解密后的字符串</returns>
		public static string DeTransform1(string str)
		{			
			int i=0;
			StringBuilder sb = new StringBuilder();
			
			foreach(char a in str)
			{						
				switch(i % 6)
				{
					case 0:
						sb.Append((char)(a-1));
						break;
					case 1:
						sb.Append((char)(a-5));
						break;
					case 2:
						sb.Append((char)(a-7));
						break;
					case 3:
						sb.Append((char)(a-2));
						break;
					case 4:
						sb.Append((char)(a-4));
						break;
					case 5:
						sb.Append((char)(a-9));
						break;
				}
				i++;
			}

			return sb.ToString();
		}



        /// <summary>
        /// 加密方法1
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>加密后的字符串</returns>
		public static string Transform1(string str)
		{			
			int i=0;
			StringBuilder sb = new StringBuilder();
			
			foreach(char a in str)
			{						
				switch(i % 6)
				{
					case 0:
						sb.Append((char)(a+1));
						break;
					case 1:
						sb.Append((char)(a+5));
						break;
					case 2:
						sb.Append((char)(a+7));
						break;
					case 3:
						sb.Append((char)(a+2));
						break;
					case 4:
						sb.Append((char)(a+4));
						break;
					case 5:
						sb.Append((char)(a+9));
						break;
				}
				i++;
			}

			return sb.ToString();
		}

        /// <summary>
        /// 转换方法2
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转换后的字符串</returns>
		public static string Transform2(string str)
		{
			uint j=0;
			StringBuilder sb = new StringBuilder();
			
			str=Reverse(str);
			foreach(char a in str)
			{	
				j=a;		
				if(j>255)
				{
					j=(uint)((a>>8) + ((a&0x0ff)<<8));
				}
				else
				{					
					j=(uint)((a>>4) + ((a&0x0f)<<4));
				}				
				sb.Append((char)j);
			}

			return sb.ToString();
		}




        /// <summary>
        /// 解密方法3
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解密后的字符串</returns>
		public static string DeTransform3(string str)
		{			
			int i=0;
			StringBuilder sb = new StringBuilder();
			
			foreach(char a in str)
			{						
				switch(i % 6)
				{
					case 0:
						sb.Append((char)(a-3));
						break;
					case 1:
						sb.Append((char)(a-6));
						break;
					case 2:
						sb.Append((char)(a-8));
						break;
					case 3:
						sb.Append((char)(a-7));
						break;
					case 4:
						sb.Append((char)(a-5));
						break;
					case 5:
						sb.Append((char)(a-2));
						break;
				}
				i++;
			}

			return sb.ToString();
		}


        /// <summary>
        /// 获取当前日期的周次
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public static int getWeekDay(DateTime dt)
        {
            int y = dt.Year;
            int m = dt.Month;
            int d = dt.Day;
            if (m == 1) m = 13;
            if (m == 2) m = 14;
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
            return week;
        }


        /// <summary>
        /// 加密方法3
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>加密后的字符串</returns>
		public static string Transform3(string str)
		{			
			int i=0;
			StringBuilder sb = new StringBuilder();
			
			foreach(char a in str)
			{						
				switch(i % 6)
				{
					case 0:
						sb.Append((char)(a+3));
						break;
					case 1:
						sb.Append((char)(a+6));
						break;
					case 2:
						sb.Append((char)(a+8));
						break;
					case 3:
						sb.Append((char)(a+7));
						break;
					case 4:
						sb.Append((char)(a+5));
						break;
					case 5:
						sb.Append((char)(a+2));
						break;
				}
				i++;
			}

			return sb.ToString();
		}

        /// <summary>
        /// 过滤SQL注入
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CleanString(string inputString)
        {
            return inputString;
            //StringBuilder retVal = new StringBuilder();

            ////sql注入
            //if (CFun.SqlFilter(inputString) == true)
            //{
            //    return "";
            //}

            //if ((inputString != null) && (inputString != String.Empty))
            //{
            //    inputString = inputString.Trim();
            //    if (CFun.IsInclude(inputString.ToLower(), "“>"))
            //    {
            //        inputString = CFun.Right(inputString, inputString.IndexOf("“>"));
            //    }
            //    if (CFun.IsInclude(inputString.ToLower(), "<script"))
            //    {
            //        inputString = CFun.Right(inputString, inputString.IndexOf("<script"));
            //    }

            //    for (int i = 0; i < inputString.Length; i++)
            //    {
            //        switch (inputString[i])
            //        {
            //            case '"':
            //                retVal.Append("&quot;");
            //                break;

            //            default:
            //                retVal.Append(inputString[i]);
            //                break;
            //        }
            //    }

            //    retVal.Replace("--", "__");
            //}

            //return retVal.ToString();
        }   



		/// <summary>
		/// 字符串倒序
		/// </summary>
		/// <param name="str">字符串</param>
        /// <returns>倒序后字符串</returns>
		public static string Reverse(string str)
		{
			int i;
			StringBuilder sb = new StringBuilder();

			for(i=str.Length-1;i>=0;i--)
			{
				sb.Append(str[i]);
			}

			return sb.ToString();
		}


        ///// <summary>
        ///// 获取客户端IP
        ///// </summary>
        ///// <param name="page">页面</param>
        ///// <returns></returns>
        public static string GetClientIP(System.Web.UI.Page page)
        {
            string ipAddress = "";
            if (page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
            {
                ipAddress = page.Request.ServerVariables["Remote_Addr"];
            }
            else
            {
                ipAddress = page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return ipAddress;
        }

        /// <summary> 
        /// 获取本机IP 
        /// </summary> 
        /// <returns>本机IP</returns> 
        //public static string GetClientIP()
        //{
        //    string CustomerIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        //    return CustomerIP;
        //    //string hostName = System.Net.Dns.GetHostName();
        //    //System.Net.IPHostEntry hostInfo = System.Net.Dns.GetHostEntry(hostName);// .GetHostByName(hostName);
        //    //System.Net.IPAddress[] IpAddr = hostInfo.AddressList;
        //    //string localIP = string.Empty;
        //    ////for (int i = 0; i < IpAddr.Length; i++)
        //    ////{
        //    ////    localIP += IpAddr[i].ToString();
        //    ////}
        //    ////if (IpAddr.Length > 0)
        //    ////{
        //    ////    localIP += IpAddr[0].ToString();
        //    ////}
        //    ////return localIP;
        //    //if (IpAddr.Length >= 2)
        //    //{
        //    //    return IpAddr[1].ToString();
        //    //}
        //    //else
        //    //{
        //    //    return IpAddr[0].ToString();
        //    //}

        //} 



        /// <summary>
        /// 字符串关键字加红
        /// </summary>
        /// <param name="str">整个串</param>
        /// <param name="key">关键字</param>
        /// <param name="size">字体大小</param>
        /// <returns></returns>
        public static string ToRedFont(string str, string key,string size)
        {
            string[] sArray = key.Split(' ');
            string sty = "<span style='color:#FF0000; font-size:" + size + "' >";

            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split(',');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split('，');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split(';');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split('；');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            return str.Replace(key, sty + key + "</span>");
        }

         /// <summary>
        /// 字符串关键字加红
        /// </summary>
        /// <param name="str">整个串</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string ToRedFontSingle(string str)
        {
            string sty = "<span style='color:#FF0000' >";
            return sty + str + "</span>";
        }
        /// <summary>
        /// 字符串关键字加红
        /// </summary>
        /// <param name="str">整个串</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string ToRedFont(string str, string key)
        {
            string[] sArray = key.Split(' ');
            string sty = "<span style='color:#FF0000' >";

            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split(',');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split('，');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split(';');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split('；');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            return str.Replace(key, sty + key + "</span>");
        }


        /// <summary>
        /// 邮箱是否有效
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }





        /// <summary>
        /// 日期间隔计算
        /// </summary>
        /// <param name="Date1">起始日期</param>
        /// <param name="Date2">截至日期</param>
        /// <param name="Interval">类型(年y,月M,日D,时h,分m,秒s,微秒ms)</param>
        /// <returns></returns>
        public static int DateDiff(System.DateTime Date1, System.DateTime Date2, string Interval)
        {
            double dblYearLen = 365;//年的长度，365天   
            double dblMonthLen = (365 / 12);//每个月平均的天数   
            System.TimeSpan objT;
            objT = Date2.Subtract(Date1);
            switch (Interval)
            {
                case "y"://返回日期的年份间隔   
                    return System.Convert.ToInt32(objT.Days / dblYearLen);
                case "M"://返回日期的月份间隔   
                    return System.Convert.ToInt32(objT.Days / dblMonthLen);
                case "d"://返回日期的天数间隔   
                    return objT.Days;
                case "h"://返回日期的小时间隔   
                    return objT.Hours;
                case "m"://返回日期的分钟间隔   
                    return objT.Minutes;
                case "s"://返回日期的秒钟间隔   
                    return objT.Seconds;
                case "ms"://返回时间的微秒间隔   
                    return objT.Milliseconds;
                default:
                    break;
            }
            return 0;
        }


        /// <summary> 
        ///SQL注入过滤 
        /// </summary> 
        /// <param name="InText">要过滤的字符串</param> 
        /// <returns>如果参数存在不安全字符，则返回true</returns> 
        public static bool SqlFilter(string InText)
        {
            string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join";
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断Session中某键值是否存在，如果不存在，输出提示信息
        /// 并且转向相应页面
        /// </summary>
        /// <param name="SessionName">Session中键名称</param>
        /// <param name="Message">提示信息</param>
        /// <param name="RedirectUrl">要转向的页面</param>
        public static void CheckSession(string SessionName, string Message, string RedirectUrl)
        {
            if (HttpContext.Current.Session[SessionName] == null || HttpContext.Current.Session[SessionName].ToString()=="")
            {
                WriteMessage(Message, false, false);
                string js = "top.document.location='" + RedirectUrl + "';";
                WriteJavaScript(js); ;
            }
        }

        /// <summary>
        /// 判断Session中某键值是否存在，如果不存在，输出提示信息
        /// 并且转向相应页面
        /// </summary>
        /// <param name="CookieName">Cookie中键名称</param>
        /// <param name="Message">提示信息</param>
        /// <param name="RedirectUrl">要转向的页面</param>
        public static void CheckCookie(string CookieName, string Message, string RedirectUrl)
        {
            if (HttpContext.Current.Response.Cookies[CookieName] == null || HttpContext.Current.Response.Cookies[CookieName].ToString() == "")
            {
                WriteMessage(Message, false, false);
                string js = "top.document.location='" + RedirectUrl + "';";
                WriteJavaScript(js); ;
            }
        }

        /// <summary>
        /// 提示后跳转页面
        /// </summary>
        /// <param name="strMsg">提示信息</param>
        /// <param name="strUrl">URL</param>
        public static void JsAlerT(string strMsg, string strUrl)
        {
            string str = "<script language=\"javascript\">alert('" + strMsg + "');</" + "script>";
            System.Web.HttpContext.Current.Response.Write(str);
            str = "<script language=javascript>window.location.href='" + strUrl + "';</script>";
            System.Web.HttpContext.Current.Response.Write(str);
        } 


        /// <summary>
        /// 错误处理函数
        /// </summary>
        /// <param name="strMsg">错误信息</param>
        /// <param name="RedirectUrl">重定向网址</param>
        /// <param name="type">错误报告类型</param>
        public static void ReportError(string strMsg, string RedirectUrl, CERR_REPORT_TYPE type)
        {
            //将单引号替换，防止javascript出错
            strMsg = strMsg.Replace("'", "“");
            //将回车换行符去掉，防止javascript出错
            strMsg = strMsg.Replace("\r\n", "");
            switch(type)
            {
                case CERR_REPORT_TYPE.ALERT:
                    JsAlerT(strMsg);
                    break;
                case CERR_REPORT_TYPE.ALERT_ERRPAGE:
                    JsAlerT(strMsg);
                    WriteJavaScript("top.document.location='" + RedirectUrl + "';"); 
                    break;
                case CERR_REPORT_TYPE.BAKE:
                    WriteJavaScript("history.back();");
                    break;
                case CERR_REPORT_TYPE.ERRPAGE:
                    WriteJavaScript("top.document.location='" + RedirectUrl + "';");
                    break;
                case CERR_REPORT_TYPE.ALERT_BAKE:
                    JsAlerT(strMsg);
                    WriteJavaScript("history.back();");
                    break;
            }
     
        }


        /// <summary>
        /// 阿拉伯数字转化成中文数字
        /// </summary>
        /// <param name="strArabicNum"></param>
        /// <returns></returns>
        public static string ArabicNumToChinaNum(string strArabicNum)
        {
            switch (strArabicNum)
            {
                case "1":
                    return "一";
              
                case "2":
                    return "二";
           
                case "3":
                    return "三";
                  
                case "4":
                    return "四";
                   
                case "5":
                    return "五";
                   
                case "6":
                    return "六";
                  
                case "7":
                    return "七";

                case "8":
                    return "八";
                   
                case "9":
                    return "九";
                   
                default:
                    return "";
                   
            }
        }
     

        /// <summary>
        /// 中文数字转化成阿拉伯数字
        /// </summary>
        /// <param name="strChinaNum"></param>
        /// <returns></returns>
        public static string ChinaNumToArabicNum(string strChinaNum)
        {
            switch (strChinaNum)
            {
                case "一":
                    return "1";

                case "二":
                    return "2";
                   
                case "三":
                    return "3";
                  
                case "四":
                    return "4";
                   
                case "五":
                    return "5";
                    
                case "六":
                    return "6";
                
                case "七":
                    return "7";
                  
                case "八":
                    return "8";
                  
                case "九":
                    return "9";
                  
                default:
                    return "";
                   
            }
        }


     
        /// <summary>
        /// 创建16位的随机数
        /// </summary>
        /// <returns></returns>
        public static  string CreateID()
        {

            string TempYear, TempMonth, TempDay, TempHour, TempMinute, TempSecond, RandomFigure;
            DateTime NowTime;
            NowTime = DateTime.Now;
            TempYear = NowTime.Year.ToString();
            TempMonth = NowTime.Month.ToString();
            TempDay = NowTime.Day.ToString();
            TempHour = NowTime.Hour.ToString();
            TempMinute = NowTime.Minute.ToString();
            TempSecond = NowTime.Second.ToString();

            if (TempMonth.Length == 1)
            {
                TempMonth = "0" + TempMonth;
            }
            if (TempDay.Length == 1)
            {
                TempDay = "0" + TempDay;
            }
            if (TempHour.Length == 1)
            {
                TempHour = "0" + TempHour;
            }
            if (TempMinute.Length == 1)
            {
                TempMinute = "0" + TempMinute;
            }
            if (TempSecond.Length == 1)
            {
                TempSecond = "0" + TempSecond;
            }
            Random randObj = new Random();
            RandomFigure = randObj.Next(0, 1000).ToString();

            String str = TempYear + TempMonth + TempDay + TempHour + TempMinute + TempSecond + RandomFigure + "0000";
            return CFun.Left(str, 16);
        }


        /// <summary>
        /// 创建16位的随机数
        /// </summary>
        /// <returns></returns>
        public static string Create12ID()
        {

            string TempYear, TempMonth, TempDay, TempHour, TempMinute, TempSecond, RandomFigure;
            DateTime NowTime;
            NowTime = DateTime.Now;
            TempYear = NowTime.Year.ToString();
            TempMonth = NowTime.Month.ToString();
            TempDay = NowTime.Day.ToString();
            TempHour = NowTime.Hour.ToString();
            TempMinute = NowTime.Minute.ToString();
            TempSecond = NowTime.Second.ToString();

            if (TempMonth.Length == 1)
            {
                TempMonth = "0" + TempMonth;
            }
            if (TempDay.Length == 1)
            {
                TempDay = "0" + TempDay;
            }
            if (TempHour.Length == 1)
            {
                TempHour = "0" + TempHour;
            }
            if (TempMinute.Length == 1)
            {
                TempMinute = "0" + TempMinute;
            }
            if (TempSecond.Length == 1)
            {
                TempSecond = "0" + TempSecond;
            }
            Random randObj = new Random();
            RandomFigure = randObj.Next(0, 1000).ToString();

            String str = TempYear + TempMonth + TempDay + TempHour + TempMinute;
            return CFun.Left(str, 12);
        }
        /// <summary>
        /// 创建10位随机数
        /// </summary>
        /// <returns></returns>
        public static string Create10ID()
        {

            string TempYear, TempMonth, TempDay, RandomFigure;
            DateTime NowTime;
            NowTime = DateTime.Now;
            TempYear = NowTime.Year.ToString();
            TempMonth = NowTime.Month.ToString();
            TempDay = NowTime.Day.ToString();

            if (TempMonth.Length == 1)
            {
                TempMonth = "0" + TempMonth;
            }
            if (TempDay.Length == 1)
            {
                TempDay = "0" + TempDay;
            }
          
            Random randObj = new Random();
            RandomFigure = randObj.Next(0, 100).ToString();

            String str = TempYear + TempMonth + TempDay +  RandomFigure + "0000";
            return CFun.Left(str, 10);
        }

        

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="strInput">需要转换的字符串</param>
        /// <param name="strInStr">要进行半角转换到全角的字符列表</param>
        /// <returns>转换后的字符串</returns>
        public static string DBCToSBC(string strInput, string strInStr)
        {
            char[] c = strInput.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (strInStr.Contains(c[i].ToString()))
                {

                    if (c[i] == 32)
                    {
                        c[i] = (char)12288;
                        continue;
                    }
                    if (c[i] < 127)
                        c[i] = (char)(c[i] + 65248);

                }
            }
            return new string(c);
        }


        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>该日期在该年中的周数</returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            return gc.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }


        /// <summary>
        /// 动态生成翻页条
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageName"></param>
        /// <param name="UrlParam"></param>
        /// <returns></returns>
        public static string PageControl(int CurrentPage, string RecordCount, string PageSize, string PageName, string UrlParam)
        {
            StringBuilder sbTable = new StringBuilder();
            sbTable.Append("<table cellpadding='2' cellspacing='0' border='0' width='98%'>");
            sbTable.Append("<tr>");
            sbTable.Append("<td align='right'>");
            string strHref = "#";
            int PageCount = (int.Parse(RecordCount) + CurrentPage - 1) / int.Parse(PageSize);
            if (UrlParam != "")
            {
                strHref = PageName + "?" + UrlParam + "&curpage=";
            }
            else
            {
                strHref = PageName + "?" + "curpage=";
            }
            if (CurrentPage > 1)
            {
                sbTable.Append("<a href='" + strHref + "1'>首页</a>");
                sbTable.Append("<a href='" + strHref + Convert.ToString(CurrentPage - 1) + "'>上一页</a>");
            }
            else
            {
                sbTable.Append("<a>首页</a>");
                sbTable.Append("<a>上一页</a>");
            }
            int firstp = CurrentPage - 5;
            if (firstp < 0)
            {
                firstp = 1;
            }
            int j = 0;
            for (int i = firstp; i <= PageCount; i++)
            {
                if (j < 10)
                {
                    if (i != CurrentPage)
                    {
                        sbTable.Append("<a href='" + strHref + i.ToString() + "'>[" + i.ToString() + "]</a>");
                    }
                    else
                    {
                        sbTable.Append("<a>[" + i.ToString() + "]</a>");
                    }
                }
                j++;
            }
            if (CurrentPage < PageCount)
            {
                sbTable.Append("<a href='" + strHref + PageCount.ToString() + "'>首页</a>");
                sbTable.Append("<a href='" + strHref + Convert.ToString(CurrentPage + 1) + "'>上一页</a>");
            }
            else
            {
                sbTable.Append("<a>下一页</a>");
                sbTable.Append("<a>尾页</a>");
            }
            sbTable.Append("</td>");
            sbTable.Append("</tr>");
            sbTable.Append("</table>");
            return sbTable.ToString();
        } 


        /// <summary>
        /// 产生指定位数的随机数
        /// </summary>
        /// <param name="RandomLevel">位数</param>
        /// <returns></returns>
        public static string GetRandom(int RandomLevel)
        {
            string str="";
            System.Random random = new Random();

            for (int i = 0; i < RandomLevel; i++)
            {
                int num = random.Next(0,9);
                str +=num.ToString();
            }
            return str;
        }
    
	}
}
