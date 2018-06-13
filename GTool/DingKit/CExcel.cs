using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel;
using DingKit;
using System.IO;
using System.Data;


/// <summary>
///CExcel 的摘要说明
/// </summary>
public class CExcelOut
{
    /// <summary>
    /// 数据源
    /// </summary>
    public System.Data.DataTable dt;
    /// <summary>
    /// 工作表名称
    /// </summary>
    public string strSheetName = "导出";
    /// <summary>
    /// Excel名称
    /// </summary>
    public string strExcelName;
    /// <summary>
    /// Excel模板名称
    /// </summary>
    public string strTemplateName;
    public const string TempletPath = "EXCEL_Temple/";
    public const string ExcelPath = "EXCEL/";

    public CExcelOut()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 直接创建Excel表
    /// </summary>
    /// <param name="dtSource">数据源DataTable</param>
    /// <param name="filePath">保存路径</param>
    /// <param name="drTitle">标题DataRow</param>
    /// <param name="TableName">Excel表头</param>
    /// <returns></returns>
    public static bool Save(System.Data.DataTable dtSource, string filePath, System.Data.DataRow drTitle, string TableName)
    {
        Excel.Application app = new Excel.Application();//EXCEL实例
        try
        {
            app.Visible = false;
            Workbook wBook = app.Workbooks.Add(true);
            Worksheet wSheet = wBook.Worksheets[1] as Worksheet;
            if (dtSource.Rows.Count > 0)
            {
                int row = dtSource.Rows.Count + 1;
                int col = dtSource.Columns.Count + 1;


                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        string str = dtSource.Rows[i][j].ToString();
                        wSheet.Cells[i + 3, j + 1] = str;
                    }
                }
            }
            int size = dtSource.Columns.Count;
            for (int i = 0; i < size; i++)
            {
                wSheet.Cells[2, 1 + i] = drTitle[i].ToString();
            }

            wSheet.get_Range(wSheet.Cells[1, 0], wSheet.Cells[1, size]).Merge(Type.Missing);
            wSheet.Cells[1, 1] = TableName;

