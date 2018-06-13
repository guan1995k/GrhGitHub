using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace GuanMethod
{
    public class CApi
    {
        //  
        #region Http接口
        /// <summary>
        /// 通过http接口的url获取内容
        /// </summary>
        /// <param name="url">http接口的路径</param>
        /// <returns>返回object型数据</returns>
        public object GetHttpUrlString(string url)
        {
            WebRequest WRequest = WebRequest.Create(url);
            WRequest.Method = "GET";
            WRequest.ContentType = "text/html;charset=UTF-8";
            WebResponse WResponse = WRequest.GetResponse();
            Stream stream = WResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            object str = reader.ReadToEnd();
            return str;
        }
        #endregion

        #region 

        #endregion
    }
}
