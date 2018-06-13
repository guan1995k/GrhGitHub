using System;
using System.Text;
using System.Data;
using System.IO;
using System.Web;

namespace DingKit
{

    /// <summary>
    /// 用于对Cookie、Session、Application的存储访问方法
    /// </summary>
    public class CCookie
    {
        private const string DOMAIN = "ESS2011";
        /// <summary>
        /// 构造函数
        /// </summary>
        public CCookie()
        {
   
        }

        #region 操作Cookie的方法
       
        /// <summary>
        /// 保存Cook
        /// </summary>
        /// <param name="Cname">名称</param>
        /// <param name="Cvaule">值</param>
        public static void SaveCookie(string Cname, string Cvaule)
        {
            Cname = Cname.ToUpper();
            HttpBrowserCapabilities sbText = HttpContext.Current.Request.Browser;
            string IsSupport = sbText.Cookies.ToString();
            Cvaule = HttpContext.Current.Server.UrlEncode(Cvaule);
            //判断浏览器是否支持Cookie，支持用Cookie，否则用Session
            if (IsSupport.ToLower() == "true")
            {
                HttpContext.Current.Response.Cookies[DOMAIN][Cname] = Cvaule;
                HttpContext.Current.Response.Cookies[DOMAIN].Expires = DateTime.Now.AddDays(1);//有效期1天
            }
            else
            {
                HttpContext.Current.Session[Cname] = Cvaule;
                HttpContext.Current.Session.Timeout = 720;//有效期12小时
            }
        }


        /// <summary>
        /// 保存Cook
        /// </summary>
        /// <param name="Cname">名称</param>
        /// <param name="Cvaule">值</param>
        /// <param name="Hours">有效时间</param>
        public static void SaveCookie(string Cname, string Cvaule, int Hours)
        {
            Cname = Cname.ToUpper();
            HttpBrowserCapabilities sbText = HttpContext.Current.Request.Browser;
        
            string IsSupport = sbText.Cookies.ToString();
   
            Cvaule = HttpContext.Current.Server.UrlEncode(Cvaule);
            //判断浏览器是否支持Cookie，支持用Cookie，否则用Session
            if (IsSupport.ToLower() == "true")
            {
                HttpContext.Current.Response.Cookies[DOMAIN][Cname] = Cvaule;
                HttpContext.Current.Response.Cookies[DOMAIN].Expires = DateTime.Now.AddHours(Hours);//有效期1天
            }
            else
            {
                HttpContext.Current.Session[Cname] = Cvaule;
                HttpContext.Current.Session.Timeout = 60 * Hours;//有效期12小时
            }
        }


        /// <summary>
        /// 保存Session
        /// </summary>
        /// <param name="Cname">名称</param>
        /// <param name="Cvaule">值</param>
        public static void SaveSession(string Cname, string Cvaule)
        {
            Cname = Cname.ToUpper();
            HttpContext.Current.Session[Cname] = Cvaule;
            HttpContext.Current.Session.Timeout = 25;//有效期25小时
        }

        /// <summary>
        /// 保存Session
        /// </summary>
        /// <param name="Cname">名称</param>
        /// <param name="Cvaule">值</param>
        /// <param name="Hours">有效时间</param>
        public static void SaveSession(string Cname, string Cvaule, int Hours)
        {
            Cname = Cname.ToUpper();
            HttpContext.Current.Session[Cname] = Cvaule;
            HttpContext.Current.Session.Timeout = Hours*60;//有效期12小时
        }

        /// <summary>
        /// 保存Application
        /// </summary>
        /// <param name="Cname">名称</param>
        /// <param name="Cvaule">值</param>
        public static void SaveApplication(string Cname, string Cvaule)
        {
            Cname = Cname.ToUpper();
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application[Cname] = Cvaule;
            HttpContext.Current.Application.UnLock();
        }




