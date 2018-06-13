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
	/// ������Ҫʵ��һЩ������������
	/// </summary>
	public class CFun
	{
        /// <summary>
        /// Ĭ����ʾͼƬ
        /// </summary>
        public static string DefaultPic = CConfig.GetValueByKey("DefaultPic");


       /// <summary>
       /// ������ʽ
       /// </summary>
        public enum CERR_REPORT_TYPE:int
        {
            /// <summary>
            /// �����
            /// </summary>
            ALERT=0,
            /// <summary>
            /// ����ҳ�棻
            /// </summary>
            ERRPAGE,
            /// <summary>
            /// ������ת����ҳ�棻
            /// </summary>
            ALERT_ERRPAGE,
            /// <summary>
            /// �����ϼ�ҳ�棻
            /// </summary>
            BAKE,
            /// <summary>
            /// �����󣬷����ϼ�ҳ�棻
            /// </summary>
            ALERT_BAKE
        }

        /// <summary>
        /// ����
        /// </summary>
        public CFun()
		{

		}

       
        /// <summary>
        /// �������-��ֹ�������Ƕ��
        /// </summary>
        /// <param name="Param">��������</param>
        /// <returns>������Ĳ���</returns>
        public static string Quote(string Param)
        {
            if (Param == null || Param.Length == 0)
            {
                return "";
            }
            else
            {
                //��ֹ�������Ƕ��
                string str = Param.Replace("'", "");
                str = str.Replace("--", "");
                return str;
            }
        }

     

        /// <summary>
        /// ͼƬ��ʾ�������Ų����滻ͼƬ��
        /// </summary>
        /// <param name="curPic">��ǰͼƬ��ַ</param>
        /// <param name="ReplacePiC">�滻��ַ</param>
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
        /// ͼƬ��ʾ�������Ų���web.config�ж����Ĭ��ͼƬ��
        /// </summary>
        /// <param name="curPic">��ǰͼƬ��ַ</param>
        /// <param name="ReplacePiC">�滻��ַ</param>
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
        /// �������ֵ�е�����
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
        /// ��ֵ�ͼ��
        /// </summary>
        /// <param name="value">�����ֵ</param>
        /// <returns>�Ƿ���ֵ��</returns>
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
        /// ��ȡָ��QueryString��ֵ
        /// </summary>
        /// <param name="ParamName">ָ��QueryString������</param>
        /// <returns>��Ӧ��ֵ</returns>
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
        /// ���س�����ʾ(��ҪJS���)
        /// </summary>
        public static void HideFailHint()
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>setTimeout('hideFailHint()',8000)</script>");
        }


        /// <summary>
        /// ���سɹ���ʾ(��ҪJS���)
        /// </summary>
        public static void HideSuccHint()
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>setTimeout('hideSuccHint()',8000)</script>");
        }




        /// <summary>
        /// ��Javascript��Window.alert()���������ʾ��Ϣ
        /// </summary>
        /// <param name="strMsg">Ҫ�������Ϣ</param>
        /// <param name="Back">�����Ϣ���Ƿ�Ҫhistory.back()</param>
        /// <param name="End">�����Ϣ���Ƿ�ҪResponse.end()</param>
        public static void WriteMessage(string strMsg, bool Back, bool End)
        {
       
            //���������滻����ֹjavascript����
            strMsg = strMsg.Replace("'", "��");
            //���س����з�ȥ������ֹjavascript����
            strMsg = strMsg.Replace("\r\n", "");
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>alert('" + strMsg + "');</script>");
            if (Back)
                System.Web.HttpContext.Current.Response.Write("<script language=javascript>history.back();</script>");
            if (End)
                System.Web.HttpContext.Current.Response.End();
        }


        /// <summary>
        /// ����ʱ������ʾ�Ի���
        /// </summary>
        /// <param name="str_Prompt">��ʾ��Ϣ</param>
        public static string JsAlerT(string str_Prompt)
        {
            string str = "<script language=\"javascript\">alert('" + str_Prompt + "');</" + "script>";
            System.Web.HttpContext.Current.Response.Write(str);
            return str;
        }

        /// <summary>
        /// ��Response.Write()��ʽ���һ��JavaScript���
        /// </summary>
        /// <param name="strJavaScript">Ҫ�����JavaScript���</param>
        public static void WriteJavaScript(string strJavaScript)
        {
            System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">" + strJavaScript + "</script>");
        }


        /// <summary>
        /// ��Response.Write()��ʽ���һ��JavaScript���
        /// </summary>
        /// <param name="strJavaScript">Ҫ�����JavaScript���</param>
        public static string  FortmatJavaScript(string strJavaScript)
        {
            return "<script type=\"text/javascript\">" + strJavaScript + "</script>";
        }




        /// <summary>
        /// �ַ����������
        /// </summary>
        /// <param name="attr">���ݿ��Ӧ�ֶ���</param>
        /// <param name="mvalue">���ݿ��Ӧ�ֶ���ֵ</param>
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
        /// �ַ����������
        /// </summary>
        /// <param name="attr">���ݿ��Ӧ�ֶ���</param>
        /// <param name="mvalue">���ݿ��Ӧ�ֶ���ֵ</param>
        /// <param name="isstring">���ݿ��Ӧ�Ƿ����ַ���</param>
        /// <returns>������ź���ַ���</returns>
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
        /// �ַ����������
        /// </summary>
        /// <param name="attr">���ݿ��Ӧ�ֶ���</param>
        /// <param name="mvalue">���ݿ��Ӧ�ֶ���ֵ</param>
        /// <param name="isstring">���ݿ��Ӧ�Ƿ����ַ���</param>
        /// <param name="islast">�Ƿ��������</param>
        /// <returns>������ź���ַ���</returns>
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
        /// �ַ����������
        /// </summary>
        /// <param name="attr">���ݿ��Ӧ�ֶ���</param>
        /// <param name="mvalue">���ݿ��Ӧ�ֶ���ֵ</param>
        public static string FormatUpdate(string attr, string mvalue)
        {
            mvalue = CFun.CleanString(mvalue);
            return attr + "='" + mvalue + "' , ";

        }

        /// <summary>
        /// �ַ����������
        /// </summary>
        /// <param name="attr">���ݿ��Ӧ�ֶ���</param>
        /// <param name="mvalue">���ݿ��Ӧ�ֶ���ֵ</param>
        /// <param name="isstring">���ݿ��Ӧ�Ƿ����ַ���</param>
        /// <returns>������ź���ַ���</returns>
        public static string FormatUpdate(string attr, string mvalue, bool isstring)
        {
            mvalue = CFun.CleanString(mvalue);
            if (isstring == true)
                return attr + "='" + mvalue + "',";
            else
                return attr + "=" + mvalue + " , ";
        }


        /// <summary>
        /// �ַ����������
        /// </summary>
        /// <param name="attr">���ݿ��Ӧ�ֶ���</param>
        /// <param name="mvalue">���ݿ��Ӧ�ֶ���ֵ</param>
        /// <param name="isstring">���ݿ��Ӧ�Ƿ����ַ���</param>
        /// <param name="islast">�Ƿ��������</param>
        /// <returns>������ź���ַ���</returns>
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
        /// ����SQL�������ϳ�
        /// </summary>
        /// <param name="str">�ַ����������</param>
        /// <returns>������ź���ַ���</returns>
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
        /// ����SQL�������ϳ�,�ַ����������
        /// </summary>
        /// <param name="str">����������ַ���</param>
        /// <param name="isstring">���ݿ��Ӧ�Ƿ����ַ���</param>
        /// <returns>������ź���ַ���</returns>
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
        /// ����SQL�������ϳ�,�ַ����������
        /// </summary>
        /// <param name="str">����������ַ���</param>
        /// <param name="isstring">���ݿ��Ӧ�Ƿ����ַ���</param>
        /// <param name="islast">�Ƿ���SQL���������</param>
        /// <returns>������ź���ַ���</returns>
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
        /// HTMLEncodeת��
        /// </summary>
        /// <param name="str">�ַ�</param>
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
        /// <returns>�ַ�</returns>
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
        /// �ı�����ɫ
        /// </summary>
        /// <param name="str">�ı�</param>
        /// <param name="ColorName">��ɫ</param>
        /// <returns>�ӹ���ɫ���ı�</returns>
        public static string WithColor(string str,string ColorName)
        {
            return "<font color='" + ColorName + "'>" + str + "</font>";
        }
        

        /// <summary>
        /// �ַ����Ƿ����
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="subStr">�Ӵ�</param>
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
       ///��ȡ��ҳ���ƣ�����չ������url������
       /// </summary>
       /// <param name="Url">URL��ַ</param>
       /// <param name="HaveSubDir">�Ƿ�����ϼ�Ŀ¼</param>
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
        /// ��ȡ��ҳ���ƣ���������չ����
        /// </summary>
        /// <param name="pageName">��ҳ����</param>
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
        /// ��ȡ��ҳ���ƣ�������չ����url������
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
        /// �ַ���Left��ȡ
        /// </summary>
        /// <param name="sSource">�ַ���</param>
        /// <param name="iLength">����</param>
        /// <returns>��ȡ��</returns>
		public static string Left(string   sSource,   int   iLength)   
		{   
			sSource = sSource.Trim();
			return sSource.Substring(0,iLength > sSource.Length?sSource.Length:iLength);   
		}

        /// <summary>
        /// �ַ������ƽ�ȡ�������ӡ���
        /// </summary>
        /// <param name="sSource">�ַ���</param>
        /// <param name="iLength">����</param>
        /// <returns>��ȡ��</returns>
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
                sSource = sSource+"��";
                return sSource;
            }
        }

        /// <summary>
        /// HTML�����ᴿ�������ӡ���
        /// </summary>
        /// <param name="sSource">HTML�ַ���</param>
        /// <param name="iLength">��ȡ����</param>
        /// <returns></returns>
        public string LimitCutHTML(string sSource, int iLength)
        {
            return CFun.LimitCut(CHtml.DelHTML(sSource), iLength);
        }


        /// <summary>
        /// �ַ���Right��ȡ
        /// </summary>
        /// <param name="sSource">�ַ���</param>
        /// <param name="iLength">����</param>
        /// <returns>��ȡ��</returns>
		public static string Right(string sSource,int iLength)   
		{   
			return sSource.Substring(iLength>sSource.Length?0 : sSource.Length - iLength);   
		}   

        /// <summary>
        /// �ַ���A�а����ַ���B
        /// </summary>
        /// <param name="a">ĸ���ַ���</param>
        /// <param name="b">�������ַ���</param>
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
        /// �ַ���Mid��ȡ
        /// </summary>
        /// <param name="sSource">�ַ���</param>
        /// <param name="iStart">��ʼλ��</param>
        /// <param name="iLength">��ֹλ��</param>
        /// <returns>��ȡ��</returns>
		public static string Mid(string sSource, int iStart,int iLength)   
		{   
			int iStartPoint = iStart>sSource.Length ? sSource.Length : iStart;   
			return  sSource.Substring(iStartPoint,iStartPoint + iLength> sSource.Length ? sSource.Length - iStartPoint : iLength);   
		}


        /// <summary>
        /// HTML�����ᴿ
        /// </summary>
        /// <param name="sSource">HTML�ַ���</param>
        /// <param name="iLength">��ȡ����</param>
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
        /// �ַ�����ȡ
        /// </summary>
        /// <param name="sSource">�ַ���</param>
        /// <param name="iLength">����</param>
        /// <returns>��ȡ��</returns>
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
        /// �ַ�����,md5,32λ
      /// </summary>
      /// <param name="Password">�������ֶ�</param>
      /// <returns>���ܺ�</returns>
		public static string Encrypt(string Password)
		{
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "md5");
  
		}

        /// <summary>
        /// �ַ�����,md5,16λ
        /// </summary>
        /// <param name="ConvertString">�������ֶ�</param>
        /// <returns>���ܺ�</returns>
        public static string GetMd5Str(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }





		/// <summary>
        /// �ַ�����(0��SHA1,1�� MD5)
		/// </summary>
        /// <param name="Password">�������ֶ�</param>
		/// <param name="Format">���ܸ�ʽ,0��SHA1,1�� MD5</param>
		/// <returns>���ܺ�</returns>
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
        /// �ַ�����
		/// </summary>
        /// <param name="Passowrd">�������ֶ�</param>
		/// <returns></returns>
		public static string Decrypt(string Passowrd)
		{
			string str="";
			str= System.Web.Security.FormsAuthentication.Decrypt(Passowrd).Name.ToString();
			return str;
		}

        /// <summary>
        ///  Cookie����
        /// </summary>
        /// <param name="strCookie">������cookie</param>
        /// <param name="type"> �������ͣ�������DeTransform2��ż����DeTransform1</param>
        /// <returns>���ܺ�cookie</returns>
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
        ///  Cookie����
        /// </summary>
        /// <param name="strCookie">������cookie</param>
        /// <param name="type"> �������ͣ�������DeTransform2��ż����DeTransform1</param>
        /// <returns>���ܺ�cookie</returns>
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
        /// ����
        /// </summary>
        /// <param name="strCookie"> ����Cookie</param>
        /// <param name="type"> �������ͣ�������DeTransform2��ż����DeTransform1</param>
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
		/// ����
		/// </summary>
        /// <param name="strCookie">����Cookie</param>
        /// <param name="type">�������ͣ�������DeTransform2��ż����DeTransform1</param>
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
		/// ���ܷ���1
		/// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
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
        /// ���ܷ���1
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
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
        /// ת������2
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>ת������ַ���</returns>
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
        /// ���ܷ���3
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
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
        /// ��ȡ��ǰ���ڵ��ܴ�
        /// </summary>
        /// <param name="dt">����</param>
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
        /// ���ܷ���3
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
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
        /// ����SQLע��
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CleanString(string inputString)
        {
            return inputString;
            //StringBuilder retVal = new StringBuilder();

            ////sqlע��
            //if (CFun.SqlFilter(inputString) == true)
            //{
            //    return "";
            //}

            //if ((inputString != null) && (inputString != String.Empty))
            //{
            //    inputString = inputString.Trim();
            //    if (CFun.IsInclude(inputString.ToLower(), "��>"))
            //    {
            //        inputString = CFun.Right(inputString, inputString.IndexOf("��>"));
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
		/// �ַ�������
		/// </summary>
		/// <param name="str">�ַ���</param>
        /// <returns>������ַ���</returns>
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
        ///// ��ȡ�ͻ���IP
        ///// </summary>
        ///// <param name="page">ҳ��</param>
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
        /// ��ȡ����IP 
        /// </summary> 
        /// <returns>����IP</returns> 
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
        /// �ַ����ؼ��ּӺ�
        /// </summary>
        /// <param name="str">������</param>
        /// <param name="key">�ؼ���</param>
        /// <param name="size">�����С</param>
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

            sArray = key.Split('��');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split(';');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split('��');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            return str.Replace(key, sty + key + "</span>");
        }

         /// <summary>
        /// �ַ����ؼ��ּӺ�
        /// </summary>
        /// <param name="str">������</param>
        /// <param name="key">�ؼ���</param>
        /// <returns></returns>
        public static string ToRedFontSingle(string str)
        {
            string sty = "<span style='color:#FF0000' >";
            return sty + str + "</span>";
        }
        /// <summary>
        /// �ַ����ؼ��ּӺ�
        /// </summary>
        /// <param name="str">������</param>
        /// <param name="key">�ؼ���</param>
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

            sArray = key.Split('��');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split(';');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            sArray = key.Split('��');
            foreach (string i in sArray)
            {
                str = str.Replace(i.ToString(), sty + i.ToString() + "</span>");
            }

            return str.Replace(key, sty + key + "</span>");
        }


        /// <summary>
        /// �����Ƿ���Ч
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }





        /// <summary>
        /// ���ڼ������
        /// </summary>
        /// <param name="Date1">��ʼ����</param>
        /// <param name="Date2">��������</param>
        /// <param name="Interval">����(��y,��M,��D,ʱh,��m,��s,΢��ms)</param>
        /// <returns></returns>
        public static int DateDiff(System.DateTime Date1, System.DateTime Date2, string Interval)
        {
            double dblYearLen = 365;//��ĳ��ȣ�365��   
            double dblMonthLen = (365 / 12);//ÿ����ƽ��������   
            System.TimeSpan objT;
            objT = Date2.Subtract(Date1);
            switch (Interval)
            {
                case "y"://�������ڵ���ݼ��   
                    return System.Convert.ToInt32(objT.Days / dblYearLen);
                case "M"://�������ڵ��·ݼ��   
                    return System.Convert.ToInt32(objT.Days / dblMonthLen);
                case "d"://�������ڵ��������   
                    return objT.Days;
                case "h"://�������ڵ�Сʱ���   
                    return objT.Hours;
                case "m"://�������ڵķ��Ӽ��   
                    return objT.Minutes;
                case "s"://�������ڵ����Ӽ��   
                    return objT.Seconds;
                case "ms"://����ʱ���΢����   
                    return objT.Milliseconds;
                default:
                    break;
            }
            return 0;
        }


        /// <summary> 
        ///SQLע����� 
        /// </summary> 
        /// <param name="InText">Ҫ���˵��ַ���</param> 
        /// <returns>����������ڲ���ȫ�ַ����򷵻�true</returns> 
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
        /// �ж�Session��ĳ��ֵ�Ƿ���ڣ���������ڣ������ʾ��Ϣ
        /// ����ת����Ӧҳ��
        /// </summary>
        /// <param name="SessionName">Session�м�����</param>
        /// <param name="Message">��ʾ��Ϣ</param>
        /// <param name="RedirectUrl">Ҫת���ҳ��</param>
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
        /// �ж�Session��ĳ��ֵ�Ƿ���ڣ���������ڣ������ʾ��Ϣ
        /// ����ת����Ӧҳ��
        /// </summary>
        /// <param name="CookieName">Cookie�м�����</param>
        /// <param name="Message">��ʾ��Ϣ</param>
        /// <param name="RedirectUrl">Ҫת���ҳ��</param>
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
        /// ��ʾ����תҳ��
        /// </summary>
        /// <param name="strMsg">��ʾ��Ϣ</param>
        /// <param name="strUrl">URL</param>
        public static void JsAlerT(string strMsg, string strUrl)
        {
            string str = "<script language=\"javascript\">alert('" + strMsg + "');</" + "script>";
            System.Web.HttpContext.Current.Response.Write(str);
            str = "<script language=javascript>window.location.href='" + strUrl + "';</script>";
            System.Web.HttpContext.Current.Response.Write(str);
        } 


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strMsg">������Ϣ</param>
        /// <param name="RedirectUrl">�ض�����ַ</param>
        /// <param name="type">���󱨸�����</param>
        public static void ReportError(string strMsg, string RedirectUrl, CERR_REPORT_TYPE type)
        {
            //���������滻����ֹjavascript����
            strMsg = strMsg.Replace("'", "��");
            //���س����з�ȥ������ֹjavascript����
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
        /// ����������ת������������
        /// </summary>
        /// <param name="strArabicNum"></param>
        /// <returns></returns>
        public static string ArabicNumToChinaNum(string strArabicNum)
        {
            switch (strArabicNum)
            {
                case "1":
                    return "һ";
              
                case "2":
                    return "��";
           
                case "3":
                    return "��";
                  
                case "4":
                    return "��";
                   
                case "5":
                    return "��";
                   
                case "6":
                    return "��";
                  
                case "7":
                    return "��";

                case "8":
                    return "��";
                   
                case "9":
                    return "��";
                   
                default:
                    return "";
                   
            }
        }
     

        /// <summary>
        /// ��������ת���ɰ���������
        /// </summary>
        /// <param name="strChinaNum"></param>
        /// <returns></returns>
        public static string ChinaNumToArabicNum(string strChinaNum)
        {
            switch (strChinaNum)
            {
                case "һ":
                    return "1";

                case "��":
                    return "2";
                   
                case "��":
                    return "3";
                  
                case "��":
                    return "4";
                   
                case "��":
                    return "5";
                    
                case "��":
                    return "6";
                
                case "��":
                    return "7";
                  
                case "��":
                    return "8";
                  
                case "��":
                    return "9";
                  
                default:
                    return "";
                   
            }
        }


     
        /// <summary>
        /// ����16λ�������
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
        /// ����16λ�������
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
        /// ����10λ�����
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
        /// ���תȫ��
        /// </summary>
        /// <param name="strInput">��Ҫת�����ַ���</param>
        /// <param name="strInStr">Ҫ���а��ת����ȫ�ǵ��ַ��б�</param>
        /// <returns>ת������ַ���</returns>
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
        /// ��ȡĳһ�����Ǹ����еĵڼ���
        /// </summary>
        /// <param name="dt">����</param>
        /// <returns>�������ڸ����е�����</returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            return gc.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }


        /// <summary>
        /// ��̬���ɷ�ҳ��
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
                sbTable.Append("<a href='" + strHref + "1'>��ҳ</a>");
                sbTable.Append("<a href='" + strHref + Convert.ToString(CurrentPage - 1) + "'>��һҳ</a>");
            }
            else
            {
                sbTable.Append("<a>��ҳ</a>");
                sbTable.Append("<a>��һҳ</a>");
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
                sbTable.Append("<a href='" + strHref + PageCount.ToString() + "'>��ҳ</a>");
                sbTable.Append("<a href='" + strHref + Convert.ToString(CurrentPage + 1) + "'>��һҳ</a>");
            }
            else
            {
                sbTable.Append("<a>��һҳ</a>");
                sbTable.Append("<a>βҳ</a>");
            }
            sbTable.Append("</td>");
            sbTable.Append("</tr>");
            sbTable.Append("</table>");
            return sbTable.ToString();
        } 


        /// <summary>
        /// ����ָ��λ���������
        /// </summary>
        /// <param name="RandomLevel">λ��</param>
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
