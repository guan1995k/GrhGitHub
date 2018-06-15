using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace ReportBuild
{
    public class ReportBuild
    {


        public static bool CreateByTemplate()
        {
            try
            {
                //模板文件
                string TempletFileName = Path.GetDirectoryName("template.xlsx") + "template.xlsx";
                //导出文件
                string ReportFileName = Path.GetDirectoryName("out.xlsx") + "out.xlsx";
                string strTempletFile = Path.GetFileName(TempletFileName);
                //将模板文件复制到输出文件
                FileInfo mode = new FileInfo(TempletFileName);
                mode.CopyTo(ReportFileName, true);
                //打开excel
                object missing = Missing.Value;
                Microsoft.Office.Interop.Excel.Application app = null;
                Workbook wb = null;
                Worksheet ws = null;
                Range r = null;
                //
                app = new Microsoft.Office.Interop.Excel.Application();
                wb = app.Workbooks.Open(ReportFileName, false, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                app.Visible = true;
                //得到WorkSheet对象
                ws = (Worksheet)wb.Worksheets.get_Item(1);
                #region 利用三循环完成温度报表的数据输入
                //int longlinenum = 0;//缆数
                //int widelinenum = 0;//节点数
                //int linepointnum = 0;//探头书
                //for (int row = 1; row < longlinenum; row++)
                //{
                //    for (int col = 1; col < widelinenum; col++)
                //    {
                //        for (int point = 1; point < linepointnum; point++)
                //        {
                //            ws.Cells[row * 5 + point, col] = "TempData";
                //        }
                //    }
                //}
                #endregion
                //添加或修改WorkSheet里的数据
                ws.Cells[1, 1] = "100";
                ws.Cells[2, 1] = "100";
                ws.Cells[2, 2] = "100";
                //代码里写个公式
                r = (Range)ws.Cells[2, 3];
                r.Formula = "=A2*B2";
                //输出Excel文件并退出
                wb.Save();
                wb.Close(null, null, null);
                app.Workbooks.Close();
                app.Application.Quit();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                ws = null;
                wb = null;
                app = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
