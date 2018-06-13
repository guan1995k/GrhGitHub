using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace DingKit
{
    /// <summary>
    /// CConfig文件操作
    /// </summary>
    public class CConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CConfig()
        {
  
        }

        /// <summary>
        /// 读取WEB.config中的值
        /// </summary>
        /// <param name="Key">key</param>
        /// <returns></returns>
        public static String GetValueByKey(String Key)
        {
            return  WebConfigurationManager.AppSettings[Key];
            //return ConfigurationSettings.AppSettings[Key];
        }
    }
}
