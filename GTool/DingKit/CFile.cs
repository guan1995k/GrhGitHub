using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text;


namespace DingKit
{

    /// <summary>
    /// 用于文件相关的各类处理方法(需要web.Config中定义File_Exp、File_Size、File_Size_K、UpDir、DefaultPic)
    /// </summary>
    public class CFile
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public static string File_Exp =  CConfig.GetValueByKey("File_Exp");

        /// <summary>
        /// 文件长度
        /// </summary>
        public static string File_Size =  CConfig.GetValueByKey("File_Size");
        /// <summary>
        /// K为单位的文件长度
        /// </summary>
        public static string File_Sizem =   CConfig.GetValueByKey("File_Size_K");
        /// <summary>
        /// 文件上传路径
        /// </summary>
        public static string UpDir =  CConfig.GetValueByKey("UpDir");

        /// <summary>
        /// 默认图片
        /// </summary>
        public static string DefaultPic = CConfig.GetValueByKey("DefaultPic");

        /// <summary>
        /// 路径分割符
        /// </summary>
        private const string PATH_SPLIT_CHAR = "\\";


        /// <summary>
        /// 构造函数
        /// </summary>
        public CFile()
        {

        }

       /// <summary>
       ///判断文件是否存在
       /// </summary>
       /// <param name="path">文件夹绝对路径</param>
       /// <param name="fileName">文件名</param>
       /// <returns>是否存在</returns>
        public static bool isFileExist(string path, string fileName)
        {
            if (!System.IO.Directory.Exists(path))
            {
                return false;
            }
            ArrayList files = new ArrayList();
            GetFiles(path, files);
            //获取当前目录下的文件
            foreach (string file in files)
            {
                if (file == path + "\\" + fileName)
                    return true;
            }
            return false;

        }

        /// <summary>
        /// 删除指定路径下的文件
        /// </summary>
        /// <param name="stfilePath">文件绝对路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string stfilePath)
        {
            File.Delete(stfilePath);
            return true;
        }

        /// <summary>
        /// 根据文件路径获取文件名
        /// </summary>
        /// <param name="stfilePath">文件绝对或者相对路径</param>
        /// <returns>文件名</returns>
        public static string GetFileName(string stfilePath)
        {
            string strTempPath = stfilePath.Replace("/", "，");
            strTempPath = strTempPath.Replace("\\", "，");
            int i = strTempPath.LastIndexOf("，"); 
            //取得文件扩展名
            string fileName = stfilePath.Substring(i + 1);
            return fileName;
        }

        /// <summary>
        /// 根据文件路径获取文件扩张名
        /// </summary>
        /// <param name="stfilePath">文件绝对或者相对路径</param>
        /// <returns>文件扩展名</returns>
        public static string GetFileExp(string stfilePath)
        {
            int i = stfilePath.LastIndexOf(".");
            //取得文件扩展名
            string FileExp = stfilePath.Substring(i + 1);
            return FileExp;
        }