            //设置禁止弹出保存和覆盖的询问提示框 
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;
            //保存工作簿
            wBook.Save();
            //保存excel文件 
            app.Save(filePath);
            app.SaveWorkspace(filePath);
            app.Quit();
            app = null;
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
        }
    }

    /// <summary>
    /// 直接创建Excel表
    /// </summary>
    /// <param name="dtSource">数据源DataTable</param>
    /// <param name="filePath">保存路径</param>
    /// <param name="drTitle">标题DataRow</param>
    /// <returns></returns>
    public static bool Save(System.Data.DataTable dtSource, string filePath, System.Data.DataRow drTitle)
    {
        Excel.Application app = new Excel.Application();//EXCEL实例
        try
        {
            app.Visible = false;
            Workbook wBook = app.Workbooks.Add(true);
            Worksheet wSheet = wBook.Worksheets[1] as Worksheet;
            if (dtSource.Rows.Count > 0)
            {
                int row = dtSource.Rows.Count + 1;
                int col = dtSource.Columns.Count + 1;


                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        string str = dtSource.Rows[i][j].ToString();
                        wSheet.Cells[i + 2, j + 1] = str;
                    }
                }
            }
            int size = dtSource.Columns.Count;
            for (int i = 0; i < size; i++)
            {
                wSheet.Cells[1, 1 + i] = drTitle[i].ToString();
            }
            //设置禁止弹出保存和覆盖的询问提示框 
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;
            //保存工作簿
            wBook.Save();
            //保存excel文件 
            app.Save(filePath);
            app.SaveWorkspace(filePath);
            app.Quit();
            app = null;
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
        }
    }

    /// <summary>
    /// 直接创建Excel表
    /// </summary>
    /// <param name="dtSource">数据源DataTable</param>
    /// <param name="filePath">保存路径</param>
    /// <returns></returns>
    public static bool Save(System.Data.DataTable dtSource, string filePath)
    {
        Excel.Application app = new Excel.Application();//EXCEL实例
        try
        {
            app.Visible = false;
            Workbook wBook = app.Workbooks.Add(true);
            Worksheet wSheet = wBook.Worksheets[1] as Worksheet;
            if (dtSource.Rows.Count > 0)
            {
                int row = 0;
                row = dtSource.Rows.Count;
                int col = dtSource.Columns.Count;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        string str = dtSource.Rows[i][j].ToString();
                        wSheet.Cells[i + 2, j + 1] = str;
                    }
                }
            }
            int size = dtSource.Columns.Count;
            for (int i = 0; i < size; i++)
            {
                wSheet.Cells[1, 1 + i] = dtSource.Columns[i].ColumnName;
            }
            //设置禁止弹出保存和覆盖的询问提示框 
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;
            //保存工作簿
            wBook.Save();
            //保存excel文件 
            app.Save(filePath);
            app.SaveWorkspace(filePath);
            app.Quit();
            app = null;
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
        }
    }


    /// <summary>
    /// 直接创建Excel表
    /// </summary>
    /// <param name="dtSource">数据源DataTable</param>
    /// <param name="filePath">保存路径</param>
    /// <param name="TableName">Excel表头</param>
    /// <returns></returns>
    public static bool Save(System.Data.DataTable dtSource, string filePath, string TableName)
    {
        Excel.Application app = new Excel.Application();//EXCEL实例
        try
        {
            app.Visible = false;
            Workbook wBook = app.Workbooks.Add(true);
            Worksheet wSheet = wBook.Worksheets[1] as Worksheet;
            if (dtSource.Rows.Count > 0)
            {
                int row = 0;
                row = dtSource.Rows.Count;
                int col = dtSource.Columns.Count;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        string str = dtSource.Rows[i][j].ToString();
                        wSheet.Cells[i + 3, j + 1] = str;
                    }
                }
            }
            int size = dtSource.Columns.Count;
            for (int i = 0; i < size; i++)
            {
                wSheet.Cells[2, 1 + i] = dtSource.Columns[i].ColumnName;
            }

            wSheet.get_Range(wSheet.Cells[1, 0], wSheet.Cells[1, size]).Merge(Type.Missing);
            wSheet.Cells[1, 1] = TableName;


            //设置禁止弹出保存和覆盖的询问提示框 
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;
            //保存工作簿
            wBook.Save();
            //保存excel文件 
            app.Save(filePath);
            app.SaveWorkspace(filePath);
            app.Quit();
            app = null;
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
        }
    }

    /// <summary>
    /// 根据模板生成Excel表
    /// </summary>
    /// <param name="dt">数据源</param>
    /// <param name="strExcelName">输出excel</param>
    /// <param name="strTemplateName">模板名称</param>
    public void CreateByTemplate(System.Data.DataTable dt, string strExcelName, string strTemplateName)
    {
        Excel.Application objExcel = new Excel.Application();//EXCEL实例
        Workbooks objBooks;//工作簿 集合
        Workbook objBook;//工作簿 
        Sheets objSheets;//工作表集合
        Worksheet objSheet;//工作表
        Range objCells;//单元格

        string strFile = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + ExcelPath + strExcelName;//下载路径
        string strTemplate = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + TempletPath + strTemplateName;//模板路径

        objExcel.Visible = false;
        objExcel.DisplayAlerts = false;
        //定义一个新的工作簿   
        objBooks = objExcel.Workbooks;
        objBooks.Open(strTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        objBook = objBooks.get_Item(1);
        objSheets = objBook.Worksheets;
        objSheet = (Worksheet)objSheets.get_Item(1);
        //命名该sheet   
        objSheet.Name = strSheetName;

        objCells = objSheet.Cells;
        //将数据导入到Excel中去   
        int intRow;//行号
        int intCol;//列号
        ////添加标题
        //for (intCol = 0; intCol < dt.Columns.Count; intCol++)
        //{
        //    objCells[2, intCol + 1] = dt.Columns[intCol].ToString();
        //}

        //添加内容
        for (intRow = 0; intRow < dt.Rows.Count; intRow++)
        {
            DataRow dr = dt.NewRow();
            dr = dt.Rows[intRow];
            for (intCol = 0; intCol < dt.Columns.Count; intCol++)
            {
                objCells[intRow + 3, intCol + 1] = dr[intCol].ToString();
            }
        }

        //保存到临时文件夹
        objSheet.SaveAs(strFile, Excel.XlFileFormat.xlTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        objBook.Close(false, Type.Missing, Type.Missing);
        //退出Excel，并且释放调用的COM资源   
        objExcel.Quit();
        //回收资源
        GC.Collect();
        //关闭进程
        //KillProcess("Excel");

        //以字符流的形式下载文件 
        FileStream fs = new FileStream(strFile, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();

        CFile.FileDown(strFile, "1");
        //Response.ContentType = "application/octet-stream";
        ////通知浏览器下载文件而不是打开 
        //string strValue = "attachment; filename=" + HttpUtility.UrlEncode(strFile, System.Text.Encoding.UTF8) ;
        //Response.AddHeader("Content-Disposition", strValue);
        //Response.BinaryWrite(bytes);
        //Response.Flush();
        ////删除临时EXCEL文件
        File.Delete(strFile);
        //Response.End();
    }

    /// <summary>
    /// 根据模板生成Excel表
    /// </summary>
    /// <param name="dt">数据源</param>
    /// <param name="strExcelName">输出excel</param>
    /// <param name="strTemplateName">模板名称</param>
    /// <param name="Title">表格标题</param>
    public void CreateByTemplate(System.Data.DataTable dt, string strExcelName, string strTemplateName, string Title)
    {
        Excel.Application objExcel = new Excel.Application();//EXCEL实例
        Workbooks objBooks;//工作簿 集合
        Workbook objBook;//工作簿 
        Sheets objSheets;//工作表集合
        Worksheet objSheet;//工作表
        Range objCells;//单元格

        string strFile = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + ExcelPath + strExcelName;//下载路径
        string strTemplate = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + TempletPath + strTemplateName;//模板路径

        objExcel.Visible = false;
        objExcel.DisplayAlerts = false;
        //定义一个新的工作簿   
        objBooks = objExcel.Workbooks;
        objBooks.Open(strTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        objBook = objBooks.get_Item(1);
        objSheets = objBook.Worksheets;
        objSheet = (Worksheet)objSheets.get_Item(1);
        //命名该sheet   
        objSheet.Name = strSheetName;

        objCells = objSheet.Cells;
        //将数据导入到Excel中去   
        int intRow;//行号
        int intCol;//列号

        //修改大标题
        objCells[1, 1] = Title;

        ////添加标题
        //for (intCol = 0; intCol < dt.Columns.Count; intCol++)
        //{
        //    objCells[2, intCol + 1] = dt.Columns[intCol].ToString();
        //}

        //添加内容
        for (intRow = 0; intRow < dt.Rows.Count; intRow++)
        {
            DataRow dr = dt.NewRow();
            dr = dt.Rows[intRow];
            for (intCol = 0; intCol < dt.Columns.Count; intCol++)
            {
                objCells[intRow + 3, intCol + 1] = dr[intCol].ToString();
            }
        }

        //保存到临时文件夹
        objSheet.SaveAs(strFile, Excel.XlFileFormat.xlTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        objBook.Close(false, Type.Missing, Type.Missing);
        //退出Excel，并且释放调用的COM资源   
        objExcel.Quit();
        //回收资源
        GC.Collect();
        //关闭进程
        //KillProcess("Excel");

        //以字符流的形式下载文件 
        FileStream fs = new FileStream(strFile, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();

        CFile.FileDown(strFile, "1");
        //Response.ContentType = "application/octet-stream";
        ////通知浏览器下载文件而不是打开 
        //string strValue = "attachment; filename=" + HttpUtility.UrlEncode(strFile, System.Text.Encoding.UTF8) ;
        //Response.AddHeader("Content-Disposition", strValue);
        //Response.BinaryWrite(bytes);
        //Response.Flush();
        ////删除临时EXCEL文件
        File.Delete(strFile);
        //Response.End();
    }

    /// <summary>
    /// 根据模板生成Excel表
    /// </summary>
    /// <param name="dt">数据源</param>
    /// <param name="strExcelName">输出excel</param>
    /// <param name="strTemplateName">模板名称</param>
    /// <param name="Title">表格标题</param>
    public static void CreateWithTitle(System.Data.DataTable dt, string strExcelName, string strTemplateName)
    {
        Excel.Application objExcel = new Excel.Application();//EXCEL实例
        Workbooks objBooks;//工作簿 集合
        Workbook objBook;//工作簿 
        Sheets objSheets;//工作表集合
        Worksheet objSheet;//工作表
        Range objCells;//单元格

        string strFile = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + ExcelPath + strExcelName;//下载路径
        string strTemplate = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + TempletPath + strTemplateName;//模板路径

        objExcel.Visible = false;
        objExcel.DisplayAlerts = false;
        //定义一个新的工作簿   
        objBooks = objExcel.Workbooks;
        objBooks.Open(strTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        objBook = objBooks.get_Item(1);
        objSheets = objBook.Worksheets;
        objSheet = (Worksheet)objSheets.get_Item(1);
        //命名该sheet   
        objSheet.Name = "导出数据";

        objCells = objSheet.Cells;
        //将数据导入到Excel中去   
        int intRow;//行号
        int intCol;//列号

        int RowNumber = 0;
        int ColNumber = 0;

        ColNumber = dt.Columns.Count;
        RowNumber = dt.Rows.Count + 1;

        CProcessInfo info = HttpContext.Current.Session["ProcessInfo"] as CProcessInfo;
        info.Message = "excel标题生成";
        //文本格式
        Excel.Range myrange = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[RowNumber, ColNumber]);
        myrange.NumberFormatLocal = "@ ";


        //添加标题
        for (intCol = 0; intCol < dt.Columns.Count; intCol++)
        {
            objCells[1, intCol + 1] = dt.Columns[intCol].ToString();
        }


        //添加内容
        for (intRow = 0; intRow < dt.Rows.Count; intRow++)
        {
            DataRow dr = dt.NewRow();
            dr = dt.Rows[intRow];
            for (intCol = 0; intCol < dt.Columns.Count; intCol++)
            {
                objCells[intRow + 2, intCol + 1] = dr[intCol].ToString();
            }

            info.Message = "第" + intRow.ToString() + "数据插入";
            info.Current = info.Current + intRow;
        }




        //保存到临时文件夹
        objSheet.SaveAs(strFile, Excel.XlFileFormat.xlTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        objBook.Close(false, Type.Missing, Type.Missing);
        //退出Excel，并且释放调用的COM资源   
        objExcel.Quit();
        //回收资源
        GC.Collect();
        //关闭进程
        //KillProcess("Excel");

        //以字符流的形式下载文件 
        FileStream fs = new FileStream(strFile, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();

        // CFile.FileDown(strFile, "1");
        //Response.ContentType = "application/octet-stream";
        ////通知浏览器下载文件而不是打开 
        //string strValue = "attachment; filename=" + HttpUtility.UrlEncode(strFile, System.Text.Encoding.UTF8) ;
        //Response.AddHeader("Content-Disposition", strValue);
        //Response.BinaryWrite(bytes);
        //Response.Flush();
        ////删除临时EXCEL文件
        // File.Delete(strFile);
        //Response.End();
    }



    /// <summary>
    /// 根据模板生成Excel表(带表头);
    /// </summary>
    /// <param name="dt">数据源</param>
    /// <param name="strExcelName">输出excel</param>
    /// <param name="strTemplateName">模板名称</param>
    /// <param name="Title">表格标题</param>
    public static void CreateByTemplateWithTitle(System.Data.DataTable dt, string strExcelName, string strTemplateName, string Title)
    {
        Excel.Application objExcel = new Excel.Application();//EXCEL实例
        Workbooks objBooks;//工作簿 集合
        Workbook objBook;//工作簿 
        Sheets objSheets;//工作表集合
        Worksheet objSheet;//工作表
        Range objCells;//单元格

        string strFile = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + ExcelPath + strExcelName;//下载路径
        string strTemplate = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + TempletPath + strTemplateName;//模板路径

        objExcel.Visible = false;
        objExcel.DisplayAlerts = false;
        //定义一个新的工作簿   
        objBooks = objExcel.Workbooks;
        objBooks.Open(strTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        objBook = objBooks.get_Item(1);
        objSheets = objBook.Worksheets;
        objSheet = (Worksheet)objSheets.get_Item(1);
        //命名该sheet   
        objSheet.Name = "导出数据";

        objCells = objSheet.Cells;
        //将数据导入到Excel中去   
        int intRow;//行号
        int intCol;//列号

        int RowNumber = 0;
        int ColNumber = 0;

        ColNumber = dt.Columns.Count;
        RowNumber = dt.Rows.Count + 2;

        CProcessInfo info = HttpContext.Current.Session["ProcessInfo"] as CProcessInfo;
        info.Message = "excel标题生成";

    

        //设置表名字体
        Excel.Range TitleRange = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[1, ColNumber]);
        TitleRange.Font.Name = "宋体";
        TitleRange.Font.Size = 16;

        TitleRange.Font.Bold = true;
        TitleRange.HorizontalAlignment = Excel.Constants.xlCenter;
        //拼合单元格
        TitleRange.Merge(Type.Missing);


        //设置表名
        objCells[1, 1] = Title;


        //添加列标题
        for (intCol = 0; intCol < dt.Columns.Count; intCol++)
        {
            objCells[2, intCol + 1] = dt.Columns[intCol].ToString();
        }

        //设置列标题字体
        Excel.Range ColumTitleRange = objSheet.get_Range(objSheet.Cells[2, 1], objSheet.Cells[2, ColNumber]);

        ColumTitleRange.Font.Name = "宋体";
        ColumTitleRange.Font.Size = 12;

        ColumTitleRange.Font.Bold = true;
        ColumTitleRange.HorizontalAlignment = Excel.Constants.xlCenter;


        ////设定起始单元格位置
        Excel.Range myrange = objSheet.get_Range(objSheet.Cells[2, 1], objSheet.Cells[RowNumber, ColNumber]);
        //文本格式
        myrange.NumberFormatLocal = "@ ";



        //添加内容
        for (intRow = 0; intRow < dt.Rows.Count; intRow++)
        {
            DataRow dr = dt.NewRow();
            dr = dt.Rows[intRow];
            for (intCol = 0; intCol < dt.Columns.Count; intCol++)
            {
                objCells[intRow + 3, intCol + 1] = dr[intCol].ToString();
            }

            info.Message = "第" + intRow.ToString() + "数据插入";
            info.Current = info.Current + intRow;
        }



        //单元格自适应宽度
        myrange.Columns.AutoFit();

        myrange.Rows.AutoFit();

        //加入表格边框
        Excel.Borders pborders = myrange.Borders;
        //设置左边框
        pborders.get_Item(XlBordersIndex.xlEdgeLeft);
        pborders.LineStyle = Excel.XlLineStyle.xlContinuous;
        pborders.Weight = Excel.XlBorderWeight.xlThin;
        pborders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
        object pObject = myrange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);

        ////设置上边框线
        pborders.get_Item(XlBordersIndex.xlEdgeTop);
        pborders.LineStyle = Excel.XlLineStyle.xlContinuous;
        pborders.Weight = Excel.XlBorderWeight.xlThin;
        pborders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
        pObject = myrange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);

        ////设置下边框线
        pborders.get_Item(XlBordersIndex.xlEdgeBottom);
        pborders.LineStyle = Excel.XlLineStyle.xlContinuous;
        pborders.Weight = Excel.XlBorderWeight.xlThin;
        pborders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
        pObject = myrange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);

        ////设置右边框线
        pborders.get_Item(XlBordersIndex.xlEdgeRight);
        pborders.LineStyle = Excel.XlLineStyle.xlContinuous;
        pborders.Weight = Excel.XlBorderWeight.xlThin;
        pborders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
        pObject = myrange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);

        ////设置内格竖线
        pborders.get_Item(XlBordersIndex.xlInsideVertical);
        pborders.LineStyle = Excel.XlLineStyle.xlContinuous;
        pborders.Weight = Excel.XlBorderWeight.xlThin;
        pborders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
        pObject = myrange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);

        ////设置内格横边框线
        pborders.get_Item(XlBordersIndex.xlInsideHorizontal);
        pborders.LineStyle = Excel.XlLineStyle.xlContinuous;
        pborders.Weight = Excel.XlBorderWeight.xlThin;
        pborders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
        pObject = myrange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);




        //保存到临时文件夹
        objSheet.SaveAs(strFile, Excel.XlFileFormat.xlTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        objBook.Close(false, Type.Missing, Type.Missing);
        //退出Excel，并且释放调用的COM资源   
        objExcel.Quit();
        //回收资源
        GC.Collect();
        //关闭进程
        //KillProcess("Excel");

        //以字符流的形式下载文件 
        FileStream fs = new FileStream(strFile, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();

        // CFile.FileDown(strFile, "1");
        //Response.ContentType = "application/octet-stream";
        ////通知浏览器下载文件而不是打开 
        //string strValue = "attachment; filename=" + HttpUtility.UrlEncode(strFile, System.Text.Encoding.UTF8) ;
        //Response.AddHeader("Content-Disposition", strValue);
        //Response.BinaryWrite(bytes);
        //Response.Flush();
        ////删除临时EXCEL文件
        // File.Delete(strFile);
        //Response.End();
    }

}