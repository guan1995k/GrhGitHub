using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;
using DingKit;
namespace DingKit
{

    /// <summary>
    /// HTML文本处理及动态页面静态化相关方法
    /// </summary>
    public class CHtml
    {

        /// <summary>
        /// 构造
        /// </summary>
        public CHtml()
        {

        }

        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <param   name="Htmlstring">包括HTML的源码   </param>
        ///   <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }



        ///   <summary>
        ///   去除script标记
        ///   </summary>
        ///   <param   name="strHtml">包括HTML的源码   </param>
        ///   <returns>已经去除后的文字</returns>
        public static string StripHTML(string strHtml)
        {
            string[] aryReg =
    {
      @"<script[^>]*?>.*?</script>",
      @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", 
      @"([\r\n])[\s]+",
      @"&(quot|#34);", 
      @"&(amp|#38);", 
      @"&(lt|#60);", 
      @"&(gt|#62);", 
      @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);",
      @"&(copy|#169);", @"&#(\d+);", @"-->", @"<!--.*\n"
    };

            string[] aryRep =
    {
      "", "", "", "\"", "&", "<", ">", "   ", "\xa1",  //chr(161),
      "\xa2",  //chr(162),
      "\xa3",  //chr(163),
      "\xa9",  //chr(169),
      "", "\r\n", ""
    };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }


        ///   <summary>
        ///   移除HTML标签
        ///   </summary>
        ///   <param   name="HTMLStr">HTMLStr</param>
        ///  <returns>已经去除后的文字</returns>
        public static string ParseTags(string HTMLStr)
        {
            return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
        }



        ///   <summary>
        ///   移除HTML标签（包含HTML及JS）
        ///   </summary>
        ///   <param   name="HTMLStr">HTMLStr</param>
        ///   <returns>已经去除后的文字</returns>
        public static string DelHTML(string HTMLStr)
        {
            return ParseTags(NoHTML(StripHTML(HTMLStr)));
        }


        ///   <summary>
        ///   取出文本中的图片地址
        ///   </summary>
        ///   <param   name="HTMLStr">HTMLStr</param>
        ///   <returns>图片路径</returns>
        public static string GetImgUrl(string HTMLStr)
        {
            string str = string.Empty;
            //string sPattern = @"^<img\s+[^>]*>";
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>",
              RegexOptions.Compiled);
            Match m = r.Match(HTMLStr.ToLower());
            if (m.Success)
                str = m.Result("${url}");
            return str;
        }


        /// <summary>
        /// 获取HTML文件，如果不存在，则动态生成HTML文件
        /// </summary>
        /// <param name="mPage">当前mPage</param>
        /// <param name="AspxFile">Aspx文件名</param>
        /// <param name="HtmlFile">待生成的HTML</param>
        /// <returns></returns>
        public static string GetPageHtml(System.Web.UI.Page mPage, string AspxFile, string HtmlFile)
        {

            string strContent = "";
            string file = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "/" + HtmlFile;
            if (File.Exists(file) == false)
            {
                StringWriter sw = new StringWriter();
                mPage.Server.Execute(AspxFile, sw);
                strContent = sw.ToString();
                CFile.CreateFile(HtmlFile, strContent);
            }
            else
            {
                strContent = CFile.GetFileContent(HtmlFile);
            }

            return strContent;
        }

        /// <summary>
        /// 获取HTML文件，如果不存在，则动态生成HTML文件,并判断文件有效时间，过期则重新生成
        /// </summary>
        /// <param name="mPage"></param>
        /// <param name="AspxFile"></param>
        /// <param name="HtmlFile"></param>
        /// <param name="Hour"></param>
        /// <returns></returns>
        public static string GetPageHtml(System.Web.UI.Page mPage, string AspxFile, string HtmlFile, int Hour)
        {

            string strContent = "";
            string file = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "/" + HtmlFile;
            if (File.Exists(file) == false)
            {
                StringWriter sw = new StringWriter();
                mPage.Server.Execute(AspxFile, sw);
                strContent = sw.ToString();
                CFile.CreateFile(HtmlFile, strContent);
            }
            else
            {
                if (CFile.IsFileExpired(file, Hour) == false)
                {
                    strContent = CFile.GetFileContent(HtmlFile);
                }
                else
                {
                    StringWriter sw = new StringWriter();
                    mPage.Server.Execute(AspxFile, sw);
                    strContent = sw.ToString();
                    CFile.CreateFile(HtmlFile, strContent);
                }
            }

            return strContent;
        }



    }
}