        /// <summary>
        /// 文件类型是否允许
        /// </summary>
        /// <param name="FileExp">文件类型</param>
        /// <returns>是否允许</returns>
        public static bool IsExpAllow(string FileExp)
        {
            string[] Arr_FileExp;
            Char[] split ={ '|', '，' };
            Arr_FileExp = File_Exp.Split(split, 100);
            //获取当前目录下的文件
            foreach (string file in Arr_FileExp)
            {
                if (file.ToLower() == FileExp.ToLower())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 文件大小是否超出
        /// </summary>
        /// <param name="size">文件大小（字节）</param>
        /// <returns></returns>
        public static bool IsSizeAllow(string size)
        {
            if (int.Parse(size) > int.Parse(File_Size))
                return false;
            else
                return true;
        }


        /// <summary>
        /// 获取指定文件夹下的文件列表到数组中
        /// </summary>
        /// <param name="path">文件夹绝对路径</param>
        /// <returns>string型数值</returns>
        public static string[] GetFiles(string path)
        {
            ArrayList files = new ArrayList();
            //获取当前目录下的文件
            foreach (string file in Directory.GetFiles(path))
            {
                files.Add(file);
            }
            //获取当前目录下的子目录文件
            foreach (string dir in Directory.GetDirectories(path))
            {
                GetFiles(dir, files);
            }
            return (string[])files.ToArray(typeof(string));
        }

        /// <summary>
        /// 获取指定文件夹下的文件列表到数组中(递归函数)
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="files">数组</param>
        public static void GetFiles(string path, ArrayList files)
        {
            //获取当前目录下的文件
            foreach (string file in Directory.GetFiles(path))
            {
                files.Add(file);
            }
            //获取当前目录下的子目录文件
            foreach (string dir in Directory.GetDirectories(path))
            {
                GetFiles(dir, files);
            }
        }


       

        /// <summary>
        /// 复制指定目录的所有文件,不包含子目录及子目录中的文件
        /// </summary>
        /// <param name="sourceDir">原始绝对目录</param>
        /// <param name="targetDir">目标绝对目录</param>
        /// <param name="overWrite">如果为true,表示覆盖同名文件,否则不覆盖</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite)
        {
            CopyFiles(sourceDir, targetDir, overWrite, false);
        }

        /// <summary>
        /// 复制指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始绝对目录</param>
        /// <param name="targetDir">目标绝对目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        /// <param name="copySubDir">如果为true,包含目录,否则不包含</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir)
        {
            //复制当前目录文件
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));

                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {

                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Copy(sourceFileName, targetFileName, overWrite);
                    }
                }
                else
                {
                    File.Copy(sourceFileName, targetFileName, overWrite);
                }
            }
            //复制子目录
            if (copySubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    CopyFiles(sourceSubDir, targetSubDir, overWrite, true);
                }
            }
        }

        /// <summary>
        /// 剪切指定目录的所有文件,不包含子目录
        /// </summary>
        /// <param name="sourceDir">原始绝对目录</param>
        /// <param name="targetDir">目标绝对目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite)
        {
            MoveFiles(sourceDir, targetDir, overWrite, false);
        }

        /// <summary>
        /// 剪切指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始绝对目录</param>
        /// <param name="targetDir">目标绝对目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        /// <param name="moveSubDir">如果为true,包含目录,否则不包含</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite, bool moveSubDir)
        {
            //移动当前目录文件
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {

                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Delete(targetFileName);
                        File.Move(sourceFileName, targetFileName);
                    }
                }
                else
                {
                    File.Move(sourceFileName, targetFileName);
                }
            }
            if (moveSubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    MoveFiles(sourceSubDir, targetSubDir, overWrite, true);
                    Directory.Delete(sourceSubDir);
                }
            }
        }

        /// <summary>
        /// 删除指定目录的所有文件，不包含子目录
        /// </summary>
        /// <param name="targetDir">操作绝对目录</param>
        public static void DeleteFiles(string targetDir)
        {
            DeleteFiles(targetDir, false);
        }

        /// <summary>
        /// 删除指定目录的所有文件和子目录
        /// </summary>
        /// <param name="targetDir">操作绝对目录</param>
        /// <param name="delSubDir">如果为true,包含对子目录的操作</param>
        public static void DeleteFiles(string targetDir, bool delSubDir)
        {
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
                File.Delete(fileName);
            }
            if (delSubDir)
            {
                DirectoryInfo dir = new DirectoryInfo(targetDir);
                foreach (DirectoryInfo subDi in dir.GetDirectories())
                {
                    DeleteFiles(subDi.FullName, true);
                    subDi.Delete();
                }
            }

        }

        /// <summary>
        /// 创建指定目录
        /// </summary>
        /// <param name="targetDir">新建绝对目录名</param>
        public static void CreateDirectory(string targetDir)
        {
            DirectoryInfo dir = new DirectoryInfo(targetDir);
            if (!dir.Exists)
                dir.Create();
        }

        /// <summary>
        /// 在特定夫目录下建立子目录
        /// </summary>
        /// <param name="parentDir">目录绝对路径</param>
        /// <param name="subDirName">子目录名称</param>
        public static void CreateDirectory(string parentDir, string subDirName)
        {
            CreateDirectory(parentDir + PATH_SPLIT_CHAR + subDirName);
        }

        /// <summary>
        /// 删除指定目录
        /// </summary>
        /// <param name="targetDir">目录绝对路径</param>
        public static void DeleteDirectory(string targetDir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(targetDir);
            if (dirInfo.Exists)
            {
                DeleteFiles(targetDir, true);
                dirInfo.Delete(true);
            }
        }

        /// <summary>
        /// 删除指定目录的所有子目录,不包括对当前目录文件的删除
        /// </summary>
        /// <param name="targetDir">目录绝对路径</param>
        public static void DeleteSubDirectory(string targetDir)
        {
            foreach (string subDir in Directory.GetDirectories(targetDir))
            {
                DeleteDirectory(subDir);
            }
        }

        /// <summary>
        /// 将指定目录下的子目录和文件生成xml文档
        /// </summary>
        /// <param name="targetDir">绝对根目录</param>
        /// <returns>返回XmlDocument对象</returns>
        public static XmlDocument CreateXml(string targetDir)
        {
            XmlDocument myDocument = new XmlDocument();
            XmlDeclaration declaration = myDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            myDocument.AppendChild(declaration);
            XmlElement rootElement = myDocument.CreateElement(targetDir.Substring(targetDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
            myDocument.AppendChild(rootElement);
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("File");
                childElement.InnerText = fileName.Substring(fileName.LastIndexOf(PATH_SPLIT_CHAR) + 1);
                rootElement.AppendChild(childElement);
            }
            foreach (string directory in Directory.GetDirectories(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("Directory");
                childElement.SetAttribute("Name", directory.Substring(directory.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                rootElement.AppendChild(childElement);
                CreateBranch(directory, childElement, myDocument);
            }
            return myDocument;
        }

        /// <summary>
        ///新建文件
        /// </summary>
        /// <param name="filePath">文件虚拟目录+文件名</param>
        /// <param name="FileContent">文件内容</param>
        public static void CreateFile(string filePath,string FileContent)
        {
            string file = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "/" + filePath;
            if (File.Exists(file))
            {
               CFile.DeleteFile(file);
            }

            StreamWriter sw = new StreamWriter(file, false, Encoding.Default); //GetEncoding("gb2312")
            sw.WriteLine(FileContent);
            sw.Close();
            sw.Dispose();
        }


        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件虚拟目录+文件名</param>
        /// <returns>文件内容</returns>
        public static string GetFileContent(string filePath)
        {
            string strContent=""; 
            string file = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "/" + filePath;
            if (File.Exists(file))
            {
                //FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(file,Encoding.Default);
                strContent = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }

            return strContent;
           
        }
        /// <summary>
        /// 文件是否存在（虚拟路径）
        /// </summary>
        /// <param name="filePath">文件虚拟目录+文件名</param>
        /// <returns></returns>
        public static bool isFileExistByFile(string filePath)
        {
            string file = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "/" + filePath;
            if (File.Exists(file))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 文件是否存在（物理路径）
        /// </summary>
        /// <param name="strFilePath">文件物理路径</param>
        /// <returns></returns>
        public static bool isFileExist(string strFilePath)
        {
            if (File.Exists(strFilePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 生成Xml分支
        /// </summary>
        /// <param name="targetDir">子目录</param>
        /// <param name="xmlNode">父目录XmlDocument</param>
        /// <param name="myDocument">XmlDocument对象</param>
        private static void CreateBranch(string targetDir, XmlElement xmlNode, XmlDocument myDocument)
        {
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("File");
                childElement.InnerText = fileName.Substring(fileName.LastIndexOf(PATH_SPLIT_CHAR) + 1);
                xmlNode.AppendChild(childElement);
            }
            foreach (string directory in Directory.GetDirectories(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("Directory");
                childElement.SetAttribute("Name", directory.Substring(directory.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                xmlNode.AppendChild(childElement);
                CreateBranch(directory, childElement, myDocument);
            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="File2">HtmlInputFile控件</param>
        /// <returns>上传结果</returns>
        public static string FileUp(System.Web.UI.HtmlControls.HtmlInputFile File2)
        {
            if (File2.PostedFile.ContentLength.ToString() == "0")
            {
                return "上传失败或指定的文件不存在";
            }
            else
            {
                //获取文件名称
                string ss;
                ss =  Path.GetExtension(File2.PostedFile.FileName);
                
                if (IsSizeAllow(File2.PostedFile.ContentLength.ToString())== false)
                { 
                    return "您的文件过大，不能上传！"; 
                }
                else
                {
                    string ty = GetFileExp(File2.PostedFile.FileName);
                    if (IsExpAllow(ty)==true)
                    {
                        File2.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(CFile.UpDir) + ss);
                        return ss;
                    }
                    else
                    { 
                        return "限制上传！"; 
                    }
                }
            }
        }

        /// <summary>
        /// 文件上传，自定义上传路径
        /// </summary>
        /// <param name="File2">HtmlInputFile控件</param>
        /// <param name="SelfUpDir">自定义上传路径（相对）</param>
        /// <returns>上传结果</returns>
        public static string FileUp(System.Web.UI.HtmlControls.HtmlInputFile File2, string SelfUpDir)
        {
            if (File2.PostedFile.ContentLength.ToString() == "0")
            {
                return "上传失败或指定的文件不存在";
            }
            else
            {
                //获取文件名称
                string ss;
                ss = Path.GetExtension(File2.PostedFile.FileName);

                if (IsSizeAllow(File2.PostedFile.ContentLength.ToString()) == false)
                {
                    return "您的文件过大，不能上传！";
                }
                else
                {
                    string ty = GetFileExp(File2.PostedFile.FileName);
                    if (IsExpAllow(ty) == true)
                    {
                        File2.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(SelfUpDir) + ss);
                        return ss;
                    }
                    else
                    {
                        return "限制上传！";
                    }
                }
            }
        }


        /// <summary>
        /// 文件上传，自定义上传路径（变更文件名称）
        /// </summary>
        /// <param name="File2">HtmlInputFile控件</param>
        /// <param name="SelfUpDir">自定义上传路径（相对）</param>
        /// <returns>上传结果</returns>
        public static string FileUpChangeName(System.Web.UI.HtmlControls.HtmlInputFile File2, string SelfUpDir)
        {
            if (File2.PostedFile.ContentLength.ToString() == "0")
            {
                return "上传失败或指定的文件不存在";
            }
            else
            {
                //获取文件名称
                string ss;
                ss =Path.GetExtension(File2.PostedFile.FileName)+ System.DateTime.Now.ToString().Replace("-", "").Replace(" ", "").Replace(":", "") ;

                if (IsSizeAllow(File2.PostedFile.ContentLength.ToString()) == false)
                {
                    return "您的文件过大，不能上传！";
                }
                else
                {
                    string ty = GetFileExp(File2.PostedFile.FileName);
                    if (IsExpAllow(ty) == true)
                    {
                        File2.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(SelfUpDir) + ss);
                        return ss;
                    }
                    else
                    {
                        return "限制上传！";
                    }
                }
            }
        }



        /// <summary>
        /// 文件上传（变更文件名称）
        /// </summary>
        /// <param name="File2">HtmlInputFile控件</param>
        /// <returns>上传结果</returns>
        public static string FileUpChangeName(System.Web.UI.HtmlControls.HtmlInputFile File2)
        {
            if (File2.PostedFile.ContentLength.ToString() == "0")
            {
                return "上传失败或指定的文件不存在";
            }
            else
            {
                //获取文件名称
                string ss;
                ss = Path.GetExtension(File2.PostedFile.FileName) + System.DateTime.Now.ToString().Replace("-", "").Replace(" ", "").Replace(":", "");

                if (IsSizeAllow(File2.PostedFile.ContentLength.ToString()) == false)
                {
                    return "您的文件过大，不能上传！";
                }
                else
                {
                    string ty = GetFileExp(File2.PostedFile.FileName);
                    if (IsExpAllow(ty) == true)
                    {
                        File2.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(CFile.UpDir) + ss);
                        return ss;
                    }
                    else
                    {
                        return "限制上传！";
                    }
                }
            }
        }


        /// <summary>
        /// 文件下载(文件推送型)
        /// </summary>
        /// <param name="filename">文件名含相对路径</param>
      /// <param name="isFullPath">是否物理地址1是 0 不是</param>
        public static void FileDown(string filename,string isFullPath)
        {
           if (filename != "")
            {
                if (isFullPath == "0")
                {
                    if (CFun.Left(filename, 1) == "/")
                    {
                        filename = filename.Replace("/", "\\");
                    }
                    if (CFun.Left(filename, 1) == @"\")
                    {
                        filename = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + CFun.Right(filename, filename.Length - 1);
                    }
                    else
                    {
                        filename = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + filename;
                    }
                }

                System.IO.FileInfo file = new System.IO.FileInfo(filename);

                if (file.Exists)
                {
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.Filter.Close();
                    System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
                    System.Web.HttpContext.Current.Response.End();
                }
                else
                {
                    CFun.JsAlerT("文件不存在!");
                    //System.Web.HttpContext.Current.Response.Redirect("/error.aspx?msg=文件不存在!");
                }

            }

        }

      

        /// <summary>
        /// 文件是否过期
        /// </summary>
        /// <param name="filename">物理路径</param>
        /// <param name="Hour">有效时间（小时）</param>
        /// <returns></returns>
        public static bool IsFileExpired(string filename, int Hour)
        {
            try
            {
                DateTime dt = File.GetLastWriteTime(filename);
                TimeSpan ts = DateTime.Now - dt;
                if (ts.TotalHours > Hour)
                    return true;　　　
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