        /// <summary>
        /// 读取Application值
        /// </summary>
        /// <param name="Cname">名称</param>
        /// <returns>Application值(无值为空字符)</returns>
        public static string PrintApplication(string Cname)
        {
            Cname = Cname.ToUpper();
            if (HttpContext.Current.Application[Cname] == null)
            {
                return "";
            }
            else
            {
                return (string)HttpContext.Current.Application[Cname];
            }
        }


        /// <summary>
        /// 读取Session值
        /// </summary>
        /// <param name="Cname">名称</param>
        /// <returns>Session值(无值为空字符)</returns>
        public static string PrintSession(string Cname)
        {
            Cname = Cname.ToUpper();
            if (HttpContext.Current.Session[Cname] == null)
            {
                return "";
            }
            else
            {
                return (string)HttpContext.Current.Session[Cname];
            }
        }


        /// <summary>
        /// 读取Cookie值
        /// </summary>
        /// <param name="Cname">名称</param>
        /// <returns>Cookie值(无值为空字符)</returns>
        public static string PrintCookie(string Cname)
        {
            Cname = Cname.ToUpper();
            string strReturn = "";
            HttpBrowserCapabilities sbText = HttpContext.Current.Request.Browser;
            string IsSupport = sbText.Cookies.ToString();
            //判断浏览器是否支持Cookie，支持用Cookie，否则用Session
            if (IsSupport.ToLower() == "true")
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[DOMAIN];
                if (HttpContext.Current.Request.Cookies[DOMAIN] == null)
                {
                    strReturn = "";
                }
                else
                {
                    if (null == HttpContext.Current.Request.Cookies[DOMAIN][Cname])
                    {
                        strReturn = "";
                    }
                    else
                    {
                        strReturn = HttpContext.Current.Request.Cookies[DOMAIN][Cname].ToString();
                    }
                }
            }
            else
            {
                if (HttpContext.Current.Session[Cname] == null)
                {
                    strReturn = "";
                }
                else
                {
                    strReturn = (string)HttpContext.Current.Session[Cname];
                }
            }
            if (strReturn != "")
            {
                strReturn = HttpContext.Current.Server.UrlDecode(strReturn);
            }

            return strReturn;
        }





        /// <summary>
        /// 清空Cook
        /// </summary>
        public static void CleanCookiesAll()
        {
            string cookieName;
            int limit = HttpContext.Current.Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = HttpContext.Current.Request.Cookies[i].Name;
                HttpContext.Current.Response.Cookies[DOMAIN][cookieName] = "";
                HttpContext.Current.Response.Cookies[DOMAIN].Expires = DateTime.Now.AddDays(-1);
            }
        }

        /// <summary>
        /// 清空Cook
        /// </summary>
        /// <param name="Cname"></param>
        public static void CleanCookies(string Cname)
        {
            Cname = Cname.ToUpper();
            HttpContext.Current.Response.Cookies[DOMAIN][Cname] = "";
            HttpContext.Current.Response.Cookies[DOMAIN].Expires = DateTime.Now.AddDays(-1);
        }
       

        /// <summary>
        /// 清空所有Session
        /// </summary>
        public static void CleanSessionALL()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        /// <summary>
        /// 清空session
        /// </summary>
        /// <param name="Cname"></param>
        public static void CleanSession(string Cname)
        {
            Cname = Cname.ToUpper();
            HttpContext.Current.Session.Remove(Cname);
        }

         /// <summary>
        /// 清空所有Application
        /// </summary>
        public static void CleanApplicationAll()
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application.Contents.RemoveAll();
            HttpContext.Current.Application.UnLock();
        }

        /// <summary>
        /// 清空Application
        /// </summary>
        /// <param name="Cname"></param>
        public static void CleanApplication(string Cname)
        {
            Cname = Cname.ToUpper();
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application.Contents.Remove(Cname);
            HttpContext.Current.Application.UnLock();
        }

        #endregion

    }
}